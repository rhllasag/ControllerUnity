using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisualNavigationPanelMeshEvents{
    public abstract string Description();
}
public class AnticollisionSystem : VisualNavigationPanelMeshEvents
{
    public override string Description()
    {
        return "anticollisionSystem";
    }
}
public class HorizontalAC : VisualNavigationPanelMeshEvents
{
    public override string Description()
    {
        return "horizontalAC";
    }
}
public class InverseAT : VisualNavigationPanelMeshEvents
{
    public override string Description()
    {
        return "inverseAT";
    }
}
public class ShowRadar : VisualNavigationPanelMeshEvents
{
    public override string Description()
    {
        return "showRadar";
    }
}
