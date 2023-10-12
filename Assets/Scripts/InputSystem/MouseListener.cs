using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.InputSystem
{
    public class MouseListener : MonoBehaviour
    {
        private ActionsMap _actionsMap;
        private Raycaster _raycaster;
        
        private GameObject _rayHitObject = null;

        public static UnityEvent<MouseClick> MouseClicked = new UnityEvent<MouseClick>();
        public static UnityEvent<GameObject> CellFound = new UnityEvent<GameObject>();

        private void Awake()
        {
            _actionsMap = new ActionsMap();
            _raycaster = new Raycaster();
        }

        private void OnEnable()
        {
            _actionsMap.Enable();

            _actionsMap.Mouse.Click.started += SendMouseClicked;
            _actionsMap.Mouse.Click.performed += SendMouseClicked;
            _actionsMap.Mouse.Click.canceled += SendMouseClicked;
        }

        private void OnDisable()
        {
            _actionsMap.Disable();

            _actionsMap.Mouse.Click.started -= SendMouseClicked;
            _actionsMap.Mouse.Click.performed -= SendMouseClicked;
            _actionsMap.Mouse.Click.canceled -= SendMouseClicked;
        }

        private void SendMouseClicked(InputAction.CallbackContext context)
        {
            MouseClick mouseClick = new MouseClick(context);

            _raycaster.InitializeCursorRay();

            if (_raycaster.CastCursorRay())
                mouseClick.ClickedObject = _raycaster.GetRayHitObject();

            MouseClicked.Invoke(mouseClick);

            //Debug.Log(mouseClick.Button + " " + mouseClick.Phase + " " + mouseClick.ClickedObject);
        }

        void Update()
        {
/*             _raycaster.CastCursorRay();

            if (_raycaster.CheckRayHitInCell())
            {
                GameObject rayHitObjectBuffer = _raycaster.GetRayHitObject();

                if (_rayHitObject != rayHitObjectBuffer)
                {
                    _rayHitObject = rayHitObjectBuffer;
                    CellFound.Invoke(_rayHitObject);
                    Debug.Log(_rayHitObject);
                }
            }*/
        } 
    }
}