using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawingPointsManager : MonoBehaviour
{
    [SerializeField] private Button resetButton = null;
    
    public BottomVertexManager BottomVertexManager { get; private set; }
    public TopVertexManager TopVertexManager { get; private set; }
    public OppositeVerticesConnector OppositeVerticesConnector { get; private set; }
    public BottomOutsideManager BottomOutsideManager { get; private set; }
    public TopOutsideManager TopOutsideManager { get; private set; }
    public BottomAxialManager BottomAxialManager { get; private set; }
    public TopAxialManager TopAxialManager { get; private set; }

    private void Awake()
    {
        BottomVertexManager = GetComponent<BottomVertexManager>();
        TopVertexManager = GetComponent<TopVertexManager>();
        OppositeVerticesConnector = GetComponent<OppositeVerticesConnector>();
        BottomOutsideManager = GetComponent<BottomOutsideManager>();
        TopOutsideManager = GetComponent<TopOutsideManager>();
        BottomAxialManager = GetComponent<BottomAxialManager>();
        TopAxialManager = GetComponent<TopAxialManager>();
    }

    private void OnEnable()
    {
        resetButton.onClick.AddListener(BottomVertexManager.ResetHighlighters);
        resetButton.onClick.AddListener(TopVertexManager.ResetHighlighters);
        resetButton.onClick.AddListener(OppositeVerticesConnector.ResetHighlights);
        resetButton.onClick.AddListener(BottomOutsideManager.ResetHighlights);
        resetButton.onClick.AddListener(TopOutsideManager.ResetHighlighters);
        resetButton.onClick.AddListener(BottomAxialManager.ResetHighlighters);
        resetButton.onClick.AddListener(TopAxialManager.ResetHighlighters);
    }

    private void OnDisable()
    {
        resetButton.onClick.RemoveListener(BottomVertexManager.ResetHighlighters);
        resetButton.onClick.RemoveListener(TopVertexManager.ResetHighlighters);
        resetButton.onClick.RemoveListener(OppositeVerticesConnector.ResetHighlights);
        resetButton.onClick.RemoveListener(BottomOutsideManager.ResetHighlights);
        resetButton.onClick.RemoveListener(TopOutsideManager.ResetHighlighters);
        resetButton.onClick.RemoveListener(BottomAxialManager.ResetHighlighters);
        resetButton.onClick.RemoveListener(TopAxialManager.ResetHighlighters);
    }
}
