using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Data;

namespace Twitter.Models
{
    public class TweetModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(280)]
        public string Content { get; set; }
        public string Time { get; set; }

        //Relationship One-to-Many (user-tweets)
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }


        //Relationship One-to-Many (parrentTweet-tweets)
        public int? ParentId { get; set; }
        public TweetModel? Parent { get; set; }
       
        public List <TweetModel> Replies { get; set; }


    }
}
