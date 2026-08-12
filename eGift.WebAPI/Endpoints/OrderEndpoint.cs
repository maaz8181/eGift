using eGift.WebAPI.Data;
using eGift.WebAPI.Dtos;
using eGift.WebAPI.Mappings;
using Microsoft.EntityFrameworkCore;

namespace eGift.WebAPI.Endpoints;

public static class OrderEndpoint
{
    public static RouteGroupBuilder MapOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/order").WithTags("Order");

        #region Default CRUD Endpoints

        // GET: api/order
        group.MapGet("/", async (AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("OrderEndpoint");

            try
            {
                var orders = await (
                    from order in context.Orders
                    join customer in context.Customers on order.CustomerId equals customer.Id
                    where !order.IsDeleted
                    select new
                    {
                        Id = order.Id,
                        CustomerId = order.CustomerId,
                        CustomerName = customer.FirstName + " " + customer.LastName,
                        TotalAmount = order.TotalAmount,
                        TotalDiscount = order.TotalDiscount,
                        TotalTax = order.TotalTax,
                        OrderNumber = order.OrderNumber,
                        Notes = order.Notes,
                        DispatchedDate = order.DispatchedDate,
                        ShippedDate = order.ShippedDate,
                        DeliveryDate = order.DeliveryDate,
                        CancelDate = order.CancelDate,
                        StatusId = order.StatusId,
                        CreatedDate = order.CreatedDate
                    }
                )
                .AsNoTracking()
                .ToListAsync();

                return orders is null
                    ? Results.NotFound()
                    : Results.Ok(orders);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in OrderEndpoint: /api/order GET: {Message}.",
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = "An error occurred while retrieving orders.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // GET: api/order/{id}
        group.MapGet("/{id:int}", async ( int id,  AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("OrderEndpoint");

            try
            {
                var order = await (
                    from o in context.Orders
                    join customer in context.Customers on o.CustomerId equals customer.Id
                    where o.Id == id && !o.IsDeleted
                    select new
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        CustomerName = customer.FirstName + " " + customer.LastName,
                        TotalAmount = o.TotalAmount,
                        TotalDiscount = o.TotalDiscount,
                        TotalTax = o.TotalTax,
                        OrderNumber = o.OrderNumber,
                        Notes = o.Notes,
                        DispatchedDate = o.DispatchedDate,
                        ShippedDate = o.ShippedDate,
                        DeliveryDate = o.DeliveryDate,
                        CancelDate = o.CancelDate,
                        StatusId = o.StatusId
                    }
                )
                .AsNoTracking()
                .FirstOrDefaultAsync();

                return order is null
                    ? Results.NotFound()
                    : Results.Ok(order);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in OrderEndpoint: /api/order/{id} GET: {Message}.",
                    id,
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = $"An error occurred while retrieving the order with ID {id}.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // POST: api/order
        group.MapPost("/", async ( OrderDto dto,  AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("OrderEndpoint");

            try
            {
                var order = dto.ToEntity();

                context.Orders.Add(order);
                await context.SaveChangesAsync();

                return Results.Created(
                    $"/api/order/{order.Id}",
                    order
                );
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in OrderEndpoint: /api/order POST: {Message}.",
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = "An error occurred while creating the order.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // PUT: api/order/{id}
        group.MapPut("/{id:int}", async (  int id,  EditOrderDto dto, AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("OrderEndpoint");

            try
            {
                var existingOrder = await context.Orders.FindAsync(id);

                if (existingOrder is null)
                {
                    return Results.NotFound();
                }

                existingOrder.ToEntity(dto);

                context.Orders.Update(existingOrder);
                await context.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in OrderEndpoint: /api/order/{id} PUT: {Message}.",
                    id,
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = $"An error occurred while updating the order with ID {id}.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // DELETE: api/order/{id}?loginUserId={loginUserId}&deletedDate={deletedDate}
        group.MapDelete("/{id:int}", async (  int id,  int loginUserId,  DateTime deletedDate, AppDBContext context,
            ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("OrderEndpoint");

            try
            {
                var existingOrder = await context.Orders.FindAsync(id);

                if (existingOrder is null)
                {
                    return Results.NotFound();
                }

                existingOrder.IsDeleted = true;
                existingOrder.UpdatedBy = loginUserId;
                existingOrder.UpdatedDate = deletedDate;

                context.Orders.Update(existingOrder);
                await context.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Exception in OrderEndpoint: /api/order/{id} DELETE: {Message}.",
                    id,
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = $"An error occurred while deleting the order with ID {id}.",
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