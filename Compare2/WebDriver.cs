using PuppeteerSharp;
using System.Threading.Tasks;

namespace ScreenshotCompare
{
    public static class WebDriver
    {
        public static async Task<Page> Initialize()
        {
            BrowserFetcher browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions() { Headless = false, Args = new string[] { "--no-sandbox" } });
            var page = await browser.NewPageAsync();
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1920,
                Height = 1080
            });
            return page;
        }

    }
}
