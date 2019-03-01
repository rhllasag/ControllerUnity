using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Mapbox.Unity.Map;
using HoloToolkit.Examples.InteractiveElements;
using Mapbox.Unity.Location;

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
        //GameObject to Landing, TakeOff, RTH
        GameObject landingObj;
        GameObject takeOffObj;
        GameObject rTHObj;
        GameObject map;
        GameObject zoomMap;
        GameObject locationAircraft;
        GameObject locationHome;
        //What will happen when this box gets an event
        MeshEvents messegsEvent;
        //JObject
        JObject json;
       // HandleTextFile handleText= new HandleTextFile();
        public MeshObject(GameObject boxObj, MeshEvents boxEvent)
        {
            this.boxObj = boxObj;
            this.messegsEvent = boxEvent;
        }
        public MeshObject(GameObject boxObj, GameObject map, GameObject zoomMap, GameObject locationHome, GameObject locationAircraft, MeshEvents boxEvent)
        {
            this.locationAircraft = locationAircraft;
            this.locationHome = locationHome;
            this.zoomMap = zoomMap;
            this.map = map;
            this.boxObj = boxObj;
            this.messegsEvent = boxEvent;
        }
        public MeshObject(GameObject gobj1, GameObject gobj2, GameObject gobj3, GameObject boxObj, MeshEvents boxEvent)
        {
            this.boxObj = boxObj;
            this.landingObj = gobj1;
            this.takeOffObj = gobj2;
            this.rTHObj = gobj3;
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
            if (messegsEvent.Description().CompareTo("rc") == 0 && component.CompareTo("rcConnectionStatusChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "batteryLevel");
                boxObj.GetComponent<TextMesh>().text = value + "%";
            }
            if (messegsEvent.Description().CompareTo("navigation") == 0 && component.CompareTo("flightAssistantStateChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "sensorBeingUsed");
                boxObj.GetComponent<TextMesh>().text = value;
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
                        landingObj.SetActive(true);
                        takeOffObj.SetActive(true);
                        rTHObj.SetActive(true);
                        break;
                    case "1":
                        boxObj.GetComponent<TextMesh>().text = "Sprt";
                        landingObj.SetActive(true);
                        takeOffObj.SetActive(true);
                        rTHObj.SetActive(false);
                        break;
                    case "2":
                        boxObj.GetComponent<TextMesh>().text = "Pos";
                        landingObj.SetActive(true);
                        takeOffObj.SetActive(true);
                        rTHObj.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            if (messegsEvent.Description().CompareTo("system") == 0 && component.CompareTo("systemStatusChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "systemStatus");
                boxObj.GetComponent<TextMesh>().text = "sys";
            }
            if (messegsEvent.Description().CompareTo("batteryNAircraft") == 0 && component.CompareTo("batteryANeededRTHChanged") == 0)
            {
                json = JObject.Parse(data);
                int value = int.Parse(GetJArrayValue(json, "batteryLevel"));
                boxObj.GetComponent<TextMesh>().text = "["+((value*20)/100)+"min]";
            }
            if (messegsEvent.Description().CompareTo("flightTime") == 0 && component.CompareTo("flightTimeChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = GetJArrayValue(json, "flightTime");
                boxObj.GetComponent<TextMesh>().text = value;
            }
            if (messegsEvent.Description().CompareTo("hight") == 0 && component.CompareTo("coordinatesChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = "m: "+GetJArrayValue(json, "hight")+"    Km: "+ GetJArrayValue(json, "distance");
                boxObj.GetComponent<TextMesh>().text = value;
            }
            if (messegsEvent.Description().CompareTo("homeLocation") == 0 && component.CompareTo("homeLocationChanged") == 0)
            {
                json = JObject.Parse(data);
                var value = "(" + GetJArrayValue(json, "latitude") + ":" + GetJArrayValue(json, "longitude") + ")";
                boxObj.GetComponent<TextMesh>().text = value;
                Debug.Log(float.Parse(GetJArrayValue(json, "latitude"))+","+ float.Parse(GetJArrayValue(json, "longitude")));
                map.GetComponent<AbstractMap>().SetCenterLatitudeLongitude(new Mapbox.Utils.Vector2d(float.Parse(GetJArrayValue(json, "latitude")), float.Parse(GetJArrayValue(json, "longitude"))));
                map.GetComponent<AbstractMap>().SetZoom(float.Parse(zoomMap.GetComponent<SliderGestureControl>().Label.text));
                map.GetComponent<AbstractMap>().UpdateMap(float.Parse(zoomMap.GetComponent<SliderGestureControl>().Label.text));
                locationHome.GetComponent<LocationArrayEditorLocationProvider>()._latitudeLongitude[0] = ""+float.Parse(GetJArrayValue(json, "latitude"))+", "+ float.Parse(GetJArrayValue(json, "longitude"));
                locationAircraft.GetComponent<LocationArrayEditorLocationProvider>()._latitudeLongitude[0] = "0, 0";
            }
            if (messegsEvent.Description().CompareTo("coordinates") == 0 && component.CompareTo("coordinatesChanged") == 0)
            {
                json = JObject.Parse(data);
                //handleText.WriteString("True;False;False;True;unity;DeviceLocationProvider;20180524-163505.787;20180524-163427.922;"+ GetJArrayValue(json, "latitude") + ";" + GetJArrayValue(json, "longitude")+ ";34.0;305.1;305.1;[not supported by provider];[not supported by provider];[not supported by provider];[not supported by provider]");
                var value = "(" + GetJArrayValue(json, "latitude") + " : " + GetJArrayValue(json, "longitude") + ")";
                boxObj.GetComponent<TextMesh>().text = value;

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
