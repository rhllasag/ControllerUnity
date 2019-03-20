using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using HoloToolkit.Examples.InteractiveElements;

public class SystemPanelController : MonoBehaviour {

    public GameObject beginnerModeToggle;
    public GameObject sliderMaximumAltitude;
    public GameObject limitDistanceToggle;
    public GameObject sliderMaximumFlightAltitude;
    void Start () {
        SystemPanelMeshObject beginnerModeToggleMesh = new SystemPanelMeshObject(beginnerModeToggle.GetComponent<InteractiveToggle>(), new BeginnerMode());
        SystemPanelMeshObject sliderMaximumAltitudeMesh = new SystemPanelMeshObject(sliderMaximumAltitude, new MaximumAltitude());
        SystemPanelMeshObject limitDistanceToggleMesh = new SystemPanelMeshObject(limitDistanceToggle.GetComponent<InteractiveToggle>(), new LimitDistance());
        SystemPanelMeshObject sliderMaximumFlightAltitudeMesh = new SystemPanelMeshObject(sliderMaximumFlightAltitude, new MaximumFlightAltitude());
        SocketConnection.getInstance().AddSystemObserver(beginnerModeToggleMesh);
        SocketConnection.getInstance().AddSystemObserver(sliderMaximumAltitudeMesh);
        SocketConnection.getInstance().AddSystemObserver(limitDistanceToggleMesh);
        SocketConnection.getInstance().AddSystemObserver(sliderMaximumFlightAltitudeMesh);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
