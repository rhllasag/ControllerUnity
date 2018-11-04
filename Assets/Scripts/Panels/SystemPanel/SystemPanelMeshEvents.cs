using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
public abstract class SystemPanelMeshEvents{
      public abstract string Description();
}
public class IntelligentFlightMode : SystemPanelMeshEvents
{
    public override string Description()
    {
        return "intelligentFM";
    }
}
public class AltitudeRTH : SystemPanelMeshEvents
{
    public override string Description()
    {
        return "altitudeRTH";
    }
}
public class BeginnerMode : SystemPanelMeshEvents
{
    public override string Description()
    {
        return "beginnerMode";
    }
}
public class MaximumAltitude : SystemPanelMeshEvents
{
    public override string Description()
    {
        return "maximumAltitude";
    }
}
public class LimitDistance : SystemPanelMeshEvents
{
    public override string Description()
    {
        return "limitDistance";
    }
}
public class MaximumFlightAltitude : SystemPanelMeshEvents
{
    public override string Description()
    {
        return "maximumFlightAltitude";
    }
}