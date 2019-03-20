using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace ObserverPattern
{
    public class JoyStickButton : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
    {
        public float x;
        public float y;
        int count = 0;
        public void OnInputClicked(InputClickedEventData eventData)
        {
            if (count % 2 == 0)
                SocketConnection.getInstance().emitData(x, y);
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
            
        }
        void Start()
        {
        }
        void Update()
        {

        }
    }
}