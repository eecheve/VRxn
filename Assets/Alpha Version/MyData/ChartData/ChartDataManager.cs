using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(UILineRenderer))]
public class ChartDataManager : MonoBehaviour
{
    [SerializeField] private ChartData chartData = null;

    public float MaxCoord { get; private set; }
    public float MaxEnergyAbs { get; private set; }
    public float MaxEnergy { get; private set; }

    public List<Vector2> NormalizedPoints { get; private set; } = new List<Vector2>();
    public ChartData ChartData { get { return chartData; } private set { chartData = value; } }

    private void OnEnable()
    {
        PopulateChartData();
    }

    public void PopulateChartData()
    {
        if (chartData != null)
        {
            //maxCoord = coordinates.Max();
            MaxCoord = chartData.Points.Max(point => point.x);
            MaxEnergy = chartData.Points.Max(point => point.y);
            MaxEnergyAbs = chartData.Points.Max(point => Mathf.Abs(point.y));
        }

        foreach (var point in chartData.Points)
        {
            float normX = point.x / MaxCoord;
            float normY = point.y / MaxEnergyAbs;
            Vector2 normPoint = new Vector2(normX, normY);

            if (!NormalizedPoints.Contains(normPoint))
            {
                NormalizedPoints.Add(normPoint);
                Debug.Log("ChartDataManager: adding a normalized point");
            }
        }

        GetComponent<UILineRenderer>().points = NormalizedPoints;

    }
    
    public void EmptyChartData()
    {
        MaxCoord = 1.0f;
        MaxEnergyAbs = 1.0f;

        NormalizedPoints.Clear();
    }
}
