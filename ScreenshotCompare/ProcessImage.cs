using Accord.Imaging;
using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotCompare
{
    public static class ProcessImage
    {
        public static void Similarity(System.IO.Stream model, System.IO.Stream observed,
            out float similPercent)
        {
            var bitModel = new Bitmap(model);
            var bitObserved = new Bitmap(observed);

            // For method Difference, see http://www.aforgenet.com/framework/docs/html/673023f7-799a-2ef6-7933-31ef09974dde.htm

            // Inspiration for this process: https://www.youtube.com/watch?v=YHT46f2244E
            // Greyscale class http://www.aforgenet.com/framework/docs/html/d7196dc6-8176-4344-a505-e7ade35c1741.htm
            // Convert model and observed to greyscale
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            // apply the filter to the model
            var greyModel = filter.Apply(bitModel);
            // Apply the filter to the observed image
            var greyObserved = filter.Apply(bitObserved);
            int modelPoints = 0, matchingPoints = 0;

            /*
             * This doesn't work. Images can have different sizes
            // For an example, https://thecsharper.com/?p=94
            // Match
            var tm = new ExhaustiveTemplateMatching(similarityThreshold); 
            // Process the images
            var results = tm.ProcessImage(greyModel, greyObserved);
            */

            using (SpeededUpRobustFeaturesDetector detector = new SpeededUpRobustFeaturesDetector())
            {
                List<SpeededUpRobustFeaturePoint> surfModel = detector.ProcessImage(greyModel);
                modelPoints = surfModel.Count();
                List<SpeededUpRobustFeaturePoint> surfObserved = detector.ProcessImage(greyObserved);

                KNearestNeighborMatching matcher = new KNearestNeighborMatching(5);
                var results = matcher.Match(surfModel, surfObserved);
                matchingPoints = results.Length;
            }
            // Determine if they represent the same points
            // Obtain the pairs of associated points, we determine the homography matching all these pairs


            // Compare the results, 0 indicates no match so return false
            if (matchingPoints <= 0)
            {
                similPercent = 0.0f;
            }

            similPercent = (matchingPoints * 100) / modelPoints;
        }
    }
}
