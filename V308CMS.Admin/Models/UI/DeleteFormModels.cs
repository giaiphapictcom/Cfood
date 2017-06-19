namespace V308CMS.Admin.Models.UI
{
    public class DeleteFormModels
    {
        public DeleteFormModels()
        {
            Url = "";            
            NameOfId = "id";
            IsConfirm = true;
        }
        public string Url { get; set; }
        public int ValueOfId { get; set; }
        public string NameOfId { get; set; }
        public  bool IsConfirm { get; set; }
    }
}