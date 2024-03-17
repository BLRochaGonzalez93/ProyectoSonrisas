using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectableObject : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Color originalColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void HighlightObject()
    {
        rend.material.color = highlightColor;
    }

    public void ResetColor()
    {
        rend.material.color = originalColor;
    }
}
