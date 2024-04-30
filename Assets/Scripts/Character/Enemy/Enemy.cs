using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : Character, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public new EnemyVisualizer Visualizer;

    public static new Action<Enemy> CursorEntered;
    public static new Action<Enemy> CursorExited;
    public static new Action<Enemy, PointerEventData> Clicked;

    private void Awake()
    {
        Visualizer = new EnemyVisualizer(this.GetComponent<Renderer>().material);

        FSM = new CharacterFSM(this);
        FSM.InitializeState<CharacterIdleState>();
        FSM.SwitchState<CharacterIdleState>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(this, eventData);
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
