using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta
{
    public class SecretSantaParticipant
    {
        [Key]
        [Required]
        [Phone(ErrorMessage = "Phone Number must be a fully " +
            "formed phone number with no special charecters")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string GiftIdeas { get; set; }

        public string RequestId { get; set; }

        [ForeignKey("MatchForeignKey")]
        public SecretSantaParticipant Match { get; set; }

        public string Role { get; set; }

        public bool HasGiver { get; set; }

        [ForeignKey("GiverForeignKey")]
        public SecretSantaParticipant Giver { get; set; }
    }
}