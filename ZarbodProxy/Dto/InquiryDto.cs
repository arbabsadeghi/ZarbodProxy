namespace ZarbodProxy;

public class InquiryDto
{
    public Guid id { get; set; }
    public string signature { get; set; }
    public string? language { get; set; }
    public string? applicationName { get; set; }
}
