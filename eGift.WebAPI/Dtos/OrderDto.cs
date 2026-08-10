using System.ComponentModel.DataAnnotations;

namespace eGift.WebAPI.Dtos;
public record OrderDto(
    int Id,
    [Required] int CustomerId,
    [Required] decimal TotalAmount, 
    decimal? TotalDiscount, 
    decimal? TotalTax,
    string OrderNumber,
    string? Notes, 
    DateTime? DispatchedDate,
    DateTime? ShippedDate,
    DateTime? DeliveryDate, 
    DateTime? CancelDate,
    [Required] int StatusId,
    bool IsDeleted,
    int CreatedBy,
    DateTime CreatedDate
);