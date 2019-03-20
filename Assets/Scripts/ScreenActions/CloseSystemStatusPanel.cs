﻿using HoloToolkit.Unity.InputModule;
using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSystemStatusPanel : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{

    // Use this for initialization
    public GameObject panel;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        panel.SetActive(false);
        SocketConnection.getInstance().emitData("newReturnToHomeDecision", "false");
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
    }

    void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            panel.SetActive(false);
            SocketConnection.getInstance().emitData("newReturnToHomeDecision", "false");
        }
    }
}
