using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnet_core.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Message> Messages { get; set; }

        public List<Upvote> Upvotes { get; set; }
        public List<Favorite> Favorites { get; set; }
    }
}