using System.ComponentModel.DataAnnotations;

namespace MediaMarket.Application.DTO.Request.Tag
{
    public class UpdateTagRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public required string Name { get; set; }
    }
}
