using eGift.WebAPI.Dtos;
using eGift.WebAPI.Models;

namespace eGift.WebAPI.Mappings;
public static class CustomerMapping
{
    // Entity -> DTO
    public static CustomerDto ToDto(this CustomerModel entity)
    {
        return new CustomerDto(
           entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.DateofBirth,
            entity.GenderId,
            entity.Mobile,
            entity.Email,
            entity.AddressId,
            entity.IsActive,
            entity.ProfileImagePath,
            entity.ProfileImageData,
            entity.RoleId,
            entity.IsDefault,
            entity.IsDeleted,
            entity.CreatedBy,
            entity.CreatedDate
        )
        {
            ProfileImage = entity.ProfileImage
        }; ;
    }

    // DTO -> Entity (create)
    public static CustomerModel ToEntity(this CustomerDto dto)
    {
        return new CustomerModel
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateofBirth = dto.DateofBirth,
            GenderId = dto.GenderId,
            Mobile = dto.Mobile,
            Email = dto.Email,
            AddressId = dto.AddressId,
            IsActive = dto.IsActive,
            ProfileImagePath = dto.ProfileImagePath,
            ProfileImageData = dto.ProfileImageData,
            RoleId = dto.RoleId,
            IsDefault = dto.IsDefault,
            IsDeleted = dto.IsDeleted,
            CreatedBy = dto.CreatedBy,
            CreatedDate = dto.CreatedDate,
            ProfileImage = dto.ProfileImage
        };
    }
    // DTO -> Entity (update existing entity)
    public static void ToEntity(this CustomerModel entity, EditCustomerDto dto)
    {
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.DateofBirth = dto.DateofBirth;
        entity.GenderId = dto.GenderId;
        entity.Mobile = dto.Mobile;
        entity.Email = dto.Email;
        entity.AddressId = dto.AddressId;
        entity.IsActive = dto.IsActive;
        entity.ProfileImagePath = dto.ProfileImagePath;
        entity.ProfileImageData = dto.ProfileImageData;
        entity.RoleId = dto.RoleId;
        entity.IsDefault = dto.IsDefault;
        entity.IsDeleted = dto.IsDeleted;
        entity.UpdatedBy = dto.UpdatedBy;
        entity.UpdatedDate = dto.UpdatedDate;
        entity.ProfileImage = dto.ProfileImage;

    }

}