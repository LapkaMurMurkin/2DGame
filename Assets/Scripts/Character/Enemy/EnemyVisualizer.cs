using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisualizer : CharacterVisualizer
{
    public EnemyVisualizer(Material material) : base(material) { }

    public override void EnableHighlight()
    {
        BaseMaterial.SetColor("_Color", Color.red);
    }

    public override void DisableHighlight()
    {
        BaseMaterial.SetColor("_Color", Color.white);
    }
}
