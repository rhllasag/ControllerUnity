﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System;
namespace ObserverPattern
{
    public class VideoReciver : Observer
    {
        RawImage image;
        bool enableLog = false;
        const int port = 8080;
        string IP = DataManager.getInstance().getVideoStreamingServer();
        TcpClient client;
        GameObject Video_Server;
        Texture2D tex;
        private bool stop = false;
        //This must be the-same with SEND_COUNT on the server
        const int SEND_RECEIVE_COUNT = 4;

        public VideoReciver(bool enableLog)
        {
            this.image = (RawImage)GameObject.Find("RawImage").GetComponent<RawImage>();
            this.enableLog = enableLog;
            Debug.Log("IP:"+IP);
        }


        public override void OnNotify(string data, string component)
        {
            if (component.CompareTo("connectSocket") == 0)
            {
                Debug.Log("Connect Video");
                connect();
            }
            if (component.CompareTo("disconnectSocket") == 0)
            {
                Debug.Log("Disconnect Video");
                disconnect();
            }
        }
        // Use this for initialization
        void imageReceiver()
        {
            //While loop in another Thread is fine so we don't block main Unity Thread
            Loom.RunAsync(() =>
            {
                while (!stop)
                {
                //Read Image Count
                int imageSize = readImageByteSize(SEND_RECEIVE_COUNT);
                //LOGWARNING("Received Image byte Length: " + imageSize);

                //Read Image Bytes and Display it
                readFrameByteArray(imageSize);

                }
            });
        }


        //Converts the data size to byte array and put result to the fullBytes array
        void byteLengthToFrameByteArray(int byteLength, byte[] fullBytes)
        {
            //Clear old data
            Array.Clear(fullBytes, 0, fullBytes.Length);
            //Convert int to bytes
            byte[] bytesToSendCount = BitConverter.GetBytes(byteLength);
            //Copy result to fullBytes
            bytesToSendCount.CopyTo(fullBytes, 0);
        }

        //Converts the byte array to the data size and returns the result
        int frameByteArrayToByteLength(byte[] frameBytesLength)
        {
            int byteLength = BitConverter.ToInt32(frameBytesLength, 0);
            return byteLength;
        }


        /////////////////////////////////////////////////////Read Image SIZE from Server///////////////////////////////////////////////////
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

        /////////////////////////////////////////////////////Read Image Data Byte Array from Server///////////////////////////////////////////////////
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


        void displayReceivedImage(byte[] receivedImageBytes)
        {

            if (tex.LoadImage(receivedImageBytes))
            {
                image.texture = tex;
            }
        }

        void LOG(string messsage)
        {
            if (enableLog)
                Debug.Log(messsage);
        }

        void LOGWARNING(string messsage)
        {
            if (enableLog)
                Debug.LogWarning(messsage);
        }



        public void connect()
        {
            Application.runInBackground = true;

            tex = new Texture2D(0, 0);
            client = new System.Net.Sockets.TcpClient(IP, Int32.Parse(port + ""));

            //Connect to server from another Thread
            Loom.RunAsync(() =>
            {
                LOGWARNING("Connecting to server...");
            // if on desktop
            //client.Connect(IPAddress.Loopback, port);

            // if using the IPAD
            //client.Connect(IPAddress.Parse(IP), port);
            LOGWARNING("Connected!");
                stop = false;
                imageReceiver();
            });
        }
        public void disconnect()
        {
            LOGWARNING("OnApplicationQuit");
            stop = true;

            if (client != null)
            {
                client.Close();
                Debug.Log("Connected :" + client.Connected);
            }
        }
    }
}
