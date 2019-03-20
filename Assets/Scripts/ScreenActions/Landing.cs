﻿using HoloToolkit.Unity.InputModule;
using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour,IInputHandler, IInputClickHandler, IManipulationHandler
{
    int count = 0;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (count % 2 == 0)
            SocketConnection.getInstance().emitMainScreenEvent("newLanding");
        count++;
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
        //SocketConnection.getInstance().emitMainScreenEvent("newLanding");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SocketConnection.getInstance().emitMainScreenEvent("newLanding");
        }
    }
}
