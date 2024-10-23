using System.ComponentModel.DataAnnotations;

namespace MediaMarket.Application.DTO.Request.Tag
{
    public class CreateTagRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public required string Name { get; set; }
    }
}
