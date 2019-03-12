using HoloToolkit.Unity.InputModule;
using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{
    public VideoReciver videoReciver;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        videoReciver.disconnect();
        videoReciver.connect();
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
        videoReciver.disconnect();
        videoReciver.connect();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            videoReciver.disconnect();
            videoReciver.connect();
        }

    }
}
