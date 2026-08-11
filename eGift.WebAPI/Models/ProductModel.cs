namespace eGift.WebAPI.Models
{
    public class ProductModel : BaseModel
    {
        #region Data Model Properties
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int? SizeId { get; set; }
        public decimal? Discount { get; set; }
        public int UnitInStock { get; set; }
        public int UnitInOrder { get; set; }
        public int ProductAvailable { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? PicturePath1 { get; set; }
        public string? PicturePath2 { get; set; }
        public string? PicturePath3 { get; set; }
        public string? PicturePath4 { get; set; }
        public byte[]? PictureData1 { get; set; }
        public byte[]? PictureData2 { get; set; }
        public byte[]? PictureData3 { get; set; }
        public byte[]? PictureData4 { get; set; }
        public string ProductImagePath { get; set; } = string.Empty; 
        public byte[] ProductImageData { get; set; } = Array.Empty<byte>();

        #endregion
    }
}