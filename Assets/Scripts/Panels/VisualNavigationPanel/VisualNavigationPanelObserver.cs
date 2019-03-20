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
    VisualNavigationPanelMeshEvents messegsEvent;
    InteractiveToggle interactiveToggleScript;
    public VisualNavigationPanelMeshObject(InteractiveToggle iTScript, VisualNavigationPanelMeshEvents boxEvent)
    {
        this.interactiveToggleScript = iTScript;
        this.messegsEvent = boxEvent;
    }
    public override void OnNotify(string data, string component)
    {
        if (messegsEvent.Description().CompareTo("anticollisionSystem") == 0 && component.CompareTo("anticollisionChanged") == 0)
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
        if (messegsEvent.Description().CompareTo("horizontalAC") == 0 && component.CompareTo("horizontalAnticollisionChanged") == 0)
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
        
    }
}