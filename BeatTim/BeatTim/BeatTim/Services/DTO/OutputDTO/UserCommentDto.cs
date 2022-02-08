using System;
using BeatTim.Models;

namespace BeatTim.Services.OutputDTO
{
    public class UserCommentDto
    {
        public UserCommentDto(UserComment userComment)
        {
            CommentId = userComment.UserCommentId;
            CommentatorId = userComment.CommentatorId;
            CommentContent = userComment.CommentContent;
            CommentatorPhotoLink = userComment.Commentator.UserProfile.UserPhotoLink;
            CommentatorNickname = userComment.Commentator.UserProfile.Nickname;
            PublicationDate = userComment.PublicationDate;
        }

        public int CommentId { get; set; }
        public int CommentatorId { get; set; }
        public string CommentatorNickname { get; set; }
        public string CommentatorPhotoLink { get; set; }
        public string CommentContent { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}