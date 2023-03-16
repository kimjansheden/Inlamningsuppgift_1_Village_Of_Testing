using System.Text.Encodings.Web;
using System.Text.Unicode;
using Ganss.Xss;

namespace GameLib;

public class WebUIHelper
{
    public string Message = "";
    public string UserInput = "";
    public event EventHandler MessageUpdated;
    
    // These are currently not used because I didn't get it to work, but ideally they should be.
    private HtmlEncoder _htmlEncoder;
    private HtmlSanitizer _htmlSanitizer;

    public WebUIHelper()
    {
        _htmlEncoder = HtmlEncoder.Create(GetTextEncoderSettings());
        _htmlSanitizer = new HtmlSanitizer(GetHtmlSanitizerOptions());
        
    }
    
    private TextEncoderSettings GetTextEncoderSettings()
    {
        var allowedRanges = new UnicodeRange[]
        {
            UnicodeRanges.BasicLatin,
            UnicodeRanges.Latin1Supplement
        };

        var settings = new TextEncoderSettings();
        foreach (var range in allowedRanges)
        {
            settings.AllowRange(range);
        }

        return settings;
    }
    private HtmlSanitizerOptions GetHtmlSanitizerOptions()
    {
        var options = new HtmlSanitizerOptions();
        options.AllowedTags = new HashSet<string> { "br" };
        return options;
    }

    public void SetMessage(string message)
    {
        Message += message;
        MessageUpdated?.Invoke(this, EventArgs.Empty);
    }
    public void ClearMessage()
    {
        Message = "";
        MessageUpdated?.Invoke(this, EventArgs.Empty);
    }
}