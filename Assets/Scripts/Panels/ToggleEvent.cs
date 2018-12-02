using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEvent : MonoBehaviour {

    public GameObject enableGameObject;
    private void changeUI()
    {
        if (enableGameObject != null)
        {
            if (enableGameObject.active == true)
            {
                enableGameObject.SetActive(false);
            }
            else
            {
                enableGameObject.SetActive(true);

            }
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onIntelligentFlightModes()
    {
        changeUI();
        Debug.Log("On Intelligent Flight Modes");
    }
    public void offIntelligentFlightModes()
    {
        changeUI();
        Debug.Log("Off Intelligent Flight Modes");
    }
    public void onIntelligentReturnToHome()
    {
        changeUI();
        Debug.Log("On Intelligent Return To Home");
    }
    public void offIntelligentReturnToHome()
    {
        changeUI();
        Debug.Log("Off Intelligent Return To Home");
    }
    public void onBeginnerMode()
    {
        changeUI();
        Debug.Log("On Beginner Mode");
    }
    public void offBeginnerMode()
    {
        changeUI();
        Debug.Log("Off Beginner Mode");
    }
    public void onHightMax()
    {
        changeUI();
        Debug.Log("On Hight Max");
    }
    public void offHightMax()
    {
        changeUI();
        Debug.Log("Off Hight Max");
    }
    public void onAnticollision()
    {
        changeUI();
        Debug.Log("On Anticollision");
    }
    public void offAnticollision()
    {
        changeUI();
        Debug.Log("Off Anticollision");
    }
    public void onHorizontalAnticollision()
    {
        changeUI();
        Debug.Log("On Horizontal Anticollision");
    }
    public void offHorizontalAnticollision()
    {
        changeUI();
        Debug.Log("Off Horizontal Anticollision");
    }
    public void onRadarInformation()
    {
        changeUI();
        Debug.Log("On Radar Information");
    }
    public void offRadarInformation()
    {
        changeUI();
        Debug.Log("Off Radar Information");
    }
    public void onMaxDistance()
    {
        changeUI();
        Debug.Log("On Max Distance");
    }
    public void offMaxDistance()
    {
        changeUI();
        Debug.Log("Off Max Distance");
    }
}
