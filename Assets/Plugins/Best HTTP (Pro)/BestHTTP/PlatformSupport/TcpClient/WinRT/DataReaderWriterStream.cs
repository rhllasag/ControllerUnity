#if (UNITY_WSA || BUILD_FOR_WP8) && !UNITY_EDITOR

using System;
using Windows.Storage.Streams;
using Windows.Networking.Sockets;
using Windows.Networking;

namespace BestHTTP.PlatformSupport.TcpClient.WinRT
{
public sealed class TcpClient : IDisposable
    {
#region Public Properties

        public bool Connected { get; private set; }
        public TimeSpan ConnectTimeout { get; set; }

        public bool UseHTTPSProtocol { get; set; }

#endregion

#region Private Properties

        internal StreamSocket Socket { get; set; }
        private System.IO.Stream Stream { get; set; }

#endregion

        public TcpClient()
        {
            ConnectTimeout = TimeSpan.FromSeconds(2);
        }

        public void Connect(string hostName, int port)
        {
            //How to secure socket connections with TLS/SSL:
            //http://msdn.microsoft.com/en-us/library/windows/apps/jj150597.aspx

            //Networking in Windows 8 Apps - Using StreamSocket for TCP Communication
            //http://blogs.msdn.com/b/metulev/archive/2012/10/22/networking-in-windows-8-apps-using-streamsocket-for-tcp-communication.aspx

            Socket = new StreamSocket();
            Socket.Control.KeepAlive = true;

            var host = new HostName(hostName);

            SocketProtectionLevel spl = SocketProtectionLevel.PlainSocket;
            if (UseHTTPSProtocol)
                spl = SocketProtectionLevel.
#if UNITY_WSA_8_0 || BUILD_FOR_WP8
                        Ssl;
#else
                        Tls12;
#endif

            // https://msdn.microsoft.com/en-us/library/windows/apps/xaml/jj710176.aspx#content
            try
            {
                var result = Socket.ConnectAsync(host, UseHTTPSProtocol ? "https" : port.ToString(), spl);
                Connected = result.AsTask().Wait(ConnectTimeout);
            }
            catch(AggregateException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw ex;
            }

            if (!Connected)
                throw new TimeoutException("Connection timed out!");
        }

        public bool IsConnected()
        {
            return true;
        }

        public System.IO.Stream GetStream()
        {
            if (Stream == null)
                Stream = new DataReaderWriterStream(this);
            return Stream;
        }

        public void Close()
        {
            Dispose();
        }

#region IDisposeble

        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Stream != null)
                        Stream.Dispose();
                    Stream = null;
                    Connected = false;
                }
                disposed = true;
            }
        }

        ~TcpClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

#endregion
    }
    public sealed class DataReaderWriterStream : System.IO.Stream
    {
        private TcpClient Client { get; set; }
        private DataReader Reader { get; set; }
        private DataWriter Writer { get; set; }

        public DataReaderWriterStream(TcpClient socket)
        {
            this.Client = socket;
            this.Reader = new DataReader(Client.Socket.InputStream);
            this.Writer = new DataWriter(Client.Socket.OutputStream);
        }

#region Stream interface

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            Writer.StoreAsync().AsTask().Wait();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool DataAvailable
        {
            get
            {
                return Reader.UnconsumedBufferLength > 0;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            Windows.Storage.Streams.Buffer tmpBuffer = new Windows.Storage.Streams.Buffer((uint)count);

            try
            {
                var task = Client.Socket.InputStream.ReadAsync(tmpBuffer, (uint)count, InputStreamOptions.None);
                task.AsTask().Wait();
            }
            catch(AggregateException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw ex;
            }

            /*byte[] tmpBuff = tmpBuffer.ToArray();
            int length = Math.Min(tmpBuff.Length, count);

            Array.Copy(tmpBuff, 0, buffer, offset, length);

            return length;*/
            
            DataReader buf = DataReader.FromBuffer(tmpBuffer);
            int length = (int)buf.UnconsumedBufferLength;
            for (int i = 0; i < length; ++i)
                buffer[offset + i] = buf.ReadByte();
            return length;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            for (int i = 0; i < count; ++i)
                Writer.WriteByte(buffer[offset + i]);
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

#endregion

#region Dispose

        private bool disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Reader != null)
                    {
                        Reader.Dispose();
                        Reader = null;
                    }

                    if (Writer != null)
                    {
                        Writer.Dispose();
                        Writer = null;
                    }
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }

#endregion
    }
}

#endif