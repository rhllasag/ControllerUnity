using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{
    void changeCamera()
    {
        Debug.Log("Change camera");
    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        changeCamera();
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
        changeCamera();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
