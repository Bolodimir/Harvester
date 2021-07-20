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
        Map = new MapItem[10,10];
    }

    void Update()
    {

    }

    public bool CellIsEmpty(Vector2 position)
    {
        if (Map[(int)position.x, (int)position.y] == null) return true;
        return false;
    }
    public void BuildItem(string Name, Vector2 pos)
    {
        for(int i = 0; i < MapItems.Length; i++)
        {
            if(MapItems[i].GetComponent<MapItem>().Name == Name)
            {
                GameObject newBuild = Instantiate(MapItems[i]);
                view.PlaceObject(pos, newBuild);
                Map[(int)pos.x, (int)pos.y] = newBuild.GetComponent<MapItem>();
            }
        }
    }
    public GameObject GetItem(string Name)
    {
        for (int i = 0; i < MapItems.Length; i++)
        {
            if (MapItems[i].GetComponent<MapItem>().Name == Name)
            {
                return MapItems[i];
            }
        }
        return null;
    }
    public bool WithinBoundaries(Vector2 gridPos)
    {
        if (gridPos.x >= 0 && gridPos.x < Map.GetLength(0) && gridPos.y >= 0 && gridPos.y < Map.GetLength(1)) return true;
        else return false;
    }
}
