using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TextInstructions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI textMesh = null;
    [SerializeField] private Image background = null;

    [Header("Instructions")]
    [TextArea] [SerializeField] private string[] instructions = null;

    private ClickButtonDetector buttonDetector;
    
    private Color initialColor;
    private Color finalColor = new Color(0, 0.21f, 0.65f);

    private void Awake()
    {
        buttonDetector = GetComponent<ClickButtonDetector>();
        
        textMesh.text = instructions[0];
        initialColor = background.color;
    }

    private void OnEnable()
    {
        buttonDetector.OnClickDetected += ChangeTextByButtonClicked;

        BottomVertexManager.OnFirstElementPlaced += ChangeTextByPlacingOneElement;
        BottomVertexManager.OnAllBottomElementsPlaced += ChangeTextAfterPlacingDieneBackbone;
        BottomVertexManager.OnFirstSingleBondMade += ChangeTextAfterMakingFirstSingleBond;
        BottomVertexManager.OnLastSingleBondMade += ChangeTextAfterMakingAllSingleBonds;
        BottomVertexManager.OnFirstDoubleBondMade += ChangeTextAfterMakingDoubleBond;
        BottomVertexManager.OnDieneCompleted += ChangeTextAfterDieneBackboneIsCompleted;

        TopVertexManager.OnElementsPlaced += ChangeTextAfterPlacingDienophileElements;
        TopVertexManager.OnSingleBondMade += ChangeTextAfterDienophileSingleBond;
        TopVertexManager.OnDoubleBondMade += ChangeTextAfterDienophileDoubleBond;

        OppositeVerticesConnector.OnBackboneCompleted += ChangeTextAfterBackboneCompleted;

        BottomOutsideManager.OnOutsideElementsPlaced += ChangeTextAfterOutsideElemensPlaced;
        BottomOutsideManager.OnSingleBond1Connected += ChangeTextAfterFirstOutsideSingleBond;
        BottomOutsideManager.OnAllBottomSubstituentsConnected += ChangeTextAfterOutsideSubstituentsConnected;

        TopOutsideManager.OnDrawingTaskCompleted += ChangeTextAfterTaskIsCompleted;

    }

    private void ChangeTextAfterTaskIsCompleted()
    {
        StartCoroutine(WaitSeconds());
        background.color = finalColor;

        textMesh.enabled = false;
        Debug.Log("TextInstructions: Step 17 - ChangeTextAfterTaskIsCompleted");
    }

    private void ChangeTextAfterOutsideSubstituentsConnected()
    {

        textMesh.text = instructions[15];
        Debug.Log("TextInstructions: Step 16 - ChangeTextAfterOutsideSubstituentsConnected");
    }

    private void ChangeTextAfterFirstOutsideSingleBond()
    {
        textMesh.text = instructions[14];
        Debug.Log("TextInstructions: Step 15 - ChangeTextAfterFirstOutsideSingleBond");
    }

    private void ChangeTextAfterOutsideElemensPlaced()
    {
        textMesh.text = instructions[13];
        Debug.Log("TextInstructions: Step 14 - ChangeTextAfterOutsideElemensPlaced");
    }

    private void ChangeTextAfterBackboneCompleted()
    {
        textMesh.text = instructions[11];
        Debug.Log("TextInstructions: Step 12 - ChangeTextAfterBackboneCompleted");
    }

    private void ChangeTextAfterDienophileDoubleBond()
    {
        textMesh.text = instructions[10];
        Debug.Log("TextInstructions: Step 11 - ChangeTextAfterDienophileDoubleBond");
    }

    private void ChangeTextAfterDienophileSingleBond()
    {
        textMesh.text = instructions[9];
        Debug.Log("TextInstructions: Step 10 - ChangeTextAfterDienophileSingleBond");
    }

    private void ChangeTextAfterPlacingDienophileElements()
    {
        textMesh.text = instructions[8];
        Debug.Log("TextInstructions: Step 9 - ChangeTextAfterPlacingDienophileElements");
    }

    private void ChangeTextAfterDieneBackboneIsCompleted()
    {
        textMesh.text = instructions[7];
        Debug.Log("TextInstructions: Step 8 - ChangeTextAfterDieneBackboneIsCompleted");
    }

    private void ChangeTextAfterMakingDoubleBond()
    {
        textMesh.text = instructions[6];
        Debug.Log("TextInstructions: Step 7 - ChangeTextAfterMakingDoubleBond");
    }

    private void ChangeTextAfterMakingAllSingleBonds()
    {
        textMesh.text = instructions[5];
        Debug.Log("TextInstructions: Step 6 - ChangeTextAfterMakingAllSingleBonds");
    }

    private void ChangeTextAfterMakingFirstSingleBond()
    {
        textMesh.text = instructions[4];
        Debug.Log("TextInstructions: Step 5 - ChangeTextAfterMakingFirstSingleBond");
    }

    private void ChangeTextAfterPlacingDieneBackbone()
    {
        textMesh.text = instructions[3];
        Debug.Log("TextInstructions: Step 4 - ChangeTextAfterPlacingDieneBackbone");
    }

    private void ChangeTextByPlacingOneElement()
    {
        textMesh.text = instructions[2];
        Debug.Log("TextInstructions: Step 3 - ChangeTextByPlacingOneElement");
    }

    private void ChangeTextByButtonClicked()
    {
        if (textMesh.text.Equals(instructions[0]))
        {
            textMesh.text = instructions[1];
            Debug.Log("TextInstructions: Step 2 - ChangeTextByButtonClicked");
        }
        else if(textMesh.text.Equals(instructions[11]))
        {
            textMesh.text = instructions[12];
            buttonDetector.RestartClickDetection();
            Debug.Log("TextInstructions: Step 13 - ChangeTextByButtonClicked");
        }
    }

    public void ResetTextInstructions()
    {
        textMesh.enabled = true;

        textMesh.text = instructions[0];
        background.color = initialColor;
        Debug.Log("TextInstructions: Step 1");
    }

    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(3.0f);
    }

    private void OnDisable()
    {
        buttonDetector.OnClickDetected -= ChangeTextByButtonClicked;

        BottomVertexManager.OnFirstElementPlaced -= ChangeTextByPlacingOneElement;
        BottomVertexManager.OnAllBottomElementsPlaced -= ChangeTextAfterPlacingDieneBackbone;
        BottomVertexManager.OnFirstSingleBondMade -= ChangeTextAfterMakingFirstSingleBond;
        BottomVertexManager.OnLastSingleBondMade -= ChangeTextAfterMakingAllSingleBonds;
        BottomVertexManager.OnFirstDoubleBondMade -= ChangeTextAfterMakingDoubleBond;
        BottomVertexManager.OnDieneCompleted -= ChangeTextAfterDieneBackboneIsCompleted;

        TopVertexManager.OnElementsPlaced -= ChangeTextAfterPlacingDienophileElements;
        TopVertexManager.OnSingleBondMade -= ChangeTextAfterDienophileSingleBond;
        TopVertexManager.OnDoubleBondMade -= ChangeTextAfterDienophileDoubleBond;

        OppositeVerticesConnector.OnBackboneCompleted -= ChangeTextAfterBackboneCompleted;

        BottomOutsideManager.OnOutsideElementsPlaced -= ChangeTextAfterOutsideElemensPlaced;
        BottomOutsideManager.OnSingleBond1Connected -= ChangeTextAfterFirstOutsideSingleBond;
        BottomOutsideManager.OnAllBottomSubstituentsConnected -= ChangeTextAfterOutsideSubstituentsConnected;

        TopOutsideManager.OnDrawingTaskCompleted -= ChangeTextAfterTaskIsCompleted;
    }
}
