using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : PhotographableObject
{
    private PhotographableObject photograph;
    public string Name;

    void Start()
    {
        photograph = GetComponent<PhotographableObject>(); 
        //photograph._renderer = GetComponent<Renderer>();
    }

    protected void Photograph()
    {
        
    }
}

