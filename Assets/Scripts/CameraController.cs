using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private Vector3 _touchStart;
    public GraphicRaycaster graphicRaycaster;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float zoomSpeed = 1.0f;

    private bool _touchStarted = false;

    private Camera _camera;

    private BuildController _buildController;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _buildController = FindObjectOfType<BuildController>();
    }

    private void Update () {
        if(Input.GetMouseButtonDown(0)){
            //_touchStart = _camera.ScreenToWorldPoint(Input.mousePosition);
            _touchStart = _camera.ScreenToWorldPoint(Input.mousePosition);
            _touchStarted = true;
            
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
            _touchStarted = true;
        }
        else
        {
            var direction = _touchStart - _camera.ScreenToWorldPoint(Input.mousePosition);
            if (_touchStarted && direction.magnitude < 0.5f)
            {
                RaycastHit hit;
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
                if (Physics.Raycast(ray, out hit, 100.0f)
                    && hit.transform.CompareTag("Slot") && !isOverUI)
                {
                    _buildController.TryBuild(hit.collider);
                }

            }
            _touchStarted = false;
        }
        
        
        Zoom(Input.GetAxis("Mouse ScrollWheel") * 3.0f);
    }

    private void Zoom(float increment){
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - increment * zoomSpeed, zoomOutMin, zoomOutMax);
    }
}
