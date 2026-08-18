using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGift.Admin.Models.ViewModels;

public class EmployeeViewModel : BaseViewModel
{
    #region Data Model Properties

    public int Id { get; set; }

    [Display(Name = "First Name")]
    [Required(ErrorMessage = "This field is required.")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "This field is required.")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Date of Birth")]
    [Required(ErrorMessage = "This field is required.")]
    public DateTime? DateofBirth { get; set; }

    [Display(Name = "Gender")]
    [Required(ErrorMessage = "This field is required.")]
    public int GenderId { get; set; }

    [Display(Name = "Mobile")]
    [Required(ErrorMessage = "This field is required.")]
    public string Mobile { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "This field is required.")]
    public string? Email { get; set; }

    [Display(Name = "Address")]
    public int? AddressId { get; set; }

    [Display(Name = "Is Active")]
    public bool IsActive { get; set; }

    [Display(Name = "Profile Image Path")]
    public string? ProfileImagePath { get; set; }

    [Display(Name = "Profile Image Data")]
    public string? ProfileImageData { get; set; }

    [Display(Name = "Role")]
    [Required(ErrorMessage = "This field is required.")]
    public int RoleId { get; set; }

    [Display(Name = "Is Default")]
    public bool IsDefault { get; set; }

    #endregion

    #region View Model Properties

    [Display(Name = "Age")]
    public int Age { get; set; }

    [Display(Name = "Gender Name")]
    public string? GenderName { get; set; }

    [Display(Name = "Address Name")]
    public string? AddressName { get; set; }

    [Display(Name = "Role Name")]
    public string? RoleName { get; set; }

    #endregion

    #region View Model Properties
    public IFormFile? ProfileImage { get; set; }
    #endregion

    #region Select List Properties

    public SelectList? Addresses { get; set; }
    public SelectList? Genders { get; set; }
    public SelectList? Roles { get; set; }

    #endregion
}