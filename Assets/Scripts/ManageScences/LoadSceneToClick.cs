﻿using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneToClick : MonoBehaviour, IInputHandler, IInputClickHandler, IManipulationHandler
{
    public GameObject gOToEnable;
    public GameObject gOToDissable1;
    public GameObject gOToDissable2;
    public GameObject gOToDissable3;
    public GameObject gOToDissable4;
    public GameObject gOToDissable5;
    public TextMesh textMesh;
    public string title;
    private void changeUI()
    {
        if (textMesh != null)
        {
            textMesh.text = title;
        }
        if (gOToEnable != null)
        {
            if (gOToEnable.active == true)
            {
                gOToEnable.SetActive(false);
            }
            else
            {
                gOToEnable.SetActive(true);
                if(gOToDissable1!=null)
                    gOToDissable1.SetActive(false);
                if (gOToDissable2 != null)
                    gOToDissable2.SetActive(false);
                if (gOToDissable3 != null)
                    gOToDissable3.SetActive(false);
                if (gOToDissable4 != null)
                    gOToDissable4.SetActive(false);
                if (gOToDissable5 != null)
                    gOToDissable5.SetActive(false);
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