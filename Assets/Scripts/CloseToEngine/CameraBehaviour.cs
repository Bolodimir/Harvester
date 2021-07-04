using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform PlaneCenterObject;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private GameObject WorldTouchPoint;

    [SerializeField] private float CameraDrag;
    [SerializeField] private float CameraSpeed;
    [SerializeField] private float MaxCameraSpeed;
    [SerializeField] private float MaxHeight;
    [SerializeField] private float MinHeight;
    [SerializeField] private Vector2 FirstBorderPoint;
    [SerializeField] private Vector2 SecondBorderPoint;

    private Vector3 PlaneCenter;
    private Plane PlaneForInterception;
    private float CurrentCameraSpeed;
    private Vector3 CurrentCameraMoveDir;
    private float LastTouchUpdateTime;

    Touch oldTouch;

    public void Start()
    {
        if (MainCamera == null) MainCamera = Camera.main;
        if (PlaneCenterObject != null) PlaneCenter = PlaneCenterObject.position;
            else PlaneCenter = Vector3.zero;
        PlaneForInterception = new Plane(Vector3.up, PlaneCenter);
    }

    public void Update()
    {
        if (Input.touchCount == 0) SpeedUpdate();

        if(Input.touchCount == 1)
        {
            Touch newTouch = Input.GetTouch(0);
            if (newTouch.phase == TouchPhase.Began)
            {
                CurrentCameraMoveDir = Vector3.zero;
                CurrentCameraSpeed = 0;
            }
            if (newTouch.phase == TouchPhase.Moved)
            {
                if (!Equals(oldTouch, newTouch))
                {
                    Vector3 WorldPointDelta = GetWorldPointDeltaFromTouch(Input.GetTouch(0));
                    MainCamera.transform.position += WorldPointDelta;
                    CurrentCameraMoveDir = WorldPointDelta;
                    ClampCameraInBorders();

                    LastTouchUpdateTime = Time.time;

                }               
            }
            if (newTouch.phase == TouchPhase.Ended)
            {
                print(CurrentCameraMoveDir);
                print(CurrentCameraMoveDir.magnitude);
                print(Time.time - LastTouchUpdateTime);
                print(CurrentCameraMoveDir.magnitude / (Time.time - LastTouchUpdateTime));
                CurrentCameraSpeed = CurrentCameraMoveDir.magnitude /(Time.time - LastTouchUpdateTime);
                CurrentCameraSpeed = Mathf.Clamp(CurrentCameraSpeed, 0, MaxCameraSpeed);
                CurrentCameraMoveDir = CurrentCameraMoveDir.normalized;
            }
            oldTouch = newTouch;
        }

        if (Input.touchCount == 2)
        {
            if(Input.touches[1].phase == TouchPhase.Began)
            {
                oldTouch = Input.touches[0];
                
            }
            if (!Equals(Input.touches[0], oldTouch))
            {
                if(Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
                {
                    Vector2 pos0 = Input.touches[0].position;
                    Vector2 pos1 = Input.touches[1].position;
                    Vector2 pos0b = Input.touches[0].position - Input.touches[0].deltaPosition;
                    Vector2 pos1b = Input.touches[1].position - Input.touches[1].deltaPosition;

                    float zoom = Vector2.Distance(pos0b, pos1b) / Vector2.Distance(pos0, pos1);

                    MainCamera.transform.position = new Vector3(MainCamera.transform.position.x,
                                                                Mathf.Clamp(MainCamera.transform.position.y * zoom, MinHeight, MaxHeight),
                                                                MainCamera.transform.position.z);
                }
                oldTouch = Input.touches[0];
            }
            if(Input.touches[1].phase == TouchPhase.Ended)
            {
            }
        }
    }

    private void SpeedUpdate()
    {
        //print(CurrentCameraSpeed);
        if (CurrentCameraSpeed < 0.1f) return;
        MainCamera.transform.position = MainCamera.transform.position + CurrentCameraMoveDir * CurrentCameraSpeed * Time.deltaTime;
        CurrentCameraSpeed -= CameraDrag * Time.deltaTime;
        if (CurrentCameraSpeed < 0) CurrentCameraSpeed = 0;
        ClampCameraInBorders();
    }

    private void ClampCameraInBorders()
    {
        MainCamera.transform.position = new Vector3(
                                        Mathf.Clamp(MainCamera.transform.position.x, FirstBorderPoint.x, SecondBorderPoint.x),
                                        MainCamera.transform.position.y,
                                        Mathf.Clamp(MainCamera.transform.position.z, FirstBorderPoint.y, SecondBorderPoint.y)
                                        );
    }

    private Vector3 GetWorldPointDeltaFromTouch(Touch touch)
    {
        Vector3 oldPoint = GetPlanePointFromScreenPoint(touch.position - touch.deltaPosition);
        Vector3 newPoint = GetPlanePointFromScreenPoint(touch.position);

        WorldTouchPoint.transform.position = newPoint;
        Vector3 direction = oldPoint - newPoint;
        return direction;
    }

    private Vector3 GetPlanePointFromScreenPoint(Vector2 ScreenPoint)
    {
        Ray FromCamera = MainCamera.ScreenPointToRay(ScreenPoint);
        if (PlaneForInterception.Raycast(FromCamera, out float DistanceToPlane))
            return FromCamera.GetPoint(DistanceToPlane);

        return Vector3.zero;
    }

}
