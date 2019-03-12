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
        public GameObject returnToHomePanel;
        // Use this for initialization
        void Start()
        {
            BatteryPanelMeshObject batteryVoltageMesh = new BatteryPanelMeshObject(batteryVoltage, new BatteryVoltage());
            BatteryPanelMeshObject batteryTemperatureMesh = new BatteryPanelMeshObject(batteryTemperature, new BatteryTemperature());
            BatteryPanelMeshObject intelligentRTHMesh = new BatteryPanelMeshObject(intelligentRTHToogle.GetComponent<InteractiveToggle>(),intelligentRTH, new IntelligentRTH());
            BatteryPanelMeshObject rthBatteryPanelMesh = new BatteryPanelMeshObject(returnToHomePanel, new BatteryRTHPanel());
            SocketConnection.getInstance().AddObserver(batteryVoltageMesh);
            SocketConnection.getInstance().AddObserver(batteryTemperatureMesh);
            SocketConnection.getInstance().AddObserver(intelligentRTHMesh);
            SocketConnection.getInstance().AddObserver(rthBatteryPanelMesh);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
