using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOverlay : MonoBehaviour
{
    [SerializeField]
    private Material lineMaterial;

    public Color gridColor;

    private Camera mainCam;

    [SerializeField]
    private Vector2 lowerBound;

    [SerializeField]
    private Vector2 upperBound;

    private void Start()
    {
        mainCam = GetComponent<Camera>();

        lowerBound = transform.InverseTransformPoint(mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)));
        upperBound = transform.InverseTransformPoint(mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0)));
    }

    public void OnPostRender()
    {
        lowerBound = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        upperBound = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        GL.PushMatrix();
        GL.LoadOrtho();
        GL.Begin(GL.LINES);
        lineMaterial.SetPass(0);

        GL.Color(gridColor);
        // Vertical lines
        for (int x = (int)lowerBound.x; x <= (int)upperBound.x + 1; x++)
        {
            Vector3 v = mainCam.WorldToViewportPoint(new Vector3(x - .5f, lowerBound.y - .5f, -10));
            Vector3 v1 = mainCam.WorldToViewportPoint(new Vector3(x - .5f, upperBound.y + .5f, -10));

            GL.Vertex(v);
            GL.Vertex(v1);
        }
        // Vertical lines
        for (int y = (int)lowerBound.y; y <= (int)upperBound.y + 1; y++)
        {
            Vector3 v = mainCam.WorldToViewportPoint(new Vector3(lowerBound.x - .5f, y - .5f, -10));
            Vector3 v1 = mainCam.WorldToViewportPoint(new Vector3(upperBound.x + .5f, y - .5f, -10));

            GL.Vertex(v);
            GL.Vertex(v1);
        }
        // Horizontal lines
        GL.End();
        GL.PopMatrix();
    }
}