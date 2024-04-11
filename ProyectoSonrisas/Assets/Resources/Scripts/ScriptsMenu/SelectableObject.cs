using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectableObject : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Color originalColor;
    private Renderer rend;
    private Transform playerTransform;

    [SerializeField] private GameObject loadingSprite;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        HideLoadingSprite();
    }

    public void HighlightObject()
    {
        rend.material.color = highlightColor;
        ShowLoadingSprite();
    }

    public void ResetColor()
    {
        rend.material.color = originalColor;
        HideLoadingSprite();
    }

    void ShowLoadingSprite()
    {
        if (loadingSprite != null)
        {
            loadingSprite.SetActive(true);
            loadingSprite.transform.position = transform.position ;
            loadingSprite.transform.LookAt(playerTransform);
        }
    }

    void HideLoadingSprite()
    {
        if (loadingSprite != null)
        {
            loadingSprite.SetActive(false);
        }
    }
}
