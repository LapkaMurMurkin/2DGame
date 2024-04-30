using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public static class MouseClick
{
    public static float LastClickTime { get; private set; } = 0;

    public static bool CheckDoubleClick(PointerEventData pointerEventData)
    {
        bool isDoubleClicked = (Time.unscaledTime - LastClickTime < Constants.DOUBLE_CLICK_THRESHOLD_TIME) && (pointerEventData.clickCount == 2);

        LastClickTime = Time.unscaledTime;

        if (isDoubleClicked)
            return true;
        else
            return false;
    }
}

