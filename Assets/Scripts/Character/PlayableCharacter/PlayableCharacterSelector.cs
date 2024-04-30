using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PlayableCharacterSelector : MonoBehaviour
{
    private void Start()
    {
        FindAnyObjectByType<PlayableCharacter>().FSM.SwitchState<PlayableCharacterInputState>();
    }
}
