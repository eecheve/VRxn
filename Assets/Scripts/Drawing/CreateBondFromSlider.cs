using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CreateBondFromSlider : MonoBehaviour
{
    [SerializeField] private LineRenderer line = null;
    [SerializeField] private Transform origin = null;
    [SerializeField] private Transform end = null;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(CreateBond);
    }

    private void CreateBond(float value)
    {
        if (value > 0)
        {
            line.SetPosition(0, origin.position);
            line.SetPosition(1, end.position);
        }
    }
}
