using Accord.Imaging;
using Compare2.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuppeteerSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Image = System.Drawing.Image;

namespace ScreenshotCompare
{
    [TestClass]
    public class ImageCompare
    {
        const string screenshotFilename = "screenshotFile.png";
        private static Task<Page> _setup;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            _setup = WebDriverSetup();

            async Task<Page> WebDriverSetup()
            {
                var page = await WebDriver.Initialize();
                await page.GoToAsync("https://www.w3schools.com/html/tryit.asp?filename=tryhtml5_canvas_tut_grad");
                await page.ScreenshotAsync(screenshotFilename, new ScreenshotOptions { FullPage = true, Type = ScreenshotType.Png });
                return page;
            }

        }

        [TestMethod]
        public async Task Control()
        {
            await _setup;

            const string controlFilename = "control.png";

            var image = Image.FromFile(screenshotFilename);
            var controlImage = Image.FromFile(controlFilename);

            var coeff = image.CompareTo(controlImage);

            Assert.AreEqual(1, coeff);
        }

        
        [TestMethod]
        public async Task Similar1()
        {
            await _setup;

            const string controlFilename = "control modified 1.png";

            var image = Image.FromFile(screenshotFilename);
            var controlImage = Image.FromFile(controlFilename);

            var coeff = image.CompareTo(controlImage);

            Assert.AreEqual(1, coeff);
        }

        
        [TestMethod]
        public async Task Similar2()
        {
            await _setup;

            const string controlFilename = "control modified 2.png";

            var image = Image.FromFile(screenshotFilename);
            var controlImage = Image.FromFile(controlFilename);

            var coeff = image.CompareTo(controlImage);

            Assert.AreEqual(1, coeff);
        }


        [ClassCleanup]
        public static void Cleanup()
        {
        }
    }
}
