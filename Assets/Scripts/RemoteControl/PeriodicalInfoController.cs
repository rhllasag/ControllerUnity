using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using HoloToolkit.Unity.InputModule;

namespace ObserverPattern
{
    public class PeriodicalInfoController : MonoBehaviour
    {
        public GameObject battery;
        //The Object that will jump
        public GameObject remoteController;
        public GameObject gpsSignalState;
        public GameObject visualNavigationState;
        public GameObject systemSatus;
        public GameObject statusPanel;
        public GameObject flightSwitchMode;
        //The Object to Landing, TakeOff, Landing
        public GameObject landing;
        public GameObject takeOff;
        public GameObject rth;
        public GameObject map;
        public GameObject zoomMap;
        public GameObject locationHome;
        public GameObject locationAircraft;
        //BatteryNeeded to RTH
        public GameObject batteryNAircraft;
        //Flight Data
        public GameObject flighTime;
        public GameObject hight;
        public GameObject homeLocation;
        public GameObject coordinates;

        public bool enableLog = false;
        //Panels
        public GameObject joystickPanel;
        void Start()
        {
            //Create MeshObject that can observe events and give them an event to do
            MeshObject batteryLevelMesh = new MeshObject(battery, new BatteryLevel() );
            MeshObject remoteControllerMesh = new MeshObject(remoteController, new RCControl());
            MeshObject gpsSignalStateMesh = new MeshObject(gpsSignalState, new GPSSignal());
            MeshObject visualNavigationStateMesh = new MeshObject(visualNavigationState, new VisualNavigation());
            MeshObject systemStatusMesh = new MeshObject(systemSatus,statusPanel, new SystemStatus());
            MeshObject flightSwitchModeMesh = new MeshObject(landing,takeOff,rth,flightSwitchMode, new FlightSwitchMode());
            MeshObject batteryNAircraftMesh = new MeshObject(batteryNAircraft, new BatteryARTH());
            MeshObject flightTimeMesh = new MeshObject(flighTime, new FlightTime());
            MeshObject hightMesh = new MeshObject(hight, new Hight());
            MeshObject homeLocationMesh = new MeshObject(homeLocation,map,zoomMap, locationHome, locationAircraft, new HomeLocation());
            MeshObject coodinatesMesh = new MeshObject(coordinates, map, zoomMap, locationHome, locationAircraft, new Coordinates());
            MeshObject joystickPanelMesh = new MeshObject(joystickPanel, new JoyStickPanel());
            MeshObject videoReconnect = new MeshObject(new VideoReconnection());
            //Add the MeshObject to the list of objects waiting for something to happen
            SocketConnection.getInstance().AddObserver(batteryLevelMesh);
            SocketConnection.getInstance().AddObserver(remoteControllerMesh);
            SocketConnection.getInstance().AddObserver(gpsSignalStateMesh);
            SocketConnection.getInstance().AddObserver(visualNavigationStateMesh);
            SocketConnection.getInstance().AddObserver(systemStatusMesh);
            SocketConnection.getInstance().AddObserver(flightSwitchModeMesh);
            SocketConnection.getInstance().AddObserver(batteryNAircraftMesh);
            SocketConnection.getInstance().AddObserver(flightTimeMesh);
            SocketConnection.getInstance().AddObserver(hightMesh);
            SocketConnection.getInstance().AddObserver(homeLocationMesh);
            SocketConnection.getInstance().AddObserver(coodinatesMesh);
            SocketConnection.getInstance().AddObserver(joystickPanelMesh);
            SocketConnection.getInstance().AddObserver(videoReconnect);
        }
        void Update()
        {
            
        }
        void OnApplicationQuit()
        {
        }


    }

}