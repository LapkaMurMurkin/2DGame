using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform _cameraTransform;
    private Transform _cameraOffsettedPivot;

    public Vector3 Position
    {
        get => _cameraOffsettedPivot.position;
        set => _cameraOffsettedPivot.position = value;
    }
    public int TranslationSpeed = 10;

    public Vector3 Rotation
    {
        get => _cameraOffsettedPivot.rotation.eulerAngles;
        set => _cameraOffsettedPivot.rotation = ClampCameraRotation(value);

    }
    public int RotationSpeed = 10;
    private float _verticalRotationMinClamp = 10;
    private float _verticalRotationMaxClamp = 80;

    public float Zoom
    {
        get => _cameraTransform.localPosition.z;
        set => _cameraTransform.localPosition = new Vector3(0, 0, ClampCameraZoom(value));
    }
    public int ZoomSpeed = 1;
    private float _minZoom = -20;
    private float _maxZoom = -5;

    private void Awake()
    {
        _cameraOffsettedPivot = this.transform;
        _cameraTransform = this.GetComponentInChildren<Camera>().transform;
    }

    private Quaternion ClampCameraRotation(Vector3 rotation)
    {
        rotation.x = Math.Clamp(rotation.x, _verticalRotationMinClamp, _verticalRotationMaxClamp);
        rotation.z = 0;

        return Quaternion.Euler(rotation);
    }

    private float ClampCameraZoom(float zoom)
    {
        zoom = Math.Clamp(zoom, _minZoom, _maxZoom);
        return zoom;
    }
}
