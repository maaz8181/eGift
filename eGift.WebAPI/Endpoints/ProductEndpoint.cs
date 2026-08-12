using eGift.WebAPI.Common;
using eGift.WebAPI.Data;
using eGift.WebAPI.Dtos;
using eGift.WebAPI.Mappings;
using Microsoft.EntityFrameworkCore;

namespace eGift.WebAPI.Endpoints;

public static class ProductEndpoint
{
    public static RouteGroupBuilder MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/product").WithTags("Product");

        #region Default CRUD Endpoints

        // GET: api/product
        group.MapGet("/", async (AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("ProductEndpoint");

            try
            {
                var products = await (
                    from product in context.Products
                    join category in context.Categories on product.CategoryId equals category.Id
                    join subCategory in context.SubCategories on product.SubCategoryId equals subCategory.Id
                    where !product.IsDeleted
                    select new
                    {

                        Id = product.Id,
                        Name = product.Name,

                        CategoryId = product.CategoryId,
                        CategoryName = category.CategoryName,

                        SubCategoryId = product.SubCategoryId,
                        SubCategoryName = subCategory.SubCategoryName,

                        QuantityPerUnit = product.QuantityPerUnit,
                        UnitPrice = product.UnitPrice,
                        SizeId = product.SizeId,
                        Discount = product.Discount,
                        UnitInStock = product.UnitInStock,
                        UnitInOrder = product.UnitInOrder,
                        ProductAvailable = product.ProductAvailable,

                        ShortDescription = product.ShortDescription,
                        LongDescription = product.LongDescription,

                        PicturePath1 = product.PicturePath1,
                        PicturePath2 = product.PicturePath2,
                        PicturePath3 = product.PicturePath3,
                        PicturePath4 = product.PicturePath4,

                        PictureData1 = product.PictureData1,
                        PictureData2 = product.PictureData2,
                        PictureData3 = product.PictureData3,
                        PictureData4 = product.PictureData4,

                        ProductImagePath = product.ProductImagePath,
                        ProductImageData = product.ProductImageData,

                        CreatedDate = product.CreatedDate
                    }
                )
                .AsNoTracking()
                .ToListAsync();

                return products is null
                    ? Results.NotFound()
                    : Results.Ok(products);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in ProductEndpoint: /api/product GET: {Message}.",
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = "An error occurred while retrieving products.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // GET: api/product/{id}
        group.MapGet("/{id:int}", async (int id, AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("ProductEndpoint");

            try
            {
                var product = await (
                    from p in context.Products
                    join category in context.Categories on p.CategoryId equals category.Id
                    join subCategory in context.SubCategories on p.SubCategoryId equals subCategory.Id
                    where p.Id == id && !p.IsDeleted
                    select new
                    {
                        Id = p.Id,
                        Name = p.Name,

                        CategoryId = p.CategoryId,
                        CategoryName = category.CategoryName,

                        SubCategoryId = p.SubCategoryId,
                        SubCategoryName = subCategory.SubCategoryName,

                        QuantityPerUnit = p.QuantityPerUnit,
                        UnitPrice = p.UnitPrice,
                        SizeId = p.SizeId,
                        SizeName = p.SizeId.HasValue ? ((Size)p.SizeId.Value).ToString() : null,

                        Discount = p.Discount,
                        UnitInStock = p.UnitInStock,
                        UnitInOrder = p.UnitInOrder,
                        ProductAvailable = p.ProductAvailable,

                        ShortDescription = p.ShortDescription,
                        LongDescription = p.LongDescription,

                        PicturePath1 = p.PicturePath1,
                        PicturePath2 = p.PicturePath2,
                        PicturePath3 = p.PicturePath3,
                        PicturePath4 = p.PicturePath4,

                        PictureData1 = p.PictureData1,
                        PictureData2 = p.PictureData2,
                        PictureData3 = p.PictureData3,
                        PictureData4 = p.PictureData4,

                        ProductImagePath = p.ProductImagePath,
                        ProductImageData = p.ProductImageData
                    }
                )
                .AsNoTracking()
                .FirstOrDefaultAsync();

                return product is null
                    ? Results.NotFound()
                    : Results.Ok(product);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in ProductEndpoint: /api/product/{id} GET: {Message}.",
                    id,
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = $"An error occurred while retrieving the product with ID {id}.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // POST: api/product
        group.MapPost("/", async (ProductDto dto, AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("ProductEndpoint");

            try
            {
                var product = dto.ToEntity();

                context.Products.Add(product);
                await context.SaveChangesAsync();

                return Results.Created(
                    $"/api/product/{product.Id}",
                    product
                );
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in ProductEndpoint: /api/product POST: {Message}.",
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = "An error occurred while creating the product.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // PUT: api/product/{id}
        group.MapPut("/{id:int}", async (int id, EditProductDto dto, AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("ProductEndpoint");

            try
            {
                var existingProduct = await context.Products.FindAsync(id);

                if (existingProduct is null)
                {
                    return Results.NotFound();
                }

                existingProduct.ToEntity(dto);

                context.Products.Update(existingProduct);
                await context.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in ProductEndpoint: /api/product/{id} PUT: {Message}.",
                    id,
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = $"An error occurred while updating the product with ID {id}.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // DELETE: api/product/{id}?loginUserId={loginUserId}&deletedDate={deletedDate}
        group.MapDelete("/{id:int}", async (int id, int loginUserId, DateTime deletedDate, AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("ProductEndpoint");

            try
            {
                var existingProduct = await context.Products.FindAsync(id);

                if (existingProduct is null)
                {
                    return Results.NotFound();
                }

                existingProduct.IsDeleted = true;
                existingProduct.UpdatedBy = loginUserId;
                existingProduct.UpdatedDate = deletedDate;

                context.Products.Update(existingProduct);
                await context.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in ProductEndpoint: /api/product/{id} DELETE: {Message}.",
                    id,
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = $"An error occurred while deleting the product with ID {id}.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        #endregion

        return group;
    }
}