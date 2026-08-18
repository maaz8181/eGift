using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eGift.WebAPI.Dtos;

public record EditEmployeeDto(
    int Id,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateTime DateofBirth,
    [Required] int GenderId,
    [Required] string Mobile,
    [Required] string? Email,
    int? AddressId,
    bool IsActive,
    string? ProfileImagePath,
    string? ProfileImageData,
    [Required] int RoleId,
    bool IsDefault,
    bool IsDeleted,
    int UpdatedBy,
    DateTime UpdatedDate
)
{
    [JsonIgnore]
    public IFormFile? ProfileImage { get; set; }
};
