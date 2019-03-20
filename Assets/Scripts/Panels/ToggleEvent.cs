using HoloToolkit.Examples.InteractiveElements;
using Mapbox.Json.Linq;
using ObserverPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEvent : MonoBehaviour {

    public GameObject enableGameObject;
    public GameObject toogle;
    int count = 0;
    bool RTHEnable = false;
    bool AnticollisionEnable = false;
    bool HorizontalAnticollisionEnable = false;
    bool BeginnerModeEnable = false;
    bool LimitDistanceEnable = false;

    private void changeUI()
    {
        if(enableGameObject!=null)
        enableGameObject.gameObject.SetActive(!enableGameObject.activeSelf);
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
    public void IsRTHEnabled() {
        if (count % 2 == 0)
        {
            changeUI();
            RTHEnable = !RTHEnable;
            SocketConnection.getInstance().emitBoolean("newSmartRTH",RTHEnable);
        }
        count++;
    }
    public void IsAnticollisionEnable()
    {
        if (count % 2 == 0)
        {
            changeUI();
            AnticollisionEnable = !AnticollisionEnable;
            SocketConnection.getInstance().emitBoolean("newAnticollision", AnticollisionEnable);
        }
        count++;
    }
    public void IsHorizontalAnticollisionEnable()
    {
        if (count % 2 == 0)
        {
            changeUI();
            HorizontalAnticollisionEnable = !HorizontalAnticollisionEnable;
            SocketConnection.getInstance().emitBoolean("newHorizontalAnticollision", HorizontalAnticollisionEnable);
        }
        count++;
    }
    public void IsBeginnerModeEnable()
    {
        if (count % 2 == 0)
        {
            changeUI();
            BeginnerModeEnable = !BeginnerModeEnable;
            SocketConnection.getInstance().emitBoolean("newBeginnerMode", BeginnerModeEnable);
        }
        count++;
    }
    public void IsLimitDistanceEnable()
    {
        if (count % 2 == 0)
        {
            changeUI();
            LimitDistanceEnable = !LimitDistanceEnable;
            SocketConnection.getInstance().emitBoolean("newLimitDistance", LimitDistanceEnable);
        }
        count++;
    }
    
}
