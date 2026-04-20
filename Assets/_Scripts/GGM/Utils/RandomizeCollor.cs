using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeCollor : MonoBehaviour
{
    public Renderer objectRenderer;
    public float redRange = 255f;
    public float greenRange = 255f;
    public float blueRange = 255f;
    public float darkRange = 150f;
    
    // Start is called before the first frame update
    void Start()
    {
        float red = Random.Range(darkRange, redRange);
        float green = Random.Range(darkRange, greenRange);
        float blue = Random.Range(darkRange, blueRange);
        objectRenderer.material.color = new Color(red/255f, green/255f, blue/255f);
    }
}
