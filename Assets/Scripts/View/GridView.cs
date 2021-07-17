using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    [SerializeField] private GameObject Field;

    [SerializeField] private float Height;
    [SerializeField] private float Width;
    [SerializeField] private float CellSizeInUnits;
    private Vector2 ReferencePoint;

    public void Start()
    {
        CalculateReferencePoint();
    }

    public void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.E))
        {
            ExpandGrid(new Vector2(-10,-10));
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            print(ReferencePoint);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject testBuilding = GameObject.Find("TestBuilding");
            PlaceObject(Vector2.zero, testBuilding);
        }
        */
    }

    public void PlaceObject(Vector2 gridPos, GameObject obj)
    {
        Vector2 placePosition = ReferencePoint + gridPos * CellSizeInUnits;
        obj.transform.position = new Vector3(   placePosition.x,
                                                0,
                                                placePosition.y);
        obj.transform.rotation = Quaternion.identity;
    }

    public void DeleteObject(Vector2 gridPos)
    {
        Vector2 deletePosition = ReferencePoint + gridPos * CellSizeInUnits;
        Collider[] items = Physics.OverlapSphere((Vector3)deletePosition, CellSizeInUnits * 0.75f);
        if(items.Length > 0)
        {
            if(items.Length == 1)
            {
                Destroy(items[0].gameObject);
            }
            else
            {
                print("Two Items in one cell on coordinates:" + gridPos);
            }
        }
    }
    
    public void ExpandGrid(Vector2 expansion)
    {
        Field.transform.localScale = new Vector3(Field.transform.localScale.x + Mathf.Abs(expansion.x),
                                                Field.transform.localScale.y,
                                                Field.transform.localScale.z + Mathf.Abs(expansion.y));

        Field.transform.position =  new Vector3(Field.transform.position.x + expansion.x / 2,
                                                Field.transform.position.y,
                                                Field.transform.position.z + expansion.y / 2);

        Height += Mathf.Abs(expansion.x);
        Width += Mathf.Abs(expansion.y);

        CalculateReferencePoint();
    }

    private void CalculateReferencePoint()
    {
        ReferencePoint = new Vector2(Field.transform.position.x - Width / 2,
                                     Field.transform.position.z + Height / 2);
    }
}
