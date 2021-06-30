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
            Debug.Log(name + " ButtonManager: activating button");
            button.targetGraphic.color = colorIfOn;
            propertyOff = false;
            actionIfOn?.Invoke();
        }
        else
        {
            Debug.Log(name + " ButtonManager: deactivating button");
            button.targetGraphic.color = colorIfOff;
            propertyOff = true;
            actionIfOff?.Invoke();
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
