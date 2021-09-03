using TMPro;
using UnityEngine;

public class ElementInstantiatedTutorial : Condition
{
    [Header("Instantiated")]
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Vertex vertex = null;
    
    [Header("Highlighter")]
    [SerializeField] private SpriteRenderer highlightArrow = null;
    [SerializeField] private Vector3 arrowOffset = Vector3.zero;

    [Header("Optional")]
    [SerializeField] private TextMeshProUGUI tmesh = null;
    [SerializeField] [TextArea] private string correctText = "";
    [SerializeField] [TextArea] private string incorrectText = "";

    private HexagonalVertex hexVert;
    private TrigonalVertex trigVert;

    private void OnEnable()
    {
        if(vertex.GetType() == typeof(HexagonalVertex))
        {
            hexVert = (HexagonalVertex)vertex;
            hexVert.OnHexVertOccupied += CheckForInstantiated;
        }
        else if(vertex.GetType() == typeof(TrigonalVertex))
        {
            trigVert = (TrigonalVertex)vertex;
            trigVert.OnTrigVertOccupied += CheckForInstantiated;
        }

        ManageArrow(true);
        vertex.GetComponent<SpriteRenderer>().enabled = true;
        vertex.GetComponent<BoxCollider>().enabled = true;
    }

    private void ManageArrow(bool state)
    {
        if (highlightArrow != null)
        {
            highlightArrow.transform.position = vertex.transform.position + arrowOffset;
            highlightArrow.enabled = state;
        }
    }

    private void CheckForInstantiated()
    {
        if (vertex.Icon.name.Contains(prefab.name))
        {
            tmesh.text = correctText;
            condition.FulfillCondition();
            this.enabled = false;
        }
        else
        {
            tmesh.text = incorrectText;
        }
    }

    private void OnDisable()
    {
        if (vertex.GetType() == typeof(HexagonalVertex))
        {
            hexVert = (HexagonalVertex)vertex;
            hexVert.OnHexVertOccupied -= CheckForInstantiated;
        }
        else if (vertex.GetType() == typeof(TrigonalVertex))
        {
            trigVert = (TrigonalVertex)vertex;
            trigVert.OnTrigVertOccupied -= CheckForInstantiated;
        }

        vertex.GetComponent<SpriteRenderer>().enabled = false;
        ManageArrow(false);
    }
}
