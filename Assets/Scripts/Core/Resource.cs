using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Resource
{
    public string Name;
    public int Number;

    public Resource(string Name, int Number)
    {        
        this.Name = Name;
        this.Number = Number;
    }

}
