using HoloToolkit.Unity.InputModule;
using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendJsonTest : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
           SocketConnection.getInstance().emitFloat("newSpeedWaypoints", float.Parse("2.3"));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SocketConnection.getInstance().emitFloat("newAltitudeWaypoints", float.Parse("5"));
        }
    }
}
