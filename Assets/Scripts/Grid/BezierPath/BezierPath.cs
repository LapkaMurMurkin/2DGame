using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BezierPath : MonoBehaviour
{
    [Range(0.05f, 1)]
    [SerializeField]
    private float _resolutionOfPath;

    public float ResolutionOfPath
    {
        get { return _resolutionOfPath; }
        set
        {
            if (value < 0.05f)
                _resolutionOfPath = 0.05f;
            else if (value > 1)
                _resolutionOfPath = 0.1f;
            else
                _resolutionOfPath = value;
        }
    }

    private LineRenderer lineRenderer;

    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private Vector3 Lerp(Vector3 startPoint, Vector3 endPoint, float percentageOfPath)
    {
        return startPoint + (endPoint - startPoint) * percentageOfPath;
    }

    private Vector3 QuadraticCurvePoint(Vector3 startPoint, Vector3 endPoint, Vector3 controlPoint, float percentageOfPath)
    {
        Vector3 intermediatePoint1 = Lerp(startPoint, controlPoint, percentageOfPath);
        Vector3 intermediatePoint2 = Lerp(controlPoint, endPoint, percentageOfPath);

        return Lerp(intermediatePoint1, intermediatePoint2, percentageOfPath);
    }

    private Vector3 CubicCurvePoint(Vector3 startPoint, Vector3 endPoint, Vector3 startControlPoint, Vector3 endControlPoint, float percentageOfPath)
    {
        Vector3 intermediatePoint1 = QuadraticCurvePoint(startPoint, endControlPoint, startControlPoint, percentageOfPath);
        Vector3 intermediatePoint2 = QuadraticCurvePoint(startControlPoint, endPoint, endControlPoint, percentageOfPath);

        return Lerp(intermediatePoint1, intermediatePoint2, percentageOfPath);
    }

    public void ShowMovementPath(List<Cell> movementPath)
    {
        List<Vector3> intermediatePoints = new List<Vector3>();
        List<Vector3> bezierPath = new List<Vector3>();

        for (int i = 0; i < movementPath.Count - 1; i++)
        {
            intermediatePoints.Add(Lerp(movementPath[i].transform.localPosition, movementPath[i + 1].transform.localPosition, 0.5f));
        }

        for (int i = 0; i < intermediatePoints.Count - 1; i++)
        {
            for (float step = 0f; step <= 1f; step += ResolutionOfPath)
            {
                Vector3 point = QuadraticCurvePoint(intermediatePoints[i], intermediatePoints[i + 1], movementPath[i + 1].transform.localPosition, step);
                bezierPath.Add(point);
            }
        }

        lineRenderer.positionCount = bezierPath.Count;
        for (int i = 0; i < bezierPath.Count; i++)
        {
            lineRenderer.SetPosition(i, bezierPath[i]);
        }
    }
}
