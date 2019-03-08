using HoloToolkit.Examples.InteractiveElements;
using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ObserverPattern
{
    public class SliderManager : MonoBehaviour
    {
        public GameObject gameObject;
        public GameObject map;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void onUpdate()
        {
            if (gameObject.name.CompareTo("SliderAltitudeRTH") == 0)
            {
                DataManager.getInstance().AltitudeRTH = int.Parse(gameObject.GetComponent<SliderGestureControl>().Label.text);
                Debug.Log(DataManager.getInstance().AltitudeRTH);
                SocketConnection.getInstance().emitData("newAltitudeRTH", "{data:"+ DataManager.getInstance().AltitudeRTH + "}");
            }
            if (gameObject.name.CompareTo("SliderMaximumAltitude") == 0)
            {
                DataManager.getInstance().MaximumAltitude = int.Parse(gameObject.GetComponent<SliderGestureControl>().Label.text);
                Debug.Log(DataManager.getInstance().MaximumAltitude);
            }
            if (gameObject.name.CompareTo("SliderMaximumFlighDistance") == 0)
            {
                DataManager.getInstance().MaximumFlightDistance = int.Parse(gameObject.GetComponent<SliderGestureControl>().Label.text);
                Debug.Log(DataManager.getInstance().MaximumFlightDistance);
            }
            if (gameObject.name.CompareTo("SliderZoomMap") == 0) {
                map.GetComponent<AbstractMap>().SetZoom(float.Parse(gameObject.GetComponent<SliderGestureControl>().Label.text));
                map.GetComponent<AbstractMap>().UpdateMap(float.Parse(gameObject.GetComponent<SliderGestureControl>().Label.text));
            }
            if (gameObject.name.CompareTo("SliderSetAltitude") == 0)
            {
                SocketConnection.getInstance().emitFloat("newAltitudeWaypoints", (float)DataManager.getInstance().AltitudeWaypoints);
            }
            if (gameObject.name.CompareTo("SliderSetSpeed") == 0)
            {
                SocketConnection.getInstance().emitFloat("newSpeedWaypoints", (float)DataManager.getInstance().SpeedWaypoints);
            }
        }
        public void onSelect()
        {
        }
        public void onDown()
        {

        }
        public void onHold()
        {

        }
    }
}