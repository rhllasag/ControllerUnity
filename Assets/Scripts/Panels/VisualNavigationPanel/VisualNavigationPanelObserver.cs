using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using HoloToolkit.Examples.InteractiveElements;

public abstract class VisualNavigationPanelObserver{
    public abstract void OnNotify(string data, string component);
}
public class VisualNavigationPanelMeshObject : VisualNavigationPanelObserver
{
    GameObject boxObj;
    VisualNavigationPanelMeshEvents messegsEvent;
    InteractiveToggle interactiveToggleScript;
    TextMesh textMesh;
    public VisualNavigationPanelMeshObject(GameObject boxObj, VisualNavigationPanelMeshEvents boxEvent)
    {
        this.boxObj = boxObj;
        this.messegsEvent = boxEvent;
    }
    public VisualNavigationPanelMeshObject(InteractiveToggle iTScript, GameObject boxObj, VisualNavigationPanelMeshEvents boxEvent)
    {
        this.interactiveToggleScript = iTScript;
        this.boxObj = boxObj;
        this.messegsEvent = boxEvent;
    }
    public override void OnNotify(string data, string component)
    {
        if (messegsEvent.Description().CompareTo("intelligentFM") == 0)
        {

        }
    }
}