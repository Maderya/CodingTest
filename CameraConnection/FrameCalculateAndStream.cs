using CameraConnection.implementations;
using CameraConnection.interfaces;
using CameraConnection.objects;
using System.Timers;

namespace CameraConnection
{
    public class FrameCalculateAndStream
    {
        private IValueReporter _reporter;
        private Queue<Frame> _receivedFrames = new Queue<Frame>();
        private System.Timers.Timer _timer;
        private string _filePath, _fileName;

        public FrameCalculateAndStream(FrameGrabber fg, IValueReporter vr, string filePath, string fileName)
        {
            fg.OnFrameUpdated += HandleFrameUpdated;
            _timer = new System.Timers.Timer(1000 / 30);
            StartStreaming(); // Call StartStreaming() here
            _timer.Elapsed += OnTimerElapsed;
            _reporter = vr;
            _filePath = filePath;
            _fileName = fileName;

            fg.OnFrameUpdated -= HandleFrameUpdated; // unsubscribe
            _timer.Elapsed -= OnTimerElapsed; // unsubscribe
        }

        private void HandleFrameUpdated(Frame frame)
        {
            _receivedFrames.Enqueue(frame);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_receivedFrames.Count > 0)
            {
                Frame frame = _receivedFrames.Dequeue();
                byte[] raw = frame.GetRawData();
                // https://stackoverflow.com/questions/29312223/finding-the-arithmetic-mean-of-an-array-c-sharp
                int sum = 0;
                for (int i = 0; i < raw.Length; i++)
                    sum += raw[i];
                int result = sum / raw.Length; // result now has the average of those numbers.
                _reporter.Report(result, _filePath, _fileName);
            }
        }
        public void StartStreaming()
        {
            _timer.Enabled = true;
        }
    }
}
