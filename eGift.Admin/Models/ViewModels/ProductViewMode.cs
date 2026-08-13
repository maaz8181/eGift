using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGift.Admin.Models.ViewModels;

public class ProductViewModel : BaseViewModel
{
    #region Data Model Properties

    public int Id { get; set; }

    [Display(Name = "Name")]
    [Required(ErrorMessage = "This field is required.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Category")]
    [Required(ErrorMessage = "This field is required.")]
    public int CategoryId { get; set; }

    [Display(Name = "Sub Category")]
    [Required(ErrorMessage = "This field is required.")]
    public int SubCategoryId { get; set; }

    [Display(Name = "Quantity Per Unit")]
    [Required(ErrorMessage = "This field is required.")]
    public int QuantityPerUnit { get; set; }

    [Display(Name = "Unit Price")]
    [Required(ErrorMessage = "This field is required.")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "Size")]
    [Required(ErrorMessage = "This field is required.")]
    public int? SizeId { get; set; }

    [Display(Name = "Discount")]
    [Required(ErrorMessage = "This field is required.")]
    public decimal? Discount { get; set; }

    [Display(Name = "Unit In Stock")]
    [Required(ErrorMessage = "This field is required.")]
    public int UnitInStock { get; set; }

    [Display(Name = "Unit In Order")]
    [Required(ErrorMessage = "This field is required.")]
    public int UnitInOrder { get; set; }

    [Display(Name = "Product Available")]
    [Required(ErrorMessage = "This field is required.")]
    public int ProductAvailable { get; set; }

    [Display(Name = "Short Description")]
    public string? ShortDescription { get; set; }

    [Display(Name = "Long Description")]
    public string? LongDescription { get; set; }

    [Display(Name = "Picture Path 1")]
    [Required(ErrorMessage = "This field is required.")]
    public string? PicturePath1 { get; set; }

    [Display(Name = "Picture Path 2")]
    [Required(ErrorMessage = "This field is required.")]
    public string? PicturePath2 { get; set; }

    [Display(Name = "Picture Path 3")]
    [Required(ErrorMessage = "This field is required.")]
    public string? PicturePath3 { get; set; }

    [Display(Name = "Picture Path 4")]
    [Required(ErrorMessage = "This field is required.")]
    public string? PicturePath4 { get; set; }

    public byte[]? PictureData1 { get; set; }

    public byte[]? PictureData2 { get; set; }

    public byte[]? PictureData3 { get; set; }

    public byte[]? PictureData4 { get; set; }

    [Display(Name = "Product Image Path")]
    [Required(ErrorMessage = "This field is required.")]
    public string ProductImagePath { get; set; } = string.Empty;

    public byte[] ProductImageData { get; set; } = Array.Empty<byte>();

    #endregion
    
    #region View Model Properties

    [Display(Name = "Category Name")]
    public string? CategoryName { get; set; }

    [Display(Name = "SubCategory Name")]
    public string? SubCategoryName { get; set; }

    [Display(Name = "Size Name")]
    public string? SizeName { get; set; }

    #endregion

    #region Select List Properties
    public SelectList? Categories { get; set; }
    public SelectList? SubCategories { get; set; }
    public SelectList? Sizes { get; set; }
    #endregion
}