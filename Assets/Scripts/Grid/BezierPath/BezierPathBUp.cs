using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierPathBUp : MonoBehaviour
{
    public List<Transform> _points;// = new List<BezierPoint>();
    public List<Vector3> _controlPoints;// = new List<BezierPoint>();

    [SerializeField]
    private LineRenderer lineRenderer;

    [Range(0, 1)]
    [SerializeField]
    private float _controlToCenterOffset = 1;

    public Vector3 Lerp(Vector3 startPoint, Vector3 endPoint, float percentageOfPath)
    {
        return startPoint + (endPoint - startPoint) * percentageOfPath;
    }

    public Vector3 QuadraticCurvePoint(Vector3 startPoint, Vector3 endPoint, Vector3 controlPoint, float percentageOfPath)
    {
        Vector3 intermediatePoint1 = Lerp(startPoint, controlPoint, percentageOfPath);
        Vector3 intermediatePoint2 = Lerp(controlPoint, endPoint, percentageOfPath);

        return Lerp(intermediatePoint1, intermediatePoint2, percentageOfPath);
    }

    public Vector3 CubicCurvePoint(Vector3 startPoint, Vector3 endPoint, Vector3 startControlPoint, Vector3 endControlPoint, float percentageOfPath)
    {
        Vector3 intermediatePoint1 = QuadraticCurvePoint(startPoint, endControlPoint, startControlPoint, percentageOfPath);
        Vector3 intermediatePoint2 = QuadraticCurvePoint(startControlPoint, endPoint, endControlPoint, percentageOfPath);

        return Lerp(intermediatePoint1, intermediatePoint2, percentageOfPath);
    }

    public void OnDrawGizmos()
    {
        _controlPoints = new List<Vector3>();

        for (int i = 0; i < _points.Count - 1; i++)
        {
            _controlPoints.Add(Lerp(_points[i].position, _points[i + 1].position, 0.5f));
        }

        for (int i = 0; i < _points.Count; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_points[i].position, 0.1f);
            Gizmos.color = Color.white;
        }

        for (int i = 0; i < _controlPoints.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_controlPoints[i], 0.1f);
            Gizmos.color = Color.white;
        }

        List<Vector3> pathPoints = new List<Vector3>();

        for (int i = 0; i < _controlPoints.Count - 1; i++)
        {
            Vector3 startTangent = Lerp(_controlPoints[i], _points[i + 1].position, _controlToCenterOffset);
            Vector3 endTangent = Lerp(_controlPoints[i + 1], _points[i + 1].position, _controlToCenterOffset);

            Gizmos.DrawSphere(startTangent, 0.03f);
            Gizmos.DrawSphere(endTangent, 0.03f);

            Handles.DrawBezier(_controlPoints[i], _controlPoints[i + 1], startTangent, endTangent, Color.red, EditorGUIUtility.whiteTexture, 5f);

            for (float resolutionOfPath = 0; resolutionOfPath <= 1f; resolutionOfPath += 0.25f)
            {
                Vector3 point = QuadraticCurvePoint(_controlPoints[i], _controlPoints[i + 1], _points[i + 1].position, resolutionOfPath);

                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(point, 0.03f);
                Gizmos.color = Color.white;

                point = CubicCurvePoint(_controlPoints[i], _controlPoints[i + 1], startTangent, endTangent, resolutionOfPath);
                pathPoints.Add(point);


                Gizmos.color = Color.green;
                Gizmos.DrawSphere(point, 0.03f);
                Gizmos.color = Color.white;
            }
        }

        lineRenderer.positionCount = pathPoints.Count;
        for (int i = 0; i < pathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pathPoints[i]);
        }
    }
}
