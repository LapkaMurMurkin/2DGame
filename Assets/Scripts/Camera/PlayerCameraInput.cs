using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerCameraInput : MonoBehaviour
{
    [SerializeField]
    private PlayerCamera _playerCamera;

    private ActionsMap _actionsMap;

    private InputAction _movement;
    private InputAction _rotation;
    private InputAction _zoom;

    private void Awake()
    {
        _playerCamera = FindObjectOfType<PlayerCamera>();

        _actionsMap = new ActionsMap();

        _movement = _actionsMap.Camera.Movement;
        _rotation = _actionsMap.Camera.Rotation;
        _zoom = _actionsMap.Camera.Zoom;
    }

    private void OnEnable()
    {
        _actionsMap.Enable();
        _rotation.performed += UpdateCameraRotation;
        _zoom.performed += UpdateCameraZoom;
    }

    private void OnDisable()
    {
        _actionsMap.Disable();
        _rotation.performed -= UpdateCameraRotation;
        _zoom.performed -= UpdateCameraZoom;
    }

    private void Update()
    {
        UpdateCameraPosition(_movement.ReadValue<Vector3>());
    }

    public void UpdateCameraPosition(Vector3 movementDirectionFlatInput)
    {
        Vector3 cameraActualForwardVector = _playerCamera.transform.forward;
        Vector3 cameraActualRightVector = _playerCamera.transform.right;

        Vector3 actualMovementVector = movementDirectionFlatInput.x * cameraActualRightVector + movementDirectionFlatInput.z * cameraActualForwardVector;
        actualMovementVector.y = 0;

        actualMovementVector = actualMovementVector.normalized;

        _playerCamera.Position += actualMovementVector * Time.deltaTime * _playerCamera.TranslationSpeed;
    }

    public void UpdateCameraRotation(InputAction.CallbackContext cursorDelta)
    {
        if (Mouse.current.middleButton.isPressed)
        {
            Vector3 rotationOffset = new Vector3(cursorDelta.ReadValue<Vector2>().y, cursorDelta.ReadValue<Vector2>().x, 0f);
            _playerCamera.Rotation += rotationOffset * Time.deltaTime * _playerCamera.RotationSpeed;
        }
    }

    private void UpdateCameraZoom(InputAction.CallbackContext wheelValue)
    {
        _playerCamera.Zoom += wheelValue.ReadValue<Vector2>().y / 120 * _playerCamera.ZoomSpeed;
    }

}

