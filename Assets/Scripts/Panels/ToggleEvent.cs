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
        if (Input.GetKeyDown(KeyCode.K))
        {
            IsRTHEnabled();
        }
    }
    public void onIntelligentFlightModes()
    {
        changeUI();
    }
    public void offIntelligentFlightModes()
    {
        changeUI();
    }
    public void onIntelligentReturnToHome()
    {
        if (enableGameObject != null)
        {
            if (count % 2 == 0)
            {
                changeUI();
                SocketConnection.getInstance().emitBoolean("newSmartRTH", true);
                SocketConnection.getInstance().emitData(count, -1);
                
            }
            else
            {
                toogle.GetComponent<InteractiveToggle>().SetSelection(false);
            }
            count++;
        }
    }
    public void offIntelligentReturnToHome()
    {
        if (count % 2 == 0)
        {
            changeUI();
            SocketConnection.getInstance().emitBoolean("newSmartRTH", false);
            SocketConnection.getInstance().emitData(-1, count);
            count++;
        }
        else
        {
            toogle.GetComponent<InteractiveToggle>().SetSelection(true);
        }
        count++;
    }
    public void IsRTHEnabled() {
        if (count % 2 == 0)
        {
            changeUI();
            RTHEnable = !RTHEnable;
            //toogle.GetComponent<InteractiveToggle>().SetSelection(RTHEnable);
            SocketConnection.getInstance().emitBoolean("newSmartRTH",RTHEnable);
        }
        count++;
    }
    public void onBeginnerMode()
    {
        changeUI();
    }
    public void offBeginnerMode()
    {
        changeUI();
    }
    public void onHightMax()
    {
        changeUI();
    }
    public void offHightMax()
    {
        changeUI();
    }
    public void onAnticollision()
    {
        changeUI();
    }
    public void offAnticollision()
    {
        changeUI();
    }
    public void onHorizontalAnticollision()
    {
        changeUI();
    }
    public void offHorizontalAnticollision()
    {
        changeUI();
    }
    public void onRadarInformation()
    {
        changeUI();
    }
    public void offRadarInformation()
    {
        changeUI();
    }
    public void onMaxDistance()
    {
        changeUI();
    }
    public void offMaxDistance()
    {
        changeUI();
    }
}
