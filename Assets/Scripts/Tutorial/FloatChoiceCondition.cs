using UnityEngine;
using UnityEngine.UI;

public class FloatChoiceCondition : Condition
{
    [SerializeField] private RadialFill radialSlider = null;
    [SerializeField] [Range(0, 360)] private float targetAngle = 0;
    [SerializeField] private Button button = null;

    private float targetMin;
    private float targetMax;
    private float angle;

    private UpdateTextAfterCondition updateText;

    protected override void Awake()
    {
        base.Awake();
        updateText = GetComponent<UpdateTextAfterCondition>();
        
        targetMin = targetAngle - 15f;
        targetMax = targetAngle + 15f;
    }

    private void OnEnable()
    {
        radialSlider.OnValueChange += ListenForValueChange;
        button.onClick.AddListener(CheckForTargetAngle);
    }

    private void ListenForValueChange(float value)
    {
        angle = value * 360f;
    }

    private void CheckForTargetAngle()
    {
        Debug.Log("FloatChoiceCondition checking for angle: " + angle.ToString("0.00"));
        
        if(angle >= targetMin && angle <= targetMax)
        {
            Debug.Log("FloatChoiceCondition condition passed");
            
            if (updateText != null)
                updateText.UpdateText(true);

            condition.FulfillCondition();
            this.enabled = false;
        }
        else
        {
            if (updateText != null)
                updateText.UpdateText(false);
        }
    }

    private void OnDisable()
    {
        radialSlider.OnValueChange -= ListenForValueChange;
        button.onClick.RemoveListener(CheckForTargetAngle);
    }
}
