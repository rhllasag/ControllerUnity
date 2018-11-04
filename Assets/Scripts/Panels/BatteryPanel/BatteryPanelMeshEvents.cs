using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ObserverPattern
{
    public abstract class BatteryPanelMeshEvents
    {

        public abstract string Description();
    }
    public class BatteryVoltage : BatteryPanelMeshEvents
    {
        public override string Description()
        {
            return "batteryVoltage";
        }
    }
    public class BatteryTemperature : BatteryPanelMeshEvents
    {
        public override string Description()
        {
            return "batteryTemperature";
        }
    }
    public class IntelligentRTH : BatteryPanelMeshEvents
    {
        public override string Description()
        {
            return "intelligentRTH";
        }
    }
    public class BatteryRTHError : BatteryPanelMeshEvents
    {
        public override string Description()
        {
            return "rthError";
        }
    }
    public class BatteryRTHWarning : BatteryPanelMeshEvents
    {
        public override string Description()
        {
            return "rthWarning";
        }
    }
    

}

