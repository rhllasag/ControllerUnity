using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public GameObject flightSwitchMode;
        //The Object to Landing, TakeOff, Landing
        public GameObject landing;
        public GameObject takeOff;
        public GameObject rth;
        //BatteryNeeded to RTH
        public GameObject batteryNAircraft;
        //Flight Time
        public GameObject flighTime;
        void Start()
        {
            //Create MeshObject that can observe events and give them an event to do
            MeshObject batteryLevelMesh = new MeshObject(battery, new BatteryLevel() );
            MeshObject remoteControllerMesh = new MeshObject(remoteController, new RCControl());
            MeshObject gpsSignalStateMesh = new MeshObject(gpsSignalState, new GPSSignal());
            MeshObject visualNavigationStateMesh = new MeshObject(visualNavigationState, new VisualNavigation());
            MeshObject systemStatusMesh = new MeshObject(systemSatus, new SystemStatus());
            MeshObject flightSwitchModeMesh = new MeshObject(landing,takeOff,rth,flightSwitchMode, new FlightSwitchMode());
            MeshObject batteryNAircraftMesh = new MeshObject(batteryNAircraft, new BatteryARTH());
            MeshObject flightTimeMesh = new MeshObject(flighTime, new FlightTime());
            //Add the MeshObject to the list of objects waiting for something to happen
            SocketConnection.getInstance().AddObserver(batteryLevelMesh);
            SocketConnection.getInstance().AddObserver(remoteControllerMesh);
            SocketConnection.getInstance().AddObserver(gpsSignalStateMesh);
            SocketConnection.getInstance().AddObserver(visualNavigationStateMesh);
            SocketConnection.getInstance().AddObserver(systemStatusMesh);
            SocketConnection.getInstance().AddObserver(flightSwitchModeMesh);
            SocketConnection.getInstance().AddObserver(batteryNAircraftMesh);
            SocketConnection.getInstance().AddObserver(flightTimeMesh);
        }
        


        void Update()
        {
            //The boxes should jump if the sphere is cose to origo
            //if ((sphereObj.transform.position).magnitude < 0.5f)
            //{
              //  subject.Notify();
            //}
        }
    }
}