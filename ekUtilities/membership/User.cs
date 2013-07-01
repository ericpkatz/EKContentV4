using System.ComponentModel.DataAnnotations;
using ekUtilities;

namespace ekUtilities.membership
{
    public class User: BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}