using System;
using System.Collections.Generic;

using UnityEngine;

using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellVisualizer
{
    public Material BaseMaterial { get; private set; }
    private Color _blue = new Color32(0, 184, 255, 255);
    private Color _yellow = new Color32(255, 255, 0, 255);
    private Color _gray = new Color32(128, 128, 128, 255);

    public Material LastVisualBuffer { get; private set; }

    public CellVisualizer(Cell cell)
    {
        Material SharedMaterial = cell.GetComponent<Renderer>().sharedMaterial;
        BaseMaterial = cell.GetComponent<Renderer>().material = new Material(SharedMaterial);
        LastVisualBuffer = new Material(BaseMaterial);
    }

    public void SetVisualToStandart()
    {
        BaseMaterial.SetColor("_highlight_color", _blue);
        BaseMaterial.SetFloat("_highlight_opacity", 0f);

        BaseMaterial.SetColor("_path_color", _yellow);
        BaseMaterial.SetFloat("_path_opacity", 0f);
    }

    public void EnableRadiusHighlight()
    {
        BaseMaterial.SetColor("_highlight_color", _yellow);
        BaseMaterial.SetFloat("_highlight_opacity", 0.75f);
    }

    public void DisableRadiusHighlight()
    {
        BaseMaterial.SetColor("_highlight_color", _blue);
        BaseMaterial.SetFloat("_highlight_opacity", 0f);
    }

    public void SwitchPathHighlightToReachable()
    {
        BaseMaterial.SetColor("_path_color", _yellow);
        BaseMaterial.SetFloat("_path_opacity", 1f);
    }

    public void SwitchPathHighlightToUnreachable()
    {
        BaseMaterial.SetColor("_path_color", _gray);
        BaseMaterial.SetFloat("_path_opacity", 1f);
    }

    public void DisabelPathHighlight()
    {
        BaseMaterial.SetColor("_path_color", _yellow);
        BaseMaterial.SetFloat("_path_opacity", 0f);
    }

    public void EnableCursorHighlight()
    {
        LastVisualBuffer.CopyPropertiesFromMaterial(BaseMaterial);

        BaseMaterial.SetColor("_highlight_color", _blue);
        BaseMaterial.SetFloat("_highlight_opacity", 1f);
    }

    public void DisableCursorHighlight()
    {
        BaseMaterial.SetColor("_highlight_color", _blue);
        BaseMaterial.SetFloat("_highlight_opacity", 0f);

        BaseMaterial.CopyPropertiesFromMaterial(LastVisualBuffer);
    }
}
