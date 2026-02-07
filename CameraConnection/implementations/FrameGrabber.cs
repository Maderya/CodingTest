using CameraConnection.interfaces;
using CameraConnection.objects;
using System.Runtime.InteropServices;

namespace CameraConnection.implementations
{
    public class FrameGrabber : IFrameCallback
    {
        private byte[] _buffer;
        public delegate void FrameUpdateHandler(Frame rawFrame);
        public event FrameUpdateHandler OnFrameUpdated;
        public void FrameReceived(IntPtr frame, int width, int height)
        {
            if (_buffer == null)
                _buffer = new byte[width * height];
            // https://stackoverflow.com/questions/5486938/c-sharp-how-to-get-byte-from-intptr
            Marshal.Copy(frame, _buffer, 0, width * height);
            Frame bufferedFrame = new Frame(_buffer);
            OnFrameUpdated(bufferedFrame);
            bufferedFrame.Dispose();
        }
    }
}
