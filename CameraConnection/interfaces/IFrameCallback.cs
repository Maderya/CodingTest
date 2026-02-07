namespace CameraConnection.interfaces
{
    public interface IFrameCallback
    {
        public void FrameReceived(IntPtr pFrame, int pixelWidth, int pixelHeight);
    }
}
