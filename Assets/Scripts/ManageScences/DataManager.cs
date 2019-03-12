using UnityEngine;

/// <summary>Manages data for persistance between levels.</summary>
/// 
public class DataManager
{
    /// <summary>Static reference to the instance of our DataManager</summary>
    public static DataManager instance;
    private string videoStreamingServer= "10.254.0.221";
    private string webSocketServer= "192.168.1.4";
    private bool intelligentFLightModes;
    private bool beginnerMode;
    private bool distanceLimit;
    private int altitudeRTH;
    private float altitudeWaypoints;
    private int maximumAltitude;
    private int maximumFlightDistance;
    private float speedWaypoints;
    private int rTHBatteryError;
    private int rTHBatteryWarning;
    public bool IntelligentFLightModes
    {
        get
        {
            return intelligentFLightModes;
        }

        set
        {
            intelligentFLightModes = value;
        }
    }

    public bool BeginnerMode
    {
        get
        {
            return beginnerMode;
        }

        set
        {
            beginnerMode = value;
        }
    }

    public bool DistanceLimit
    {
        get
        {
            return distanceLimit;
        }

        set
        {
            distanceLimit = value;
        }
    }

    public int AltitudeRTH
    {
        get
        {
            return altitudeRTH;
        }

        set
        {
            altitudeRTH = value;
        }
    }
    public float AltitudeWaypoints
    {
        get
        {
            return altitudeWaypoints;
        }

        set
        {
            altitudeWaypoints = value;
        }
    }
    public float SpeedWaypoints
    {
        get
        {
            return speedWaypoints;
        }

        set
        {
            speedWaypoints = value;
        }
    }
    public int RTHBatteryWarning
    {
        get
        {
            return rTHBatteryWarning;
        }

        set
        {
            rTHBatteryWarning = value;
        }
    }
    public int RTHBatteryError
    {
        get
        {
            return rTHBatteryError;
        }

        set
        {
            rTHBatteryError = value;
        }
    }
    public int MaximumAltitude
    {
        get
        {
            return maximumAltitude;
        }

        set
        {
            maximumAltitude = value;
        }
    }

    public int MaximumFlightDistance
    {
        get
        {
            return maximumFlightDistance;
        }

        set
        {
            maximumFlightDistance = value;
        }
    }

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
