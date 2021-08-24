using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurvedText : Text
{
    public float diameter = 1.0f;
    public float segmentAngle = 180f;

    protected override void OnPopulateMesh(VertexHelper vh) //https://answers.unity.com/questions/1551552/how-to-create-360-curved-text.html
    {
        base.OnPopulateMesh(vh);

        for (int i = 0; i < vh.currentVertCount; i++)
        {
            UIVertex vert = UIVertex.simpleVert;
            vh.PopulateUIVertex(ref vert, i);
            Vector3 position = vert.position;

            float ratio = (float)position.x / preferredWidth;
            float mappedRatio = ratio * segmentAngle * Mathf.Deg2Rad;

            position.x = -Mathf.Cos(mappedRatio) * diameter;
            position.z = Mathf.Sin(mappedRatio) * diameter;

            vert.position = position;
            vh.SetUIVertex(vert, i);
        }
    }
}
