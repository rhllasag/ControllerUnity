﻿using System.Collections;
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
        public GameObject returnToHomeError;
        public GameObject returnToHomeWarning;
        // Use this for initialization
        void Start()
        {
            BatteryPanelMeshObject batteryVoltageMesh = new BatteryPanelMeshObject(batteryVoltage, new BatteryVoltage());
            BatteryPanelMeshObject batteryTemperatureMesh = new BatteryPanelMeshObject(batteryTemperature, new BatteryTemperature());
            BatteryPanelMeshObject intelligentRTHMesh = new BatteryPanelMeshObject(intelligentRTH, new IntelligentRTH());
            BatteryPanelMeshObject returnToHomeErrorMesh = new BatteryPanelMeshObject(returnToHomeError, new BatteryRTHError());
            BatteryPanelMeshObject returnToHomeWarningMesh = new BatteryPanelMeshObject(returnToHomeWarning, new BatteryRTHWarning());
            SocketConnection.getInstance().AddObserver(batteryVoltageMesh);
            SocketConnection.getInstance().AddObserver(batteryTemperatureMesh);
            SocketConnection.getInstance().AddObserver(intelligentRTHMesh);
            SocketConnection.getInstance().AddObserver(returnToHomeErrorMesh);
            SocketConnection.getInstance().AddObserver(returnToHomeWarningMesh);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
