using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchStats : MonoBehaviour
{
    public Research ResourceSpawnFrequency;
    public Research FurnaceEfficiency;
    public Research ForgeEfficiency;
    public Research FactoryEfficiency;

    [HideInInspector] public List<Research> ResearchList;

    private void Awake()
    {
        ResearchList.Add(ResourceSpawnFrequency);
        ResearchList.Add(FurnaceEfficiency);
        ResearchList.Add(ForgeEfficiency);
        ResearchList.Add(FactoryEfficiency);
    }

    public Research Find(string name)
    {
        if (name == ResourceSpawnFrequency.GetName()) return ResourceSpawnFrequency;
        if (name == FurnaceEfficiency.GetName()) return FurnaceEfficiency;
        if (name == ForgeEfficiency.GetName()) return ForgeEfficiency;
        if (name == FactoryEfficiency.GetName()) return FactoryEfficiency;
        return null;
    }
}
