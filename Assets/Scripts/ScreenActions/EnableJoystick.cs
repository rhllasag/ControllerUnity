using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace ObserverPattern
{
    public class EnableJoystick : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
    {
        int count = 0;
        public void OnInputClicked(InputClickedEventData eventData)
        {
            
            if(count%2==0)
            SocketConnection.getInstance().emitMainScreenEvent("newJoystickPanel");
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

        // Use this for initialization
        void Start()
        {
        }

        void Awake()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                SocketConnection.getInstance().emitMainScreenEvent("newJoystickPanel");
            }
        }
    }
}
