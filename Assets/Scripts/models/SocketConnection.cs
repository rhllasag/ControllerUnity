
//using Quobject.SocketIoClientDotNet.Client;
using UnityEngine;
public class SocketConnection {
    public static SocketConnection instance = null;
    //private Socket socket = null;
    private string serverURL = "http://192.168.140.110:8080";
    private SocketConnection() {
        //this.socket = IO.Socket(serverURL);
        //this.socket.On(Socket.EVENT_CONNECT, () => {
        //          // Access to Unity UI is not allowed in a background thread, so let's put into a shared variable
        //        //  Debug.Log("Socket.IO connected.");

        //  });
    }
    public static SocketConnection getInstance() {
        if (instance == null)
        {
            instance = new SocketConnection();

        }
        return instance;
    }
    public void emitData(float x, float y)
    {
        Debug.Log("Emiting x:"+x+ " y:"+y);
    }
    //public Socket getSocket() {
    //    return this.socket;
    //}
    //public void setDisconnectSocket() {
    //    if (instance != null)
    //    {
    //        this.socket.Disconnect();
            
    //    }
    //}
    //public void setNullSocket()
    //{
    //    if (instance != null)
    //    {
    //        this.socket = null;

    //    }
    //}
}
