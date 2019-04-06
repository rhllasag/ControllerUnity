using HoloToolkit.Unity.InputModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if !UNITY_EDITOR
using System.Threading.Tasks;
#endif

public class VideoStreaming : MonoBehaviour, IInputClickHandler
{
    RawImage image;
    bool enableLog = false;
    string IP = DataManager.getInstance().getVideoStreamingServer();
    int port = DataManager.getInstance().PortVideoStreaming;
    Texture2D texture;
    //This must be the-same with SEND_COUNT on the server
    const int SEND_RECEIVE_COUNT = 4;
    // Use this for initialization
    int byteLength;
    void Start()
    {
        image = GameObject.Find("RawImage").GetComponent<RawImage>();
    }
    // Update is called once per frame
    public USStatusTextManager StatusTextManager;

#if !UNITY_EDITOR
    private bool _useUWP = true;
    private Windows.Networking.Sockets.StreamSocket socket;
    private Task exchangeTask;
    private Task loadImage;
    private Task readImage;
    private Stream streamIn;
    byte[] bytesImage;
    private bool reconnect = false;
    Coroutine coroutine;
#endif

#if UNITY_EDITOR
    private bool _useUWP = false;
    System.Net.Sockets.TcpClient client;
    System.Net.Sockets.NetworkStream stream;
#endif

