﻿using HoloToolkit.Unity.InputModule;
using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StopWaypointsMission : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{

    public void OnInputClicked(InputClickedEventData eventData)
    {
        SocketConnection.getInstance().emitMainScreenEvent("endWaypointsMission");
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
        SocketConnection.getInstance().emitMainScreenEvent("endWaypointsMission");

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SocketConnection.getInstance().emitMainScreenEvent("endWaypointsMission");

        }
    }
}
