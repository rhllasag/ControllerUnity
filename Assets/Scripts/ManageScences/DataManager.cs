using UnityEngine;

/// <summary>Manages data for persistance between levels.</summary>
/// 
public class DataManager
{
    /// <summary>Static reference to the instance of our DataManager</summary>
    public static DataManager instance;
    public string videoStreamingServer= "192.168.1.4";
    public string webSocketServer= "192.168.1.6";
    private DataManager() {
    }
    public void setVideoStreamingServer(string ip) {
        videoStreamingServer = ip;
    }
    public void setWebSocketServer(string ip)
    {
        webSocketServer = ip;
    }
    public string getVideoStreamingServer() {
        return videoStreamingServer;
    }
    public string getWebSocketServer()
    {
        return webSocketServer;
    }
    public static DataManager getInstance() {
        if (instance == null)
        {
            instance = new DataManager();
        }
            return instance;
    }
    /// <summary>Awake is called when the script instance is being loaded.</summary>
}
