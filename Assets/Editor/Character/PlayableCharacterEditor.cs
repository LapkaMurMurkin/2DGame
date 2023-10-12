using UnityEngine;

using UnityEditor;

using Game.HexGrid;
using Game.Character;
using Game.InputSystem;

[CustomEditor(typeof(PlayableCharacter))]
public class PlayableCharacterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayableCharacter playableCharacter = (PlayableCharacter)target;

        if (GUILayout.Button("InitializeCharacterPositionOnGrid"))
        {
            Raycaster raycaster = new Raycaster();
            Undo.RegisterChildrenOrderUndo(playableCharacter, "InitializeCharacterPositionOnGrid");

            if (raycaster.CastRayDownOnCellLayer(playableCharacter.transform.position + new Vector3(0, 0.1f, 0)))
            {
                GameObject cellObject = raycaster.GetRayHitObject();
                Cell cellComponent = cellObject.GetComponent<Cell>();

                playableCharacter.transform.position = cellObject.transform.position;
                playableCharacter.CharacterPositionOnGrid = cellComponent;
            }

            PrefabUtility.RecordPrefabInstancePropertyModifications(playableCharacter);
        }
    }
}