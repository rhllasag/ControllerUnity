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
    public class GPSSignal : MeshEvents
    {
        public override string Description()
        {
            return "gps";
        }
    }
    public class VisualNavigation : MeshEvents
    {
        public override string Description()
        {
            return "navigation";
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
    
}