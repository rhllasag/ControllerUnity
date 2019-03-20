using HoloToolkit.Examples.InteractiveElements;
using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialManager : MonoBehaviour {
    public GameObject gameObject;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetRadial(string message)
    {
        if (message.CompareTo("none") == 0) {
            SocketConnection.getInstance().emitData("newActionAfterMission", "none");
        }
        if (message.CompareTo("auto_land") == 0)
        {
            SocketConnection.getInstance().emitData("newActionAfterMission", "auto_land");
        }
        if (message.CompareTo("go_home") == 0)
        {
            SocketConnection.getInstance().emitData("newActionAfterMission", "go_home");

        }
        if (message.CompareTo("back_to_1st") == 0)
        {
            SocketConnection.getInstance().emitData("newActionAfterMission", "back_to_1st");

        }
        if (message.CompareTo("auto") == 0)
        {
            SocketConnection.getInstance().emitData("newHeading", "auto");

        }
        if (message.CompareTo("initial") == 0)
        {
            SocketConnection.getInstance().emitData("newHeading", "initial");

        }
        if (message.CompareTo("rc_control") == 0)
        {
            SocketConnection.getInstance().emitData("newHeading", "rc_control");

        }
        if (message.CompareTo("waypoints") == 0)
        {
            SocketConnection.getInstance().emitData("newHeading", "waypoints");

        }
    }
}
