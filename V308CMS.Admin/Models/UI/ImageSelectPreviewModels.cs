namespace V308CMS.Admin.Models.UI
{
    public class ImageSelectPreviewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlHiddenName { get; set; }
        public bool AutoGenerateLabel { get; set; }

        public ImageSelectPreviewModels()
        {
            AutoGenerateLabel = true;
        }
    }
}