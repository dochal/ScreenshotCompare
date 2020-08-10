using Accord.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuppeteerSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Image = System.Drawing.Image;
namespace Compare2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            //            BrowserFetcher browserFetcher = new BrowserFetcher();
            //await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
            //var browser = await Puppeteer.LaunchAsync(new LaunchOptions() { Headless = false, Args = new string[] { "--no-sandbox" } });
            //var page = await browser.NewPageAsync();
            //await page.SetViewportAsync(new ViewPortOptions
            //{
            //    Width = 1920,
            //    Height = 1080
            //});
            //await page.GoToAsync("https://www.w3schools.com/html/tryit.asp?filename=tryhtml5_canvas_tut_grad");
            
            //await page.ScreenshotAsync("test1.png", new ScreenshotOptions { FullPage = true, Type = ScreenshotType.Png });

            var sourceImage = new Bitmap(Image.FromFile("test1.png"));
            var templateImage = new Bitmap(Image.FromFile("test1.png"));

            // create template matching algorithm's instance
            var tm = new ExhaustiveTemplateMatching(0.9f);

            // find all matchings with specified above similarity
            TemplateMatch[] matchings = tm.ProcessImage(sourceImage, templateImage);

            // highlight found matchings
            BitmapData data = sourceImage.LockBits(ImageLockMode.ReadWrite);

            foreach (TemplateMatch m in matchings)
            {
                Drawing.Rectangle(data, m.Rectangle, Color.White);

                // do something else with the matching
            }

            sourceImage.UnlockBits(data);

            // check similarity level
            if (matchings[0].Similarity > 0.95f)
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