    private Byte[] bytes = new Byte[256];
    public void Connect(string host, string port)
    {
        if (_useUWP)
        {
            ConnectUWP(host, port);
        }
        else
        {
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
        errorStatus = "UWP TCP client used in Unity!";
#else

        texture = new Texture2D(0, 0);
        try
        {
            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(host);
            await socket.ConnectAsync(serverHost, port);

            streamIn = socket.InputStream.AsStreamForRead();
            successStatus = "Connected!";
            exchangeStopRequested = false;
            coroutine = StartCoroutine(DisplayImage());
            
        }
        catch (Exception e)
        {
            errorStatus = e.ToString();
        }
#endif
    }

    private IEnumerator DisplayImage()
    {
        while (!exchangeStopRequested)
        {
            try
            {

                bool disconnected = false;
                byte[] bytes = new byte[SEND_RECEIVE_COUNT];
                var numBytesRead = 0;
                do
                {
                    if (streamIn.CanRead)
                    {
                        try
                        {
                            var read1 = streamIn.Read(bytes, numBytesRead, SEND_RECEIVE_COUNT - numBytesRead);
                            if (read1 == 0)
                            {
                                disconnected = true;
                                break;
                            }
                            numBytesRead += read1;
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("Reading Error:" + e);
                            StopExchange();
                            break;
                        }
                    }
                } while (numBytesRead != SEND_RECEIVE_COUNT);
                if (disconnected)
                {
                    byteLength = -1;
                }
                else
                {
                    byteLength = frameByteArrayToByteLength(bytes);
                    if(byteLength<500000)
                    ReadImageAsync(byteLength);
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Size Out of range ");
                        reconnect = true;
                        break;
                    }
                    
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Task Error:" + e);
                exchangeStopRequested = true;
                reconnect = true;
                break;
            }
            yield return null;
        }
        yield return null;
        StopCoroutine(coroutine);
    }
    private void ConnectUnity(string host, string port)
    {
#if !UNITY_EDITOR
        errorStatus = "Unity TCP client used in UWP!";
#else
        Application.runInBackground = true;
        texture = new Texture2D(0, 0);
        client = new System.Net.Sockets.TcpClient(host, Int32.Parse(port));
        Loom.RunAsync(() =>
        {
            exchangeStopRequested = false;
            imageReceiver();
        });
#endif
    }

    private bool exchangeStopRequested = false;

    private string errorStatus = null;
    private string warningStatus = null;
    private string successStatus = null;
    private string unknownStatus = null;

    public void RestartExchange()
    {
        StopExchange();
        Connect(DataManager.getInstance().getVideoStreamingServer(), port + "");
    }

    void Update()
    {
        if (reconnect) {
            RestartExchange();
            reconnect = false;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            RestartExchange();
        }
        if (DataManager.getInstance().ConnectVideoStreaming)
        {
            RestartExchange();
            DataManager.getInstance().ConnectVideoStreaming = false;
        }
        if (DataManager.getInstance().DisconnectVideoStraming)
        {
            StopExchange();
            DataManager.getInstance().DisconnectVideoStraming = false;
        }
        if (errorStatus != null)
        {
#if UNITY_EDITOR
            Debug.Log(errorStatus);
#endif
            errorStatus = null;
        }
        if (warningStatus != null)
        {
#if UNITY_EDITOR
            Debug.Log(warningStatus);
#endif
            warningStatus = null;
        }
        if (successStatus != null)
        {
#if UNITY_EDITOR
            Debug.Log(successStatus);
#endif
            successStatus = null;
        }
        if (unknownStatus != null)
        {
#if UNITY_EDITOR
            Debug.Log(unknownStatus);
#endif
            unknownStatus = null;
        }
    }
    int frameByteArrayToByteLength(byte[] frameBytesLength)
    {
        int byteLength = BitConverter.ToInt32(frameBytesLength, 0);
        return byteLength;
    }
    public void StopExchange()
    {
        exchangeStopRequested = true;
        if(coroutine!=null)
        StopCoroutine(coroutine);
#if UNITY_EDITOR
        if (client != null)
        {
            client.Close();
            client = null;
        }

#else
        if (loadImage != null)
        {
            loadImage = null;
        }
        if (readImage != null)
        {
            readImage = null;
        }
        if (exchangeTask != null)
        {
            socket.Dispose();
            socket = null;
            exchangeTask = null;
        }
#endif

    }

    public void OnDestroy()
    {
        StopExchange();
    }

#if !UNITY_EDITOR
    private void ReadImageAsync(int byteLength)
    {
        System.Diagnostics.Debug.WriteLine("Client recieved :" + byteLength);
        bytesImage = new byte[byteLength];
        var imageSize = 0;
        do
        {
            if (streamIn.CanRead)
            {
                try
                {
                    var read = streamIn.Read(bytesImage, imageSize, byteLength - imageSize);
                    System.Diagnostics.Debug.WriteLine("Client recieved bytes:" + imageSize);
                    if (read == 0)
                    {
                        break;
                    }
                    imageSize += read;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Reading Image Error:" + e);
                    StopExchange();
                    break;
                }
            }

        } while (imageSize != byteLength);
        System.Diagnostics.Debug.WriteLine("Ready to render");
        displayReceivedImage(bytesImage);
    }
#endif
























#if UNITY_EDITOR
    void imageReceiver()
    {
        //While loop in another Thread is fine so we don't block main Unity Thread
        Loom.RunAsync(() =>
        {
            while (!exchangeStopRequested)
            {
                //Read Image Count
                int imageSize = readImageByteSize(SEND_RECEIVE_COUNT);
                //LOGWARNING("Received Image byte Length: " + imageSize);

                //Read Image Bytes and Display it
                readFrameByteArray(imageSize);

            }
        });
    }
    private int readImageByteSize(int size)
    {
        bool disconnected = false;

        NetworkStream serverStream = client.GetStream();
        byte[] imageBytesCount = new byte[size];
        var total = 0;
        do
        {
            var read = serverStream.Read(imageBytesCount, total, size - total);
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
    private void readFrameByteArray(int size)
    {
        bool disconnected = false;

        NetworkStream serverStream = client.GetStream();
        byte[] imageBytes = new byte[size];
        var total = 0;
        do
        {
            var read = serverStream.Read(imageBytes, total, size - total);
            Debug.LogFormat("Client recieved {0} bytes", total);
            if (read == 0)
            {
                disconnected = true;
                break;
            }
            total += read;
        } while (total != size);

        bool readyToReadAgain = false;

        //Display Image
        if (!disconnected)
        {
            //Display Image on the main Thread
            Loom.QueueOnMainThread(() =>
            {
                displayReceivedImage(imageBytes);
                readyToReadAgain = true;
            });
        }

        //Wait until old Image is displayed
        while (!readyToReadAgain)
        {
            System.Threading.Thread.Sleep(0);
        }
    }

#endif

    void displayReceivedImage(byte[] receivedImageBytes)
    {
        if (texture.LoadImage(receivedImageBytes))
        {
            image.texture = texture;
        }
#if !UNITY_EDITOR
        System.Diagnostics.Debug.WriteLine("Show image");
#endif
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        RestartExchange();
    }
}
