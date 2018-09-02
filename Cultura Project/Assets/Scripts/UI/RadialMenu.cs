using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class RadialMenu : SerializedMonoBehaviour
{
    private enum Type { Alpha, Beta }
    public int number = 3;
    public float radius = 200f;
    [SerializeField]private Type variant;
    public Button[] buttons;
    private float theta;
    [SerializeField]private GameObject textTipObj;
    private TextMeshProUGUI textTip;

    private void Start()
    {
        if (textTipObj != null)
        {
            textTip = textTipObj.GetComponent<TextMeshProUGUI>() ?? textTipObj.AddComponent<TextMeshProUGUI>();
            textTip.text = "";
        }
        else Debug.Log("Radial menu '" + this.name + "' is missing a text field");
    }

    public void updateText(string txt)
    {
        textTip.text = txt;
    }

    [Button]
    void PlaceButtons()
    {
        if (textTipObj != null)
        {
            textTip = textTipObj.GetComponent<TextMeshProUGUI>() ?? textTipObj.AddComponent<TextMeshProUGUI>();
            textTipObj.transform.localPosition = new Vector2(0, 0);
        }
        else Debug.Log("Radial menu '" + this.name + "' is missing a text field");
            
        foreach (Button button in buttons)
        {
            if (button == null)
            {
                Debug.Log("Cannot place buttons without objects!");
                return;
            }
            RadialButton buttonScript = button.gameObject.GetComponent<RadialButton>() ?? button.gameObject.AddComponent<RadialButton>();
            buttonScript.setMenuParent(this.gameObject);
        }

        if (variant == Type.Alpha)
        {
            float thetap = theta / 2;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.localPosition = new Vector2(radius * Mathf.Sin((theta * (i + 1)) - thetap), radius * Mathf.Cos((theta * (i + 1)) - thetap));
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.localPosition = new Vector2(radius * Mathf.Sin(theta * i), radius * Mathf.Cos(theta * i));
            }
        }
        
    }

    [Button]
	void Refresh()
    {
        Button[] temp = buttons;
        buttons = new Button[number];
        for(int i = 0; i < temp.Length; i++)
        {
            if (i >= buttons.Length) return;
            buttons[i] = temp[i];
        }
        theta = 360f / number;
        theta = (theta * Mathf.PI) / 180f;

        PlaceButtons();
	}
}
