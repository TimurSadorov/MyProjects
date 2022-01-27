using System;

namespace BeatTim.Models
{
    public class UserComment
    {
        public int UserCommentId { get; set; }
        public int CommentatorId { get; set; }
        public Client Commentator { get; set; }
        public int CommentedId { get; set; }
        public Client Commented { get; set; }
        public string CommentContent { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsUnderConsideration { get; set; }
    }
}