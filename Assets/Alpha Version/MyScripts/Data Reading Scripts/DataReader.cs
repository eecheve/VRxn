using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class DataReader : MonoBehaviour
{
    [Header ("Input")]
    [SerializeField] private TextAsset textAsset = null;

    [Header("Output")]
    [SerializeField] private ChartData chartData = null;

    private string textString;

    private List<float> allValues = new List<float>();
    private List<float> coordinates = new List<float>();
    private List<float> energies = new List<float>();

    private List<Vector2> energiesAndCoordinates = new List<Vector2>();

    public void PopulateVector2List()
    {
        textString = textAsset.text;
        string[] lines = Regex.Split(textString, "\n|\r|\r\n"); //splits string on each line

        for (int i = 4; i < lines.Length; i++) //in the 5th line begins the numerical data for the irc calculations
        {
            string[] entries = Regex.Split(lines[i], " "); //separates each line by spaces
            foreach (string entry in entries)
            {
                if (!string.IsNullOrEmpty(entry)) //checks for empty values
                {
                    float.TryParse(entry, out float value); //gets numerical value
                    allValues.Add(value);
                }
            }
        }

        coordinates = allValues.Where((x, i) => i % 2 == 0).ToList(); //gets even values
        energies = allValues.Where((x, i) => i % 2 != 0).ToList(); //gets odd values

        for (int i = 0; i < coordinates.Count; i++)
        {
            energiesAndCoordinates.Add(new Vector2(coordinates[i], energies[i]));
        }
        chartData.SetPoints(energiesAndCoordinates);
    }
}
