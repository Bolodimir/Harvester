using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Resource
{
    public string Name { get; set; }
    public int Number { get; set; }

    public Resource(string Name, int Number)
    {        
        this.Name = Name;
        this.Number = Number;
    }

}
