using Cultura.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMPro.TMP_Dropdown))]
public class SelectionModeDropdown : MonoBehaviour
{
    private TMPro.TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<TMPro.TMP_Dropdown>();

        SelectionManager.Instance.SelectionModeChangeEventHandler += (s) => dropdown.SetValue((int)s);
    }
}