using System.ComponentModel.DataAnnotations;

namespace eGift.WebAPI.Dtos;
public record EditSubCategoryDto(
    [Required] int Id,
    [Required] int CategoryId,
    string SubCategoryName,
    string? Description,
    bool IsDeleted,
    int UpdatedBy,
    DateTime UpdatedDate
);