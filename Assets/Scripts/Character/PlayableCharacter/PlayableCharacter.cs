using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableCharacter : Character, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public new CharacterVisualizer Visualizer;

    public static new Action<PlayableCharacter> CursorEntered;
    public static new Action<PlayableCharacter> CursorExited;
    public static new Action<PlayableCharacter> Clicked;

    private void Awake()
    {
        Visualizer = new CharacterVisualizer(this.transform.GetChild(0).GetComponent<Renderer>().material);

        MaxMovementPoints = new ReactiveProperty<int>(5);
        RemainingMovementPoints = new ReactiveProperty<int>(MaxMovementPoints.Value);

        MaxActionPoints = new ReactiveProperty<int>(1); ;
        RemainingActionPoints = new ReactiveProperty<int>(MaxActionPoints.Value);

        FSM = new CharacterFSM(this);
        FSM.InitializeState<CharacterIdleState>();
        FSM.InitializeState<PlayableCharacterInputState>();
        FSM.InitializeState<CharacterMovementState>();
        FSM.SwitchState<CharacterIdleState>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Visualizer.EnableHighlight();
        CursorEntered?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Visualizer.DisableHighlight();
        CursorExited?.Invoke(this);
    }
}
