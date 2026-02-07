using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraConnection.objects
{
    public class Frame : IDisposable
    {
        private bool _disposed;
        private byte[] _rawBuffer;
        public Frame(byte[] raw)
        {
            _rawBuffer = raw;
        }

        public byte[] GetRawData()
        {
            if (_disposed)
                throw new ObjectDisposedException("underlying buffer has changed, should not be used anymore");
            return _rawBuffer;
        }
        public void Dispose()
        {
            _disposed = true;
        }
    }
}
