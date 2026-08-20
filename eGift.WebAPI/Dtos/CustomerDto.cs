using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eGift.WebAPI.Dtos;

public record CustomerDto(
   int Id,
   [Required] string FirstName,
   [Required] string LastName,
   [Required] DateTime DateofBirth,
   [Required] int GenderId,
   [Required] string Mobile,
   [Required] string? Email,
   int? AddressId,
   bool IsActive,
   [Required] string? ProfileImagePath,
   string? ProfileImageData,
   [Required] int RoleId,
   bool IsDefault,
   bool IsDeleted,
   int CreatedBy,
   DateTime CreatedDate

)
{
   [JsonIgnore]
   public IFormFile? ProfileImage { get; set; }
};
