using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dotnet_core.Models;

namespace dotnet_vsr.ViewModels
{
    public class DetailsViewModel
    {
        public Account Account { get; set; }
        public Message Message { get; set; }

        public List<Message> Replies { get; set; }
    }
}