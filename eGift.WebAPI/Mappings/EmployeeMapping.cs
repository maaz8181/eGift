using eGift.WebAPI.Dtos;
using eGift.WebAPI.Models;

namespace eGift.WebAPI.Mappings;

public static class EmployeeMapping
{
    // Entity -> DTO
    public static EmployeeDto ToDto(this EmployeeModel entity)
    {
        return new EmployeeDto(
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
            ProfileImage=entity.ProfileImage
        };
    }

    // DTO -> Entity (create)
    public static EmployeeModel ToEntity(this EmployeeDto dto)
    {
        return new EmployeeModel
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
            ProfileImage =dto.ProfileImage
        };
    }
    // DTO -> Entity (update existing entity)
    public static void ToEntity(this EmployeeModel entity, EditEmployeeDto dto)
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