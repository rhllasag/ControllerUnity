using HoloToolkit.Examples.InteractiveElements;
using Mapbox.Json.Linq;
using System;
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
        JObject json;
        public BatteryPanelMeshObject(GameObject boxObj, BatteryPanelMeshEvents boxEvent)
        {
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
            if (messegsEvent.Description().CompareTo("batteryVoltage") == 0 && component.CompareTo("batteryStateChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "voltage");
                boxObj.GetComponent<TextMesh>().text = value+ "V";
            }
            if (messegsEvent.Description().CompareTo("batteryTemperature") == 0 && component.CompareTo("batteryStateChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "temperature");
                boxObj.GetComponent<TextMesh>().text = value+"C";
            }
            if (messegsEvent.Description().CompareTo("intelligentRTH") == 0 && component.CompareTo("smartRTHChanged") == 0)
            {
                Debug.Log("In intelligentRTH");
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "value");
                if(value.CompareTo("true")==0)
                interactiveToggleScript.SetSelection(true);
                else if(value.CompareTo("false") == 0)
                    interactiveToggleScript.SetSelection(false);
            }
        }

        public string GetJArrayValue(JObject yourJArray, string key)
        {
            foreach (KeyValuePair<string, JToken> keyValuePair in yourJArray)
            {
                if (key == keyValuePair.Key)
                {
                    return keyValuePair.Value.ToString();
                }
            }
            return null;
        }
    }
}