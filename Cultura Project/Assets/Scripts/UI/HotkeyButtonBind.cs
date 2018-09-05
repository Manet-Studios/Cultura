using UnityEngine;
using UnityEngine.EventSystems;

namespace Cultura.UI
{
    [RequireComponent(typeof(EventTrigger))]
    public class HotkeyButtonBind : MonoBehaviour
    {
        [SerializeField]
        private KeyCode hotkey;

        private bool hasTrigger = false;

        private EventTrigger button;

        private EventSystem eventSystem;

        private void Start()
        {
            button = GetComponent<EventTrigger>();
            eventSystem = EventSystem.current;
        }

        private void Update()
        {
            if (Input.GetKeyDown(hotkey))
            {
                ExecuteEvents.Execute(button.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerDownHandler);
            }
        }
    }
}