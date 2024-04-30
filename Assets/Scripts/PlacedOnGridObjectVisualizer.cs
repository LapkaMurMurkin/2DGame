using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedOnGridObjectVisualizer
{
    protected Material BaseMaterial;

    public PlacedOnGridObjectVisualizer(Material material)
    {
        BaseMaterial = material;
    }

    public virtual void EnableHighlight()
    {
        BaseMaterial.SetColor("_Color", Color.green);
    }

    public virtual void DisableHighlight()
    {
        BaseMaterial.SetColor("_Color", Color.white);
    }
}
