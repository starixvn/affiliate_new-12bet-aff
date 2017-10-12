namespace Fanex.Affiliate.DataService.Importer.DTO
{
    public class ResultInfo
    {
        public bool IsSuccessful
        {
            get { return string.IsNullOrEmpty(Message) || (Code == 0); }
        }

        public int Code { get; set; }

        public string Message { get; set; }
    }
}