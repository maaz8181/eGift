using System.ComponentModel.DataAnnotations;

namespace eGift.WebAPI.Dtos;

public record EditProductDto(
    int Id,
   [Required] string Name,
    [Required] int CategoryId,
    [Required] int SubCategoryId,
    [Required] int QuantityPerUnit,
    [Required] decimal UnitPrice,
    [Required] int? SizeId,
    [Required] decimal? Discount,
    [Required] int UnitInStock,
    [Required] int UnitInOrder,
    [Required] int ProductAvailable,
    string? ShortDescription,
    string? LongDescription,
    [Required] string? PicturePath1,
    [Required] string? PicturePath2,
    [Required] string? PicturePath3,
    [Required] string? PicturePath4,
    byte[]? PictureData1,
    byte[]? PictureData2,
    byte[]? PictureData3,
    byte[]? PictureData4,
    [Required] string ProductImagePath,
    byte[] ProductImageData,
    bool IsDeleted,
    int UpdatedBy,
    DateTime UpdatedDate
);