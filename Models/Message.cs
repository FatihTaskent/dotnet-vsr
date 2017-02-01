using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


//https://docs.microsoft.com/en-us/ef/core/modeling/relationships#many-to-many
namespace dotnet_core.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public Message ParentMessage { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }

        public Account Account { get; set; }

        public Tag Tag { get; set; }

        public DateTime PostDate { get; set; }

        public List<Upvote> Upvotes { get; set; }
        public List<Favorite> Favorites { get; set; }
    }
}