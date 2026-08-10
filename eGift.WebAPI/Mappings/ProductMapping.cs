using eGift.WebAPI.Dtos;
using eGift.WebAPI.Models;

namespace eGift.WebAPI.Mappings;

public static class ProductMapping
{
    // Entity -> DTO
    public static ProductDto ToDto(this ProductModel entity)
    {
        return new ProductDto(
            entity.Id,
            entity.Name,
            entity.CategoryId,
            entity.SubCategoryId,
            entity.QuantityPerUnit,
            entity.UnitPrice,
            entity.SizeId,
            entity.Discount,
            entity.UnitInStock,
            entity.UnitInOrder,
            entity.ProductAvailable,
            entity.ShortDescription,
            entity.LongDescription,
            entity.PicturePath1,
            entity.PicturePath2,
            entity.PicturePath3,
            entity.PicturePath4,
            entity.PictureData1,
            entity.PictureData2,
            entity.PictureData3,
            entity.PictureData4,
            entity.ProductImagePath,
            entity.ProductImageData,
            entity.IsDeleted,
            entity.CreatedBy,
            entity.CreatedDate
        );
    }

    // DTO -> Entity (create)
    public static ProductModel ToEntity(this ProductDto dto)
    {
        return new ProductModel
        {
            Id = dto.Id,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            SubCategoryId = dto.SubCategoryId,
            QuantityPerUnit = dto.QuantityPerUnit,
            UnitPrice = dto.UnitPrice,
            SizeId = dto.SizeId,
            Discount = dto.Discount,
            UnitInStock = dto.UnitInStock,
            UnitInOrder = dto.UnitInOrder,
            ProductAvailable = dto.ProductAvailable,
            ShortDescription = dto.ShortDescription,
            LongDescription = dto.LongDescription,
            PicturePath1 = dto.PicturePath1,
            PicturePath2 = dto.PicturePath2,
            PicturePath3 = dto.PicturePath3,
            PicturePath4 = dto.PicturePath4,
            PictureData1 = dto.PictureData1,
            PictureData2 = dto.PictureData2,
            PictureData3 = dto.PictureData3,
            PictureData4 = dto.PictureData4,
            ProductImagePath = dto.ProductImagePath,
            ProductImageData = dto.ProductImageData,
            IsDeleted = dto.IsDeleted,
            CreatedBy = dto.CreatedBy,
            CreatedDate = dto.CreatedDate
        };
    }

    // DTO -> Entity (update existing entity)
    public static void ToEntity(this ProductModel entity, EditProductDto dto)
    {
        entity.Name = dto.Name;
        entity.CategoryId = dto.CategoryId;
        entity.SubCategoryId = dto.SubCategoryId;
        entity.QuantityPerUnit = dto.QuantityPerUnit;
        entity.UnitPrice = dto.UnitPrice;
        entity.SizeId = dto.SizeId;
        entity.Discount = dto.Discount;
        entity.UnitInStock = dto.UnitInStock;
        entity.UnitInOrder = dto.UnitInOrder;
        entity.ProductAvailable = dto.ProductAvailable;
        entity.ShortDescription = dto.ShortDescription;
        entity.LongDescription = dto.LongDescription;
        entity.PicturePath1 = dto.PicturePath1;
        entity.PicturePath2 = dto.PicturePath2;
        entity.PicturePath3 = dto.PicturePath3;
        entity.PicturePath4 = dto.PicturePath4;
        entity.PictureData1 = dto.PictureData1;
        entity.PictureData2 = dto.PictureData2;
        entity.PictureData3 = dto.PictureData3;
        entity.PictureData4 = dto.PictureData4;
        entity.ProductImagePath = dto.ProductImagePath;
        entity.ProductImageData = dto.ProductImageData;
        entity.IsDeleted = dto.IsDeleted;
        entity.UpdatedBy = dto.UpdatedBy;
        entity.UpdatedDate = dto.UpdatedDate;
    }
}