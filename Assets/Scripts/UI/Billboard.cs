using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform _mainCamera;
    void Start()
    {
        _mainCamera = Camera.main.transform;
    }

    void Update()
    {
        transform.rotation = _mainCamera.rotation;
    }
}
