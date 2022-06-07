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

    private bool _touchLocked;
    private bool _controlsLocked;
    private Vector3 _planeCenter;
    private Plane _planeForInterception;
    private float _currentCameraSpeed;
    private Vector3 _currentCameraMoveDir;
    private float _lastTouchUpdateTime;

    Touch oldTouch;
    bool hasMoved;

    public void Start()
    {
        if (MainCamera == null) MainCamera = Camera.main;
        if (PlaneCenterObject != null) _planeCenter = PlaneCenterObject.position;
            else _planeCenter = Vector3.zero;
        _planeForInterception = new Plane(Vector3.up, _planeCenter);
    }    

    public void Update()
    {

        if (Input.touchCount == 0) SpeedUpdate();

        if (_controlsLocked) return;

        if(Input.touchCount == 1)
        {
            Touch newTouch = Input.GetTouch(0);
            if (newTouch.phase == TouchPhase.Began)
            {
                _currentCameraMoveDir = Vector3.zero;
                _currentCameraSpeed = 0;
                hasMoved = false;
            }
            if (newTouch.phase == TouchPhase.Moved)
            {
                hasMoved = true;
                if (!Equals(oldTouch, newTouch))
                {
                    Vector3 WorldPointDelta = GetWorldPointDeltaFromTouch(Input.GetTouch(0));
                    MainCamera.transform.position += WorldPointDelta;
                    _currentCameraMoveDir = WorldPointDelta;
                    ClampCameraInBorders();

                    _lastTouchUpdateTime = Time.time;

                }               
            }
            if (newTouch.phase == TouchPhase.Ended)
            {
                if(!hasMoved)
                {
                    if (!_touchLocked)
                    {
                        Ray FromCamera = MainCamera.ScreenPointToRay(newTouch.position);
                        RaycastHit hit = new RaycastHit();
                        if (Physics.Raycast(FromCamera, out hit))
                        {
                            hit.transform.GetComponent<RaycastTarget>().OnPressed();
                        }
                    }                    
                }
                else
                { 
                    _currentCameraSpeed = _currentCameraMoveDir.magnitude /(Time.time - _lastTouchUpdateTime);
                    _currentCameraSpeed = Mathf.Clamp(_currentCameraSpeed, 0, MaxCameraSpeed);
                    _currentCameraMoveDir = _currentCameraMoveDir.normalized;
                }
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
        if (_currentCameraSpeed < 0.1f) return;
        MainCamera.transform.position = MainCamera.transform.position + _currentCameraMoveDir * _currentCameraSpeed * Time.deltaTime;
        _currentCameraSpeed -= CameraDrag * Time.deltaTime;
        if (_currentCameraSpeed < 0) _currentCameraSpeed = 0;
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

    public Vector3 GetPlanePointFromScreenPoint(Vector2 ScreenPoint)
    {
        Ray FromCamera = MainCamera.ScreenPointToRay(ScreenPoint);
        if (_planeForInterception.Raycast(FromCamera, out float DistanceToPlane))
            return FromCamera.GetPoint(DistanceToPlane);

        return Vector3.zero;
    }

    public void UpdateBorders(Vector2 newBorders)
    {

    }

    public void LockControls()
    {
        _controlsLocked = true;
    }

    public void UnLockControls()
    {
        _controlsLocked = false;
    }
    public void LockTouch()
    {
        _touchLocked = true;
    }
    public void UnLockTouch()
    {
        _touchLocked = false;
    }

}
