using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbleConfigurationPanels : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{

    public GameObject gOToEnable;
    public GameObject gOToEnable2;
    private void changeUI()
    {

        if (gOToEnable != null)
        {
            if (gOToEnable.active == true)
            {
                gOToEnable.SetActive(false);
            }
            else
            {
                gOToEnable.SetActive(true);

            }
        }
        if (gOToEnable2 != null)
        {
            if (gOToEnable2.active == true)
            {
                gOToEnable2.SetActive(false);
            }
            else
            {
                gOToEnable2.SetActive(true);

            }
        }
    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        changeUI();
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
        changeUI();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
