using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

public class HexGrid : MonoBehaviour
{
    public static List<Cell> Cells = new List<Cell>();

    public HexGridVisualizer Visualizer = new HexGridVisualizer();
}
