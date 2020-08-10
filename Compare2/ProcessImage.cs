using Accord.Imaging;
using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compare2
{
    public static class ProcessImage
    {
        public static void Similarity(System.IO.Stream model, System.IO.Stream observed,
            out double similPercent)
        {
            var bitModel = new Bitmap(model);
            var bitObserved = new Bitmap(observed);

            int modelPoints = 0, matchingPoints = 0;
            
            Accord.Math.Random.Generator.Seed = 0;

            using (SpeededUpRobustFeaturesDetector detector = new SpeededUpRobustFeaturesDetector(threshold: 0.0002f, octaves: 5, initial: 2))
            {
                List<SpeededUpRobustFeaturePoint> surfModel = detector.ProcessImage(bitModel);
                modelPoints = surfModel.Count();
                List<SpeededUpRobustFeaturePoint> surfObserved = detector.ProcessImage(bitObserved);

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

            similPercent = ((double)matchingPoints * 100) / (double)modelPoints;
        }
    }
}
