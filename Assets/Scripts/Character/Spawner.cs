using System.Collections.Generic;
using UnityEngine;

using Game.HexGrid;

namespace Game.Character
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private PlayableCharacter _character;

        public void Spawn(Cell cell, List<Cell> grid)
        {
            //_character.Grid = grid;
            _character.CharacterPositionOnGrid = cell;
            Instantiate(_character, cell.gameObject.transform.position, new Quaternion());
        }
    }
}