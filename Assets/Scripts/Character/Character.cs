using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public abstract class Character : PlacedOnGridObject
{
    public ReactiveProperty<int> MaxHealthPoints;
    public ReactiveProperty<int> RemainingHealthPoints;

    public ReactiveProperty<int> MaxMovementPoints;
    public ReactiveProperty<int> RemainingMovementPoints;

    public ReactiveProperty<int> MaxActionPoints;

    public ReactiveProperty<int> RemainingActionPoints;

    public CharacterVisualizer Visualizer;

    public static Action<Character> CursorEntered;
    public static Action<Character> CursorExited;
    public static Action<Character> Clicked;

    public CharacterFSM FSM { get; protected set; }

    protected void Update()
    {
        FSM.Update();
    }
}
