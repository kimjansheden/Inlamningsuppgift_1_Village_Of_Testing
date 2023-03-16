namespace BlazorApp;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Set the content root path from command line arguments, if provided.
        var contentRootArg = args.FirstOrDefault(arg => arg.StartsWith("--content-root"));
        if (contentRootArg != null)
        {
            var contentRootSplit = contentRootArg.Split('=');
            if (contentRootSplit.Length > 1)
            {
                var contentRootPath = contentRootSplit[1].Trim('"');
                if (!string.IsNullOrEmpty(contentRootPath))
                {
                    builder.Environment.ContentRootPath = contentRootPath;
                }
            }
        }

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
