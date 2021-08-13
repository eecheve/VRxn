using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SliderCheckbox : MonoBehaviour
{
    [SerializeField] private Slider activeSlider = null;
    [SerializeField] private Toggle inactiveCheckbox = null;
    [SerializeField] private Slider inactiveSlider = null;
    
    private Toggle toggle;

    private void OnEnable()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ToggleElement);
    }

    private void ToggleElement(bool state)
    {
        activeSlider.enabled = state;

        if (state == true && inactiveCheckbox != null)
        {
            if (inactiveSlider != null)
            {
                inactiveSlider.value = 0;
            }

            inactiveCheckbox.isOn = false;
        }
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(ToggleElement);
    }
}
