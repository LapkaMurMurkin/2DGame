using UnityEngine;
using UnityEngine.Events;
using TMPro;

using Game.Character;

namespace Game.InputSystem 
{
    public class PlayableCharacterInput : MonoBehaviour
    {
        private GameObject _selectedPlayableCharacter = null;
        [SerializeField] private TextMeshProUGUI CharacterText;

        private void OnEnable()
        {
            PlayableCharacter.PlayableCharacterSelected.AddListener(SetSelectedPlayableCharacter);
        }

        private void OnDisable()
        {
            PlayableCharacter.PlayableCharacterSelected.RemoveListener(SetSelectedPlayableCharacter);
        }
        
        private void SetSelectedPlayableCharacter(GameObject gameObject)
        {
            _selectedPlayableCharacter = gameObject;
            CharacterText.text = _selectedPlayableCharacter.ToString();
        }
    }
}