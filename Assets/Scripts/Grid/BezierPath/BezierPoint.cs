using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPoint{
    public Vector3 anchor;
    public Vector3 backControl;
    public Vector3 frontControl;

    public BezierPoint(Vector3 point)
    {
        anchor = point;
    }

}
