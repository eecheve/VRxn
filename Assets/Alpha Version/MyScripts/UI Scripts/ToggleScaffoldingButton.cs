using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleScaffoldingButton : MonoBehaviour
{
    [SerializeField] private GameObject scaffolding = null;
    [SerializeField] private TextMeshProUGUI buttonText = null;

    private GameObject goRef;

    public void ShowScaffolding()
    {
        buttonText.text = "hide";
        goRef = Instantiate(scaffolding, new Vector3(0f,0f,0f), Quaternion.identity);
        Debug.Log("button is being listened");
    }

    public void HideScaffolding()
    {
        buttonText.text = "show";
        Destroy(goRef);
        goRef = new GameObject();
    }
}
