using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.InputSystem
{
    public class MouseClick
    {
        public string Button;
        public string Phase;
        public GameObject ClickedObject;

        public MouseClick(InputAction.CallbackContext context)
        {
            Button = context.control.name;
            Phase = context.phase.ToString();
            ClickedObject = null;
        }

        public MouseClick(InputAction.CallbackContext context, GameObject clickedObject)
        {
            Button = context.control.name;
            Phase = context.phase.ToString();
            ClickedObject = clickedObject;
        }

        public static class ButtonName
        {
            public const string LEFT = "leftButton";
            public const string MIDDLE = "middleButton";
            public const string RIGHT = "rightButton";
        }

        public static class PhaseName
        {
            public const string STARTED = "Started";
            public const string PERFORMED = "Performed";
            public const string CANCELED = "Canceled";
        }
    }
}
