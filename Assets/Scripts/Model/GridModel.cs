using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModel : MonoBehaviour
{
    [SerializeField] GridView view;
    [SerializeField] GameObject[] MapItems;

    MapItem[,] Map;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void CheckCell(Vector2 position)
    {

    }
    public void BuildItem(string Name, Vector2 pos)
    {
        for(int i = 0; i < MapItems.Length; i++)
        {
            if(MapItems[i].GetComponent<MapItem>().Name == Name)
            {
                GameObject newBuild = Instantiate(MapItems[i]);
                view.PlaceObject(pos, newBuild);
            }
        }
    }
}
