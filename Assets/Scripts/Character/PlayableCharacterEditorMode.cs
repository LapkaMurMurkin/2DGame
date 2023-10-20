using UnityEngine;

using Game.HexGrid;
using Game.InputSystem;

namespace Game.Character
{
    [ExecuteAlways]
    public class PlayableCharacterEditorMode : MonoBehaviour
    {
        private PlayableCharacter _selfPlayableCharacter;

        private Raycaster _raycaster;

        private void Awake()
        {
            _selfPlayableCharacter = this.GetComponent<PlayableCharacter>();
            _raycaster = new Raycaster();
        }

        private void Start()
        {
            UpdateCoordinates();
        }

        private void Update()
        {
            if (transform.hasChanged)
            {
                UpdateCoordinates();
                transform.hasChanged = false;
            }
        }

        private void UpdateCoordinates()
        {
            if (_raycaster.CastRayDownOnCellLayer(_selfPlayableCharacter.transform.position + new Vector3(0, 0.1f, 0)))
            {
                GameObject cellObject = _raycaster.GetRayHitObject();
                Cell cellComponent = cellObject.GetComponent<Cell>();

                _selfPlayableCharacter.transform.position = cellObject.transform.position;
                _selfPlayableCharacter.CharacterPositionOnGrid = cellComponent;
            }
        }
    }
}