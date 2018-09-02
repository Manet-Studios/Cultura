using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    [SerializeField]private GameObject menuParent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuParent.GetComponent<RadialMenu>().updateText(this.gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menuParent.GetComponent<RadialMenu>().updateText("");
    }

    public void setMenuParent(GameObject obj)
    {
        menuParent = obj;
    }
}
