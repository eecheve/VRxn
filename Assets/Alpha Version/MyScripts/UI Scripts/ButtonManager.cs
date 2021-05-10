using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonManager : MonoBehaviour
{
    public UnityEvent actionIfOn;
    public UnityEvent actionIfOff;

    [Header("Button Attributes")]
    [SerializeField] private Button button = null;
    [SerializeField] private Color colorIfOn = Color.white;
    [SerializeField] private Color colorIfOff = Color.white;

    private bool propertyOff = true;

    private void OnEnable()
    {
        button.onClick.AddListener(ManageButton);
    }

    public void ManageButton()
    {
        if(propertyOff == true)
        {
            button.targetGraphic.color = colorIfOn;
            actionIfOn.Invoke();
            propertyOff = false;
        }
        else
        {
            button.targetGraphic.color = colorIfOff;
            actionIfOff.Invoke();
            propertyOff = true;
        }
    }

    public void ManageInvertedButton()
    {
        if (propertyOff == true)
        {
            button.targetGraphic.color = colorIfOff;
            actionIfOn.Invoke();
            propertyOff = false;
        }
        else
        {
            button.targetGraphic.color = colorIfOn;
            actionIfOff.Invoke();
            propertyOff = true;
        }
    }

    public void ResetButton()
    {
        button.targetGraphic.color = colorIfOff;
        actionIfOff?.Invoke();
        propertyOff = true;
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ManageButton);
    }

}
