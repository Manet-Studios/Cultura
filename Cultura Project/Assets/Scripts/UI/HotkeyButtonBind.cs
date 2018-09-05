using UnityEngine;
using UnityEngine.UI;

namespace Cultura.UI
{
    [RequireComponent(typeof(Button))]
    public class HotkeyButtonBind : MonoBehaviour
    {
        [SerializeField]
        private KeyCode hotkey;

        private Button button;

        private void Start()
        {
            button = GetComponent<Button>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(hotkey))
            {
                button.onClick.Invoke();
            }
        }
    }
}