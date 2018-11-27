using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ObserverPattern
{


    public abstract class MeshEvents
    {
        public abstract string Description();
    }

    public class BatteryLevel : MeshEvents
    {
        public override string Description()
        {
            return "battery";
        }
    }
    public class RCControl : MeshEvents
    {
        public override string Description()
        {
            return "rc";
        }
    }
    public class VisualNavigation : MeshEvents
    {
        public override string Description()
        {
            return "navigation";
        }
    }
    public class GPSSignal : MeshEvents
    {
        public override string Description()
        {
            return "gps";
        }
    }
    public class FlightSwitchMode : MeshEvents
    {
        public override string Description()
        {
            return "flightMode";
        }
    }
    public class SystemStatus : MeshEvents
    {
        public override string Description()
        {
            return "system";
        }
    }
    public class BatteryARTH : MeshEvents
    {
        public override string Description()
        {
            return "batteryNAircraft";
        }
    }
    public class FlightTime : MeshEvents
    {
        public override string Description()
        {
            return "flightTime";
        }
    }
    public class Hight : MeshEvents
    {
        public override string Description()
        {
            return "hight";
        }
    }
    public class HomeLocation : MeshEvents
    {
        public override string Description()
        {
            return "homeLocation";
        }
    }
    public class Coordinates : MeshEvents
    {
        public override string Description()
        {
            return "coordinates";
        }
    }
}