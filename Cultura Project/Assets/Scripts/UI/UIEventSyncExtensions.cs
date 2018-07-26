using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIEventSyncExtensions
{
    private static Slider.SliderEvent emptySliderEvent = new Slider.SliderEvent();

    public static void SetValue(this Slider instance, float value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptySliderEvent;
        instance.value = value;
        instance.onValueChanged = originalEvent;
    }

    private static Toggle.ToggleEvent emptyToggleEvent = new Toggle.ToggleEvent();

    public static void SetValue(this Toggle instance, bool value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptyToggleEvent;
        instance.isOn = value;
        instance.onValueChanged = originalEvent;
    }

    private static InputField.OnChangeEvent emptyInputFieldEvent = new InputField.OnChangeEvent();

    public static void SetValue(this InputField instance, string value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptyInputFieldEvent;
        instance.text = value;
        instance.onValueChanged = originalEvent;
    }

    private static Dropdown.DropdownEvent emptyDropdownFieldEvent = new Dropdown.DropdownEvent();

    public static void SetValue(this Dropdown instance, int value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptyDropdownFieldEvent;
        instance.value = value;
        instance.onValueChanged = originalEvent;
    }

    private static TMPro.TMP_Dropdown.DropdownEvent emptyTMDropdownFieldEvent = new TMPro.TMP_Dropdown.DropdownEvent();

    public static void SetValue(this TMPro.TMP_Dropdown instance, int value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptyTMDropdownFieldEvent;
        instance.value = value;
        instance.onValueChanged = originalEvent;
    }

    // TODO: Add more UI types here.
}