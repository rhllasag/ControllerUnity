using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;
namespace ObserverPattern
{
    public class BatteryPanelController : MonoBehaviour
    {
        public GameObject batteryVoltage;
        //The Object that will jump
        public GameObject batteryTemperature;
        public GameObject intelligentRTH;
        public GameObject intelligentRTHToogle;
        // Use this for initialization
        void Start()
        {
            BatteryPanelMeshObject batteryVoltageMesh = new BatteryPanelMeshObject(batteryVoltage, new BatteryVoltage());
            BatteryPanelMeshObject batteryTemperatureMesh = new BatteryPanelMeshObject(batteryTemperature, new BatteryTemperature());
            BatteryPanelMeshObject intelligentRTHMesh = new BatteryPanelMeshObject(intelligentRTHToogle.GetComponent<InteractiveToggle>(),intelligentRTH, new IntelligentRTH());

            SocketConnection.getInstance().AddObserver(batteryVoltageMesh);
            SocketConnection.getInstance().AddObserver(batteryTemperatureMesh);
            SocketConnection.getInstance().AddObserver(intelligentRTHMesh);

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
