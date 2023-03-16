namespace GameLib;

public class WebUI : IUI
{
    private WebUIHelper _helper;
    
    public WebUI(WebUIHelper helper)
    {
        _helper = helper;
    }
    public void WriteLine(string message)
    {
        try
        {
            _helper.SetMessage(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string ReadLine()
    {
        // Wait for the user to start typing.
        while (_helper.UserInput == "")
        {
            Thread.Sleep(1000);
        }

        // Set the user input to a new variable and clear the old one.
        var userInput = _helper.UserInput;
        _helper.UserInput = "";
        
        return userInput;
    }

    public void Clear()
    {
        try
        {
            _helper.ClearMessage();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}