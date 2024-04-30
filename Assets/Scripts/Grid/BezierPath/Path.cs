using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Path : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField]
    float t = 0;

    [SerializeField]
    Transform[] points = new Transform[4];

    Vector3 GetPosition(int i)
    {
        return points[i].position;
    }

    public void OnDrawGizmos()
    {
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawSphere(GetPosition(i), 0.1f);
        }

        Handles.DrawBezier(GetPosition(0), GetPosition(3), GetPosition(1), GetPosition(2), Color.red, EditorGUIUtility.whiteTexture, 1f);

        Vector3 movementPoint = GetBezierPoints(t);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(movementPoint, 0.1f);
        Gizmos.color = Color.white;

    }

    Vector3 GetBezierPoints(float t)
    {
        Vector3 point0 = GetPosition(0);
        Vector3 point1 = GetPosition(1);
        Vector3 point2 = GetPosition(2);
        Vector3 point3 = GetPosition(3);

        Vector3 a = Vector3.Lerp(point0, point1, t);
        Vector3 b = Vector3.Lerp(point1, point2, t);
        Vector3 c = Vector3.Lerp(point2, point3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
    }
}
