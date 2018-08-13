using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickButton : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{
    public float x;
    public float y;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        SocketConnection.getInstance().emitData(x, y);
    }
    public void OnInputDown(InputEventData eventData)
    {
       // Debug.Log("OnInputDown");
    }

    public void OnInputUp(InputEventData eventData)
    {
       // Debug.Log("OnInputUp");
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
       // Debug.Log("OnManipulationCanceled");
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
       // Debug.Log("OnManipulationCompleted");
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
      //  Debug.Log("OnManipulationStarted");
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        SocketConnection.getInstance().emitData(x,y);
    }



    



    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
