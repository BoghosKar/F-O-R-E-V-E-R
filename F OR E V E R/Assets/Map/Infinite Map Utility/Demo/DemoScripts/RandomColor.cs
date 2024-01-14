using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public Color []colorsused;
    public Renderer OBJRenderer;
    void Start()
    {
        OBJRenderer.material.color = colorsused[Random.Range(0, colorsused.Length -1)];
    }

}
