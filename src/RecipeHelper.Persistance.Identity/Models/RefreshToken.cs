using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHelper.Persistance.Identity.Models
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TokenId { get; set; } = default!;
        public string JwtId { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public DateTime ExpireDate { get; set; } = default!;
        public bool Used { get; set; } = default!;
        public bool Invalidated { get; set; } = default!;
        public string UserId { get; set; } = default!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = default!;
    }
}
