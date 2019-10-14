using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace CVTest1
{
    public class VideoCaptureSamples
    {
        private bool captureFlag = false;

        public void Run()
        {
            // Opens MP4 File
            var capture = new VideoCapture(0);
            double fps = capture.Fps;
            int sleepTime = (int)Math.Round(1000 / capture.Fps) > 0 ? (int)Math.Round(1000 / capture.Fps): 100;

            using (var window = new Window("capture"))
            {
                // Frame Image Buffer
                Mat image = new Mat();
                this.captureFlag = true;
                //When the movie playback reaches end,  Mat.data becomes Null.
                while (captureFlag)
                {
                    capture.Read(image); // 撮像
                    if (image.Empty())
                    {
                        break;
                    }
                    window.ShowImage(image);
                    Cv2.WaitKey(sleepTime);
                }
            }
        }

        public void Stop()
        {
            this.captureFlag = false;
        }

        // 録画もしたいよね

    }
}
