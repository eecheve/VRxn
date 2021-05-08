using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private int maxValue;

    private void Start()
    {
        slider = GetComponent<Slider>();
        maxValue = InterviewQuestionDataList.Instance.InterviewQuestions.Count-1;

        slider.wholeNumbers = true;
        slider.minValue = 0;
        slider.maxValue = maxValue;
    }

    public void UpdateSlider(int value)
    {
        if(slider!=null)
            slider.value = value;
    }
}
