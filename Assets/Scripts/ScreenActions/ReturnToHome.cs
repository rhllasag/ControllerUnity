using HoloToolkit.Unity.InputModule;
using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToHome : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{
    public GameObject panel;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        SocketConnection.getInstance().emitMainScreenEvent("newReturnToHome");
        panel.SetActive(false);
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
        SocketConnection.getInstance().emitMainScreenEvent("newReturnToHome");
        panel.SetActive(false);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(panel!=null)
                panel.SetActive(false);
            SocketConnection.getInstance().emitMainScreenEvent("newReturnToHome");
        }
    }
}
