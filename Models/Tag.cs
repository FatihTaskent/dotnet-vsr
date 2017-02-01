using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_core.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}