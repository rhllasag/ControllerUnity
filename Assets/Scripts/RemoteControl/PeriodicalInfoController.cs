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
        void Start()
        {
            //Create MeshObject that can observe events and give them an event to do
            MeshObject batteryLevelMesh = new MeshObject(battery, new JumpLittle() );
            MeshObject remoteControllerMesh = new MeshObject(remoteController, new JumpLittle());
            MeshObject gpsSignalStateMesh = new MeshObject(gpsSignalState, new JumpLittle());
            MeshObject visualNavigationStateMesh = new MeshObject(visualNavigationState, new JumpLittle());
            MeshObject systemStatusMesh = new MeshObject(systemSatus, new JumpLittle());

            //Add the MeshObject to the list of objects waiting for something to happen
            SocketConnection.getInstance().AddObserver(batteryLevelMesh);
            SocketConnection.getInstance().AddObserver(remoteControllerMesh);
            SocketConnection.getInstance().AddObserver(gpsSignalStateMesh);
            SocketConnection.getInstance().AddObserver(visualNavigationStateMesh);
            SocketConnection.getInstance().AddObserver(systemStatusMesh);
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