using Accord.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;

namespace Compare2.Extensions
{
    public static class ImageExtensions
    {
        public static Bitmap ConvertToFormat(this Image image, PixelFormat format)
        {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            }
            return copy;
        }

        public static float CompareTo(this Image image, Image other)
        {
            var sourceImage = new Bitmap(image).ConvertToFormat(PixelFormat.Format24bppRgb);
            var templateImage = new Bitmap(other).ConvertToFormat(PixelFormat.Format24bppRgb);

            // create template matching algorithm's instance
            var tm = new ExhaustiveTemplateMatching(0.9f);

            // find all matchings with specified above similarity
            TemplateMatch[] matchings = tm.ProcessImage(sourceImage, templateImage);

            // check similarity level
            return matchings[0].Similarity;
        }
    }
}
