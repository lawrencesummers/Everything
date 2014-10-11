using System.ComponentModel.DataAnnotations;

namespace Models.UserModels
{
    public class Note : DbSetBase
    {
        [MaxLength]
        public string Content { get; set; }
    }
}