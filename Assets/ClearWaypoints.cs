﻿using HoloToolkit.Unity.InputModule;
using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearWaypoints : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        SocketConnection.getInstance().emitMainScreenEvent("clearWaypoints");
    }

    public void OnInputDown(InputEventData eventData)
    {
    }

    public void OnInputUp(InputEventData eventData)
    {
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        SocketConnection.getInstance().emitMainScreenEvent("clearWaypoints");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SocketConnection.getInstance().emitMainScreenEvent("clearWaypoints");
        }
    }
}
