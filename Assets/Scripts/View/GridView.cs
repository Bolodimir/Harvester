using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    [SerializeField] private GameObject Field;
    [SerializeField] private GridModel model;

    [SerializeField] private float Height;
    [SerializeField] private float Width;
    [SerializeField] private float CellSizeInUnits;
    private Vector2 ReferencePoint;
    CameraBehaviour MainCamera;

    //BuildingMode

    GameObject ToBuild;
    bool BuildingMode = false;
    Camera cam;
    bool MovingBuilding = false;
    Vector3 forGizmo;

    public void Start()
    {
        CalculateReferencePoint();
        MainCamera = Camera.main.GetComponent<CameraBehaviour>();
        cam = Camera.main;
    }  
    public void PlaceObject(Vector2 gridPos, GameObject obj)
    {
        gridPos.y *= -1;
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
    private Vector3 FromGridToWorld(Vector2 pos)
    {
        return Vector3.zero;
    }
    private Vector2 FromWorldToGrid(Vector3 pos)
    {
        pos.x -= ReferencePoint.x;
        pos.z -= ReferencePoint.y;
        pos /= CellSizeInUnits;
        pos.z = pos.z * -1;
        return new Vector2(pos.x, pos.z);
    }

    //Building Mode
    public void Update()
    {
        if (BuildingMode && Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray FromCamera = cam.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(FromCamera, out hit))
                {
                    if(hit.transform.parent.gameObject == ToBuild)
                    {
                        MainCamera.LockControls();
                        MovingBuilding = true;
                    }
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (MovingBuilding)
                {
                    Vector3 TouchPos = MainCamera.GetPlanePointFromScreenPoint(Input.GetTouch(0).position);
                    Vector3 Direction = TouchPos - ToBuild.transform.position;
                    float Distance = Direction.magnitude;
                    if(Distance >= 0.9f * CellSizeInUnits)
                    {
                        Vector3 newPos = Vector3.zero;
                        if(Distance <1.3f * CellSizeInUnits)
                        {
                            if (Mathf.Abs(Direction.normalized.x) > 0.25 &&
                                Mathf.Abs(Direction.normalized.z) > 0.25) return;

                            if (Mathf.Abs(Direction.normalized.x) < 0.25)
                            {
                                newPos.x = 0;
                                newPos.z = CellSizeInUnits * Mathf.Sign(Direction.z);
                            }

                            if (Mathf.Abs(Direction.normalized.z) < 0.25)
                            {
                                newPos.x = CellSizeInUnits * Mathf.Sign(Direction.x);
                                newPos.z = 0;
                            }
                        }
                        else
                        {
                            newPos = new Vector3(CellSizeInUnits * Mathf.Sign(Direction.x),
                                                 0,
                                                 CellSizeInUnits * Mathf.Sign(Direction.z));
                        }
                        
                        newPos += ToBuild.transform.position;
                        Vector2 gridPos = FromWorldToGrid(newPos);
                        if(model.WithinBoundaries(gridPos))
                        {
                            PlaceObject(gridPos, ToBuild);
                            if (model.CellIsEmpty(gridPos)) ChangeBuildingColor(ToBuild, Color.green);
                            else ChangeBuildingColor(ToBuild, Color.red);
                        }
                    }
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                MainCamera.UnLockControls();
                MovingBuilding = false;
            }
        }
    }
    public void InitiateBuildingForPlacing(string Name)
    {
        BuildingMode = true;

        GameObject Item = model.GetItem(Name);
        ToBuild = Instantiate(Item);
        Vector2 pos = new Vector2(5,5);
        PlaceObject(pos, ToBuild);
        if (model.CellIsEmpty(pos))
        {
            ChangeBuildingColor(ToBuild, Color.green);
        }
        else
        {
            ChangeBuildingColor(ToBuild, Color.red);
        }

    }
    private void ChangeBuildingColor(GameObject building, Color color)
    {
        building.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color",color);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(forGizmo, Vector3.one * 3);
        /*if(ToBuild != null)
        {
            Gizmos.DrawLine(ToBuild.transform.position, ToBuild.transform.position + forGizmo);
        }
        */
    }
}
