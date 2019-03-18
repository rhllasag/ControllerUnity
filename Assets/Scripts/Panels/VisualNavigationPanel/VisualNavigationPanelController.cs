using HoloToolkit.Examples.InteractiveElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNavigationPanelController : MonoBehaviour {
    public GameObject anticollisionSystemToggle;
    public GameObject horizontalACToggle;

    // Use this for initialization
    void Start () {
        VisualNavigationPanelMeshObject anticollisionSystemToggleMesh = new VisualNavigationPanelMeshObject(anticollisionSystemToggle.GetComponent<InteractiveToggle>(), anticollisionSystemToggle, new AnticollisionSystem());
        VisualNavigationPanelMeshObject horizontalACToggleMesh = new VisualNavigationPanelMeshObject(horizontalACToggle.GetComponent<InteractiveToggle>(), horizontalACToggle, new HorizontalAC());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
