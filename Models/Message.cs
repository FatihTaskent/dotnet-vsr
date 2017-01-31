using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


//https://docs.microsoft.com/en-us/ef/core/modeling/relationships#many-to-many
namespace dotnet_core.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }

        public Account Account { get; set; }

        public string Text { get; set; }

        public DateTime PostDate { get; set; }

        //public virtual List<Account> Upvotes { get; set; }

        //public virtual List<Account> Favorites { get; set; }
    }
}