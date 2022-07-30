using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float zoomSpeed = 1.0f;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update () {
        if(Input.GetMouseButtonDown(0)){
            //_touchStart = _camera.ScreenToWorldPoint(Input.mousePosition);
            _touchStart = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.touchCount == 2){
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }else if(Input.GetMouseButton(0)){
            //direction = _touchStart - _camera.ScreenToWorldPoint(Input.mousePosition);
            var direction = _touchStart - _camera.ScreenToWorldPoint(Input.mousePosition);
            direction.z += direction.y / 2.0f;
            direction.x += direction.y / 2.0f;
            direction.y = 0;
            _camera.transform.position += direction;
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel") * 3.0f);
    }

    private void Zoom(float increment){
        _camera.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment * zoomSpeed, zoomOutMin, zoomOutMax);
    }
}
