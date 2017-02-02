using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dotnet_core.Models;

namespace dotnet_vsr.ViewModels
{
    public class IndexViewModel
    {
        public Account Account { get; set; }

        public List<Message> Messages { get; set; }
    }
}