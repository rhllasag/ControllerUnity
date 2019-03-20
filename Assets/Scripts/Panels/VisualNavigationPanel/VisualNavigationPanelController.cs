using HoloToolkit.Examples.InteractiveElements;
using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNavigationPanelController : MonoBehaviour {
    public GameObject anticollisionSystemToggle;
    public GameObject horizontalACToggle;

    // Use this for initialization
    void Start () {
        VisualNavigationPanelMeshObject anticollisionSystemToggleMesh = new VisualNavigationPanelMeshObject(anticollisionSystemToggle.GetComponent<InteractiveToggle>(), new AnticollisionSystem());
        VisualNavigationPanelMeshObject horizontalACToggleMesh = new VisualNavigationPanelMeshObject(horizontalACToggle.GetComponent<InteractiveToggle>(), new HorizontalAC());
        SocketConnection.getInstance().AddCollisionObserver(anticollisionSystemToggleMesh);
        SocketConnection.getInstance().AddCollisionObserver(horizontalACToggleMesh);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
