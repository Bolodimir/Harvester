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

    //DestroyMode

    bool DestroyMode = false;
    GameObject ToDestroy;
    bool HasMoved;

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
        
        if(items.Length == 1)
        {
            Destroy(items[0].gameObject);
        }
        else
        {
            print("Two Items in one cell on coordinates:" + gridPos);
        }
        
    }    
    public void ExpandGrid(Vector2 expansion)
    {
        Field.transform.localScale = new Vector3(Field.transform.localScale.x + Mathf.Abs(expansion.x) * CellSizeInUnits,
                                                Field.transform.localScale.y,
                                                Field.transform.localScale.z + Mathf.Abs(expansion.y) * CellSizeInUnits);

        Field.transform.position =  new Vector3(Field.transform.position.x + expansion.x * CellSizeInUnits / 2,
                                                Field.transform.position.y,
                                                Field.transform.position.z + expansion.y * CellSizeInUnits / 2);

        Height += Mathf.Abs(expansion.x) * CellSizeInUnits;
        Width += Mathf.Abs(expansion.y) * CellSizeInUnits;

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

    void Update()
    {
        if (BuildingMode && Input.touchCount == 1)
        {
            BuildModeUpdate();
        }
        if(DestroyMode && Input.touchCount == 1)
        {
            DestroyModeUpdate();
        }
    }   
    //DestroyMode
    public void InitiateDestroyMode()
    {
        DestroyMode = true;
    }
    private void DestroyModeUpdate()
    {
        if(Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HasMoved = false;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            HasMoved = true;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (!HasMoved)
            {
                Ray FromCamera = cam.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(FromCamera, out hit, 1000f, LayerMask.GetMask("Building")))
                {
                    GameObject building = hit.transform.parent.gameObject;
                    if (building.GetComponent<Building>() != null)
                    {
                        if(ToDestroy != null)
                        {
                            ChangeBuildingColor(ToDestroy, Color.white);
                        }
                        ToDestroy = building;
                        ChangeBuildingColor(ToDestroy, Color.red);
                    }
                }
            }
        }
    } 
    public void ChooseBuildingForDestroying()
    {
        if(ToDestroy != null)
        {
            model.DestroyBuilding(FromWorldToGrid(ToDestroy.transform.position));
            Destroy(ToDestroy);
        }        
    }
    public void CancelDestroyMode()
    {
        if(ToDestroy != null)
        {
            ChangeBuildingColor(ToDestroy, Color.white);
        }
        DestroyMode = false;
        ToDestroy = null;
    }    
    //BuildingMode
    public void InitiateBuildingForPlacing(string Name)
    {
        BuildingMode = true;
        Destroy(ToBuild);

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
    private void BuildModeUpdate()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray FromCamera = cam.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(FromCamera, out hit, 1000f, LayerMask.GetMask("Building")))
            {
                if (hit.transform.parent.gameObject == ToBuild)
                {
                    MainCamera.LockControls();
                    MovingBuilding = true;
                }
            }
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved && MovingBuilding)
        {
            Vector3 TouchPos = MainCamera.GetPlanePointFromScreenPoint(Input.GetTouch(0).position);
            Vector3 Direction = TouchPos - ToBuild.transform.position;
            float Distance = Direction.magnitude;
            if (Distance >= 0.9f * CellSizeInUnits)
            {
                Vector3 newPos = Vector3.zero;
                if (Distance < 1.3f * CellSizeInUnits)
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
                if (model.WithinBoundaries(gridPos))
                {
                    PlaceObject(gridPos, ToBuild);
                    if (model.CellIsEmpty(gridPos)) ChangeBuildingColor(ToBuild, Color.green);
                    else ChangeBuildingColor(ToBuild, Color.red);
                }
            }

        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            MainCamera.UnLockControls();
            MovingBuilding = false;
        }
    }
    private void ChangeBuildingColor(GameObject building, Color color)
    {
        building.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color",color);
    }
    public bool ChoosePlaceForBuilding()
    {
        MovingBuilding = false;
        MainCamera.UnLockControls();
        Vector2 gridPos = FromWorldToGrid(ToBuild.transform.position);
        if (model.CellIsEmpty(gridPos))
        {
            model.BuildItem(ToBuild.GetComponent<MapItem>().Name, gridPos);
            Destroy(ToBuild);
            BuildingMode = false;
            return true;
        }
        return false;
    }
    public void CancelBuildingMode()
    {
        MovingBuilding = false;
        MainCamera.UnLockControls();
        Destroy(ToBuild);
        BuildingMode = false;
    }
    public bool IsBuildingMode()
    {
        return BuildingMode;
    }
}
