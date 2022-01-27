using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeatTim.DTO;
using BeatTim.Models;
using BeatTim.Repositories;
using BeatTim.Services.DTO.OutputDTO;
using BeatTim.Services.OutputDTO;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;

namespace BeatTim.Services
{
	public class AccountService
	{
		private readonly IUserProfileRepository _userProfileRepository;
		private readonly IUserCommentRepository _userCommentRepository;
		private readonly IContactRepository _contactRepository;
		private readonly IClientRepository _clientRepository;
		private readonly IBeatRepository _beatRepository;
		private readonly IWebHostEnvironment _appEnvironment;
		private static readonly string pathToUserPhoto = "/img/user_photo/";

		public AccountService(IUserProfileRepository userProfileRepository,
			IUserCommentRepository userCommentRepository,
			IWebHostEnvironment appEnvironment,
			IContactRepository contactRepository,
			IClientRepository clientRepository,
			IBeatRepository beatRepository)
		{
			_userProfileRepository = userProfileRepository;
			_userCommentRepository = userCommentRepository;
			_appEnvironment = appEnvironment;
			_contactRepository = contactRepository;
			_clientRepository = clientRepository;
			_beatRepository = beatRepository;
		}
		
		public async Task<bool> SetVerificationStatusComments(int commentId, bool isApproved)
		{
			var comment = await _userCommentRepository.GetAsync(commentId);
			if (comment is null || !comment.IsUnderConsideration)
				return false;

			if (isApproved)
			{
				comment.IsUnderConsideration = false;
				await _userCommentRepository.UpdateAsync(comment);
			}
			else
				await _userCommentRepository.DeleteAsync(comment);
			return true;
		}

		public async Task<UserProfileDto> GetUserProfileAsync(int clientId)
		{
			var userProfile = await _userProfileRepository.GetAsync(clientId);
			if (userProfile is not null)
				return new UserProfileDto(await _userProfileRepository.GetAsync(clientId));

			return null;
		}
		
		public async Task<bool> TryDeleteComment(int commentId)
		{
			var comment = await _userCommentRepository.GetAsync(commentId);
			if (comment is null)
				return false;

			await _userCommentRepository.DeleteAsync(comment);
			return true;
		}

		public IEnumerable<UserCommentDto> GetSortedCommentByDate(int clientId, int amountSkipped, int amountTaken)
		{
			return _userCommentRepository.GetSortedDate(clientId, amountSkipped, amountTaken)
				.Select(u => new UserCommentDto(u));
		}

		public async Task UpdateUserProfileAsync(UserProfileDto userProfile)
		{
			var oldUserProfile = await _userProfileRepository.GetAsync(userProfile.ClientId);
			UpdateMainInfo(userProfile, oldUserProfile);
			if (oldUserProfile.Contact is null)
				await AddNewContactAsync(userProfile.Contact, oldUserProfile);
			else
				UpdateContacts(userProfile, oldUserProfile);

			if (userProfile.UserPhoto is not null)
				await UpdatePhotoAsync(userProfile.UserPhoto, oldUserProfile);

			await _userProfileRepository.UpdateAsync(oldUserProfile);
		}

		private async Task AddNewContactAsync(Contact contact, UserProfile oldUserProfile)
		{
			await _contactRepository.AddAsync(contact);
			oldUserProfile.Contact = contact;
		}

		private void UpdateMainInfo(UserProfileDto userProfile, UserProfile oldUserProfile)
		{
			oldUserProfile.AboutMe = userProfile.AboutMe;
			oldUserProfile.City = userProfile.City;
			oldUserProfile.Nickname = userProfile.Nickname;
		}

		private async Task UpdatePhotoAsync(Image image, UserProfile oldUserProfile)
		{
			var userPhotoLink = pathToUserPhoto +
			                    $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";
			await image.SaveAsync(_appEnvironment.WebRootPath + userPhotoLink);
			oldUserProfile.UserPhotoLink = userPhotoLink;
		}

		private void UpdateContacts(UserProfileDto userProfile, UserProfile oldUserProfile)
		{
			oldUserProfile.Contact.FacebookLink = userProfile.Contact.FacebookLink;
			oldUserProfile.Contact.InstagramLink = userProfile.Contact.InstagramLink;
			oldUserProfile.Contact.TelegramLink = userProfile.Contact.TelegramLink;
			oldUserProfile.Contact.TwitterLink = userProfile.Contact.TwitterLink;
			oldUserProfile.Contact.VkLink = userProfile.Contact.VkLink;
			oldUserProfile.Contact.YoutubeLink = userProfile.Contact.YoutubeLink;
		}

		public async Task<bool> TryReportCommentAsync(int commentId)
		{
			if (commentId <= 0)
				return false;

			var comment = await _userCommentRepository.GetAsync(commentId);
			if (comment is null)
				return false;

			comment.IsUnderConsideration = true;
			await _userCommentRepository.UpdateAsync(comment);
			return true;
		}

		public async Task<IdEntity> TryAddCommentAsync(string content, int commentedId, int commentatorId)
		{
			if (await _clientRepository.GetAsync(commentedId) is null)
				return null;

			var newComment = CreateComment(content, commentedId, commentatorId);
			await _userCommentRepository.AddAsync(newComment);
			return new IdEntity(newComment.UserCommentId);
		}

		private UserComment CreateComment(string content, int commentedId, int commentatorId) =>
			new()
			{
				CommentatorId = commentatorId,
				CommentedId = commentedId,
				CommentContent = content,
				IsUnderConsideration = false,
				PublicationDate = DateTime.Now
			};

		public async Task<UserCommentDto> GetComment(int id)
		{
			return new UserCommentDto(await _userCommentRepository.GetWithUserProfileAsync(id));
		}

		public IEnumerable<PublicUserProfileDto> GetUsersByInitialCoincidenceNickname(string nickname)
		{
			var users = _userProfileRepository
				.GetUsersByInitialCoincidenceNickname(nickname.ToLower());
			var amountAuditions = _beatRepository
				.GetSumNumberAuditions(users
					.Select(u => u.ClientId)
					.ToHashSet());
			return users
				.Select(u => new PublicUserProfileDto(u.ClientId, u.UserPhotoLink, u.Nickname, 
					amountAuditions.TryGetValue(u.ClientId, out var na) ? na : 0));
		}

		public IEnumerable<UserCommentDto> GetPendingCommentsOrderByDatePublication(int amountSkip, int amountTake)
		{
			return _userCommentRepository
				.GetPendingCommentsOrderByDatePublication(amountSkip, amountTake)
				.Select(c => new UserCommentDto(c));
		}
	}
}