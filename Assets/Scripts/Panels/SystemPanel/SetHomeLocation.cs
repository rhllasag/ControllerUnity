using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class SetHomeLocation : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
            SocketConnection.getInstance().emitMainScreenEvent("newHome");
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
            SocketConnection.getInstance().emitMainScreenEvent("newHome");

    }

        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SocketConnection.getInstance().emitMainScreenEvent("newHome");
        }
    }
}
}