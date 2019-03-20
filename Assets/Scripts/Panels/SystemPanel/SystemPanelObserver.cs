using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using HoloToolkit.Examples.InteractiveElements;

public abstract class SystemPanelObserver {
    public abstract void OnNotify(string data, string component);
}
public class SystemPanelMeshObject : SystemPanelObserver
{
    GameObject boxObj;
    SystemPanelMeshEvents messegsEvent;
    InteractiveToggle interactiveToggleScript;
    TextMesh textMesh;

    public SystemPanelMeshObject(GameObject boxObj, SystemPanelMeshEvents boxEvent)
    {
        this.boxObj = boxObj;
        this.messegsEvent = boxEvent;
    }
    public SystemPanelMeshObject(TextMesh textMesh, GameObject boxObj, SystemPanelMeshEvents boxEvent)
    {
        this.textMesh = textMesh;
        this.boxObj = boxObj;
        this.messegsEvent = boxEvent;
    }
    public SystemPanelMeshObject(InteractiveToggle iTScript, SystemPanelMeshEvents boxEvent)
    {
        this.interactiveToggleScript = iTScript;
        this.messegsEvent = boxEvent;
    }
    public override void OnNotify(string data, string component)
    {
        if (messegsEvent.Description().CompareTo("beginnerMode") == 0 && component.CompareTo("beginnerModeChanged") == 0)
        {
            if (data.Contains("True"))
            {
                interactiveToggleScript.SetSelection(true);
            }
            else if (data.Contains("False"))
            {
                interactiveToggleScript.SetSelection(false);
            }
        }
        if (messegsEvent.Description().CompareTo("maximumAltitude") == 0)
        {

        }
        if (messegsEvent.Description().CompareTo("limitDistance") == 0 && component.CompareTo("limitDistanceChanged") == 0)
        {
            if (data.Contains("True"))
            {
                interactiveToggleScript.SetSelection(true);
            }
            else if (data.Contains("False"))
            {
                interactiveToggleScript.SetSelection(false);
            }
        }
        if (messegsEvent.Description().CompareTo("maximumFlightAltitude") == 0)
        {

        }
    }
}