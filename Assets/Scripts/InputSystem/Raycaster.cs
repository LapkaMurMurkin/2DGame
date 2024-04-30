using UnityEngine;


public class Raycaster
{
    private Camera mainCamera;
    private Ray _cursorRay;
    private RaycastHit rayHitInformation;

    public Raycaster()
    {
        mainCamera = Camera.main;
    }

    public void InitializeCursorRay()
    {
        _cursorRay = mainCamera.ScreenPointToRay(Input.mousePosition);
    }

    public bool CastRay(Vector3 startPoint, Vector3 direction)
    {
        return Physics.Raycast(startPoint, direction, out rayHitInformation, 100);
    }

    public bool CastRayDownOnCellLayer(Vector3 objectPosition)
    {
        return Physics.Raycast(objectPosition, Vector3.down, out rayHitInformation, 100, 1 << 10);
    }

    public bool CastCursorRay()
    {
        InitializeCursorRay();
        return Physics.Raycast(_cursorRay, out rayHitInformation, 100);
    }

    public bool CastCursorRayOnCellLayer()
    {
        return Physics.Raycast(_cursorRay, out rayHitInformation, 100, 1 << 10);
    }

    public RaycastHit GetRayHitInformation()
    {
        return rayHitInformation;
    }

    public GameObject GetRayHitObject()
    {
        return rayHitInformation.transform?.gameObject;
    }
}
