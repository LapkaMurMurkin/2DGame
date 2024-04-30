using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class CharacterPanel : MonoBehaviourSingleton<CharacterPanel>
{
    public Character SelectedCharacter { get; private set; }
    public TextMeshProUGUI CharacterName { get; private set; }
    public TextMeshProUGUI RemainingActionPoints { get; private set; }
    public TextMeshProUGUI RemainingMovementPoints { get; private set; }

    private new void Awake()
    {
        base.Awake();

        List<TextMeshProUGUI> UIFields = GetComponentsInChildren<TextMeshProUGUI>().ToList();

        CharacterName = UIFields.Find(gameObject => gameObject.name.Contains("CharacterName"));
        RemainingActionPoints = UIFields.Find(gameObject => gameObject.name.Contains("RemainingActionPoints"));
        RemainingMovementPoints = UIFields.Find(gameObject => gameObject.name.Contains("RemainingMovementPoints"));
    }

    public void BindPanelToCharacter(Character character)
    {
        if (SelectedCharacter is not null)
            UnbindPanel();

        SelectedCharacter = character;

        SelectedCharacter.RemainingActionPoints.Changed += UpdateRemainingActionPoints;
        SelectedCharacter.RemainingMovementPoints.Changed += UpdateRemainingMovementPoints;

        CharacterName.text = SelectedCharacter.name;
        RemainingActionPoints.text = SelectedCharacter.RemainingActionPoints.Value.ToString();
        RemainingMovementPoints.text = SelectedCharacter.RemainingMovementPoints.Value.ToString();
    }

    public void UnbindPanel()
    {
        SelectedCharacter.RemainingMovementPoints.Changed -= UpdateRemainingMovementPoints;
    }

    private void UpdateRemainingActionPoints(int newValue)
    {
        RemainingActionPoints.text = newValue.ToString();
    }

    private void UpdateRemainingMovementPoints(int newValue)
    {
        RemainingMovementPoints.text = newValue.ToString();
    }
}
