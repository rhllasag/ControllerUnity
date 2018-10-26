using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
namespace ObserverPattern
{
    public abstract class Observer
    {

        public abstract void OnNotify(string data, string component);
    }
    public class MeshObject : Observer
    {
        //The  gameobject which will do something
        GameObject boxObj;
        //What will happen when this box gets an event
        MeshEvents messegsEvent;
        //JObject
        JObject json;

        public MeshObject(GameObject boxObj, MeshEvents boxEvent)
        {
            this.boxObj = boxObj;
            this.messegsEvent = boxEvent;
        }

        //What the box will do if the event fits it (will always fit but you will probably change that on your own)
        public override void OnNotify(string data, string component)
        {
            if (messegsEvent.Description().CompareTo("battery") == 0 && component.CompareTo("batteryLevelChanged") ==0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "batteryLevel");
                boxObj.GetComponent<TextMesh>().text = value+"%";
            }
            if (messegsEvent.Description().CompareTo("system") == 0 && component.CompareTo("systemStatusChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "systemStatus");
                boxObj.GetComponent<TextMesh>().text = "sys";
            }
            if (messegsEvent.Description().CompareTo("rc") == 0 && component.CompareTo("rcConnectionStatusChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "batteryLevel");
                boxObj.GetComponent<TextMesh>().text = value+"%";
            }
            if (messegsEvent.Description().CompareTo("gps") == 0 && component.CompareTo("gpsSignalStatusChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "gpsSignalStatus");
                boxObj.GetComponent<TextMesh>().text = value;
            }
            if (messegsEvent.Description().CompareTo("flightMode") == 0 && component.CompareTo("fightModeSwitchChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "flightMode");
                switch (value) {
                    case "0":
                        boxObj.GetComponent<TextMesh>().text = "Atti";
                        break;
                    case "1":
                        boxObj.GetComponent<TextMesh>().text = "Sprt";
                        break;
                    case "2":
                        boxObj.GetComponent<TextMesh>().text = "Pos";
                        break;
                    default:
                        break;
                }
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
