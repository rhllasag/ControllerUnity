using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using HoloToolkit.Examples.InteractiveElements;

public class SystemPanelController : MonoBehaviour {

    public GameObject uAVHomeButton;
    public GameObject pilotHomeButton;
    public GameObject intelligentFlighModesToggle;
    public GameObject sliderAltitudeRTH;
    public GameObject beginnerModeToggle;
    public GameObject sliderMaximumAltitude;
    public GameObject limitDistanceToggle;
    public GameObject sliderMaximumFlightAltitude;
    void Start () {
        SystemPanelMeshObject intelligentFlighModesToggleMesh = new SystemPanelMeshObject(intelligentFlighModesToggle.GetComponent<InteractiveToggle>(), intelligentFlighModesToggle, new IntelligentFlightMode());
        SystemPanelMeshObject sliderAltitudeRTHMesh = new SystemPanelMeshObject(sliderAltitudeRTH.GetComponent<TextMesh>(), sliderAltitudeRTH, new AltitudeRTH());
        SystemPanelMeshObject beginnerModeToggleMesh = new SystemPanelMeshObject(beginnerModeToggle.GetComponent<InteractiveToggle>(), beginnerModeToggle, new BeginnerMode());
        SystemPanelMeshObject sliderMaximumAltitudeMesh = new SystemPanelMeshObject(sliderMaximumAltitude.GetComponent<TextMesh>(), sliderMaximumAltitude, new MaximumAltitude());
        SystemPanelMeshObject limitDistanceToggleMesh = new SystemPanelMeshObject(limitDistanceToggle.GetComponent<InteractiveToggle>(), limitDistanceToggle, new LimitDistance());
        SystemPanelMeshObject sliderMaximumFlightAltitudeMesh = new SystemPanelMeshObject(sliderMaximumFlightAltitude.GetComponent<TextMesh>(), sliderMaximumFlightAltitude, new MaximumFlightAltitude());

    }

    // Update is called once per frame
    void Update () {
		
	}
}
