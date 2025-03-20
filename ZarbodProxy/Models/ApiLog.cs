namespace ZarbodProxy
{
    public class ApiLog
    {
        public int Id { get; set; }
        public string ApiName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
