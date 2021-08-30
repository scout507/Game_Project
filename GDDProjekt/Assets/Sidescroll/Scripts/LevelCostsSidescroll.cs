using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Index = Level
[System.Serializable]
public class LevelCostsSidescroll
{
    public ComponentsSidescroll[] level = new ComponentsSidescroll[3];
}

[System.Serializable]
public class ComponentsSidescroll
{
    public RessourceSidescroll[] componenten = new RessourceSidescroll[3];
}

[System.Serializable]
public class RessourceSidescroll
{
    public int amount = 1;
    public int id = 0;
}