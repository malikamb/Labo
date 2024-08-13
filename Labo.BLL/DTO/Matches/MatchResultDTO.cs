using Labo.DL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Labo.BLL.DTO.Matches
{
    public class MatchResultDTO
    {
        [Required]
        public MatchResult Result { get; set; }
    }
}
