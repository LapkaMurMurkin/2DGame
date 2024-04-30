using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject _pauseMenu;
    private bool _isPauseMenuOpen;
    private ActionsMap _actionsMap;

    private void Awake()
    {
        _actionsMap = new ActionsMap();
        _isPauseMenuOpen = false;
        _pauseMenu.SetActive(_isPauseMenuOpen);
    }

    private void OnEnable()
    {
        _actionsMap.Enable();

        _actionsMap.Keyboard.Pause.performed += SwitchPauseMenuState;
    }

    private void OnDisable()
    {
        _actionsMap.Disable();

        _actionsMap.Keyboard.Pause.performed -= SwitchPauseMenuState;
    }

    public void SwitchPauseMenuState(InputAction.CallbackContext context)
    {
        _isPauseMenuOpen = !_isPauseMenuOpen;
        _pauseMenu.SetActive(_isPauseMenuOpen);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log(eventData);
    }
}
