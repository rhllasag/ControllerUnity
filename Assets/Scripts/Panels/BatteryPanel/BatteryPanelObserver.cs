using HoloToolkit.Examples.InteractiveElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ObserverPattern
{
    public abstract class BatteryPanelObserver
    {

        public abstract void OnNotify(string data, string component);
    }
    public class BatteryPanelMeshObject : BatteryPanelObserver
    {
        GameObject boxObj;
        BatteryPanelMeshEvents messegsEvent;
        InteractiveToggle interactiveToggleScript;
        TextMesh textMesh;

        public BatteryPanelMeshObject(GameObject boxObj, BatteryPanelMeshEvents boxEvent)
        {
            this.boxObj = boxObj;
            this.messegsEvent = boxEvent;
        }
        public BatteryPanelMeshObject(TextMesh textMesh,GameObject boxObj, BatteryPanelMeshEvents boxEvent)
        {
            this.textMesh = textMesh;
            this.boxObj = boxObj;
            this.messegsEvent = boxEvent;
        }
        public BatteryPanelMeshObject(InteractiveToggle iTScript, GameObject boxObj, BatteryPanelMeshEvents boxEvent)
        {
            this.interactiveToggleScript = iTScript;
            this.boxObj = boxObj;
            this.messegsEvent = boxEvent;
        }
        public override void OnNotify(string data, string component)
        {
            if (messegsEvent.Description().CompareTo("batteryVoltage") == 0)
            {

            }
            if (messegsEvent.Description().CompareTo("batteryTemperature") == 0)
            {

            }
            if (messegsEvent.Description().CompareTo("intelligentRTH") == 0)
            {
                interactiveToggleScript.SetSelection(true);
                bool go=interactiveToggleScript.IsSelected;
            }
            if (messegsEvent.Description().CompareTo("rthError") == 0)
            {
                textMesh.text = "";
            }
            if (messegsEvent.Description().CompareTo("rthWarning") == 0)
            {
                textMesh.text="";
                textMesh.ToString();
            }
        }
    }
}