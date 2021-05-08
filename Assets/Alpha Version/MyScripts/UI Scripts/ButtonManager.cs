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

    private bool propertyOn = true;

    private void OnEnable()
    {
        button.onClick.AddListener(ManageButton);
    }

    public void ManageButton()
    {
        if(propertyOn == true)
        {
            button.targetGraphic.color = colorIfOn;
            actionIfOn.Invoke();
            propertyOn = false;
        }
        else
        {
            button.targetGraphic.color = colorIfOff;
            actionIfOff.Invoke();
            propertyOn = true;
        }
    }

    public void ManageInvertedButton()
    {
        if (propertyOn == true)
        {
            button.targetGraphic.color = Color.red;
            actionIfOn.Invoke();
            propertyOn = false;
        }
        else
        {
            button.targetGraphic.color = Color.green;
            actionIfOff.Invoke();
            propertyOn = true;
        }
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ManageButton);
    }

}
