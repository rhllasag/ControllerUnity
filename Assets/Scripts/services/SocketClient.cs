using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

#if !UNITY_EDITOR
using System.Threading.Tasks;
#endif

public class SocketClient: MonoBehaviour
{
#if !UNITY_EDITOR
    private bool _useUWP = true;
    private Windows.Networking.Sockets.StreamSocket socket;
    private Task exchangeTask;
#endif

#if UNITY_EDITOR
    private bool _useUWP = false;
    System.Net.Sockets.TcpClient client;
    System.Net.Sockets.NetworkStream stream;
    private Thread exchangeThread;
#endif
    byte[] bytes = null;
    private StreamWriter writer;
    private StreamReader reader;
    const int SEND_RECEIVE_COUNT = 4;
    void Start()
    {
        Connect("192.168.137.232", "8080");
    }
    public void Connect(string host, string port)
    {
        if (_useUWP)
        {
            Debug.Log("useUWP:"+_useUWP);
            ConnectUWP(host, port);
        }
        else
        {
            Debug.Log("useUWP:" + _useUWP);
            ConnectUnity(host, port);
        }
    }



#if UNITY_EDITOR
    private void ConnectUWP(string host, string port)
#else
    private async void ConnectUWP(string host, string port)
#endif
    {
#if UNITY_EDITOR
        Debug.Log("UWP TCP client used in Unity!");
#else
        try
        {
            if (exchangeTask != null) StopExchange();
        
            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(host);
            await socket.ConnectAsync(serverHost, port);
        
            Stream streamOut = socket.OutputStream.AsStreamForWrite();
            writer = new StreamWriter(streamOut) { AutoFlush = true };
        
            Stream streamIn = socket.InputStream.AsStreamForRead();
            reader = new StreamReader(streamIn);

            RestartExchange();
            successStatus = "Connected!";
        }
        catch (Exception e)
        {
            errorStatus = e.ToString();
        }
#endif
    }

    private void ConnectUnity(string host, string port)
    {
#if !UNITY_EDITOR
        errorStatus = "Unity TCP client used in UWP!";
#else
        try
        {
            if (exchangeThread != null) StopExchange();

            client = new System.Net.Sockets.TcpClient(host, Int32.Parse(port));
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush=true};
            writer.Flush();
            RestartExchange();
            
            Debug.Log("Connected!");
        }
        catch (Exception e)
        {
            Debug.Log("Error:"+ e.ToString());
           
        }
#endif
    }

    private bool exchanging = false;
    private bool exchangeStopRequested = false;
    private string lastPacket = null;
    public void RestartExchange()
    {
#if UNITY_EDITOR
        if (exchangeThread != null) StopExchange();
        exchangeStopRequested = false;
        exchangeThread = new System.Threading.Thread(ExchangePackets);
        exchangeThread.Start();
#else
        if (exchangeTask != null) StopExchange();
        exchangeStopRequested = false;
        exchangeTask = Task.Run(() => ExchangePackets());
#endif
    }

    public void Update()
    {
        if (lastPacket != null)
        {
            ReportDataToTrackingManager(lastPacket);
        }
    }

    public void ExchangePackets()
    {
                
        while (!exchangeStopRequested) 
        {
            if (writer == null || reader == null) continue;
            exchanging = true;
            
            string received = null;

            //writer.Write("X\n");
#if UNITY_EDITOR
            //byte[] msg = new byte[client.SendBufferSize];
            //int count=0;
            //Debug.Log("Size:" + reader.ReadUInt32());
            //while (true)
            //{
            //    count += reader.Read(msg, 0, msg.Length);
            //    received += Encoding.UTF8.GetString(msg, 0, count);
            //    Debug.Log("Count:" + count);
            //    Debug.Log(received.Length);
            //    //if (received.EndsWith("\n"))
            //    //{
            //    //    break;
            //    //}

            //}
            if (stream.CanRead)
            {
                Debug.Log("frameByteArrayToByteLength:" + readImageByteSize(SEND_RECEIVE_COUNT));
            }
            else {
                Debug.Log("Socket Closed");
            }
#else
            received = reader.ReadLine();
#endif

            lastPacket = received;
            
            exchanging = false;


        }
        

    }
    private int readImageByteSize(int size)
    {
        bool disconnected = false;
        byte[] imageBytesCount = new byte[4];
        var total = 0;
        do
        {
            var read = stream.Read(imageBytesCount, total, size - total);
            Debug.LogFormat("Client recieved {0} bytes", total);
            if (read == 0)
            {
                disconnected = true;
                break;
            }
            total += read;
        } while (total != size);

        int byteLength;

        if (disconnected)
        {
            byteLength = -1;
        }
        else
        {
            byteLength = frameByteArrayToByteLength(imageBytesCount);
        }
        return byteLength;
    }
    int frameByteArrayToByteLength(byte[] frameBytesLength)
    {
        int byteLength = BitConverter.ToInt32(frameBytesLength, 0);
        return byteLength;
    }
    private void ReportDataToTrackingManager(string data)
    {
        if (data == null)
        {
            Debug.Log("Received a frame but data was null");
            return;
        }
    }

    

    public void StopExchange()
    {
        exchangeStopRequested = true;
        Debug.Log("StopedRequest");

#if UNITY_EDITOR
        if (exchangeThread != null)
        {
            exchangeThread.Abort();
            stream.Close();
            client.Close();
            writer.Close();
            reader.Close();
            
            stream = null;
            exchangeThread = null;
        }
#else
        if (exchangeTask != null) {
            exchangeTask.Wait();
            socket.Dispose();
            writer.Dispose();
            reader.Dispose();
            socket = null;
            exchangeTask = null;
        }
#endif
        writer = null;
        reader = null;
    }

    public void OnDestroy()
    {
        StopExchange();
    }

}