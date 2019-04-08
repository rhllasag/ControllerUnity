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
        texture = new Texture2D(0, 0);
    }
    // Update is called once per frame
    public USStatusTextManager StatusTextManager;

#if !UNITY_EDITOR
    private bool _useUWP = true;
    private Windows.Networking.Sockets.StreamSocket socket;
    private Stream streamIn;
    byte[] bytesImage;
    Coroutine coroutine;
    Coroutine readImageCoroutine;
    Coroutine displayImageCoroutine;
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



#if UNITY_EDITOR
    private void ConnectUWP(string host, string port)
#else
    private async void ConnectUWP(string host, string port)
#endif
    {
#if UNITY_EDITOR
        errorStatus = "UWP TCP client used in Unity!";
#else

        try
        {
            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(host);
            await socket.ConnectAsync(serverHost, port);
            streamIn = socket.InputStream.AsStreamForRead();
            
        }
        catch (Exception e)
        {
            errorStatus = e.ToString();
        }
#endif
    }
#if !UNITY_EDITOR
    private IEnumerator DisplayImage()
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
                            if (read1 == 0|| read1!=SEND_RECEIVE_COUNT)
                            {
                                disconnected = true;
                                RestartExchange();
                                break;
                            }
                            numBytesRead += read1;
                        }
                        catch (Exception e)
                        {
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
                    if (byteLength < 500000&& byteLength>0) {
                        readImageCoroutine = StartCoroutine(ReadImageAsync(byteLength));
                        StopCoroutine(readImageCoroutine);
                        displayImageCoroutine = StartCoroutine(DisplayImageAsync(bytesImage));
                        StopCoroutine(displayImageCoroutine);
                    }
                    else
                    {
                        RestartExchange();
                    }
                    
                }

            }
            catch (Exception e)
            {
            }
            yield return null;
    }

    private IEnumerator DisplayImageAsync(byte[] bytesImage)
    {
        if (texture.LoadImage(bytesImage))
        {
            image.texture = texture;
        }
        yield return null;
    }
#endif
    private bool exchangeStopRequested = false;

    private string errorStatus = null;
    private string warningStatus = null;
    private string successStatus = null;
    private string unknownStatus = null;

    public void RestartExchange()
    {
        try
        {
            StopExchange();
            Connect(DataManager.getInstance().getVideoStreamingServer(), port + "");
        }
        catch (Exception e) {
        }
        
    }
    void Update()
    {
#if !UNITY_EDITOR
        if (DataManager.getInstance().ReadingEnabled) {
            //exchangeStopRequested = false;
            coroutine = StartCoroutine(DisplayImage());
            StopCoroutine(coroutine);
            DataManager.getInstance().ReadingEnabled = false;
        }
#endif
        if (Input.GetKeyDown(KeyCode.M))
        {
            RestartExchange();
        }
        if (DataManager.getInstance().ConnectVideoStreaming)
        {
            StopExchange();
            Connect(DataManager.getInstance().getVideoStreamingServer(), port + "");
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
#if UNITY_EDITOR
        if (client != null)
        {
            client.Close();
            client = null;
        }

#else
        if (displayImageCoroutine != null)
            StopCoroutine(displayImageCoroutine);
        if (readImageCoroutine != null)
            StopCoroutine(readImageCoroutine);
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        if (socket != null) {
            socket.Dispose();
            socket = null;
        }
#endif

    }

    public void OnDestroy()
    {
        StopExchange();
    }

#if !UNITY_EDITOR
    
        private IEnumerator ReadImageAsync(int byteLength)
    {
        bytesImage = new byte[byteLength];
        var imageSize = 0;
        do
        {
            if (streamIn.CanRead)
            {
                try
                {
                    var read = streamIn.Read(bytesImage, imageSize, byteLength);
                    if (read == 0||read!=byteLength)
                    {
                        RestartExchange();
                        break;
                    }
                    imageSize += read;
                }
                catch (Exception e)
                {
                    RestartExchange();
                    break;
                }
            }

        } while (imageSize != byteLength);
        yield return null;
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
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        RestartExchange();
    }
}
