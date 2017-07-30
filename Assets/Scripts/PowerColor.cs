using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerColor : MonoBehaviour
{
    Material material;

    // Use this for initialization
    void Awake () {
        material = GetComponent<Renderer>().material;
     }
	
	// Update is called once per frame
	public void PowerUpdate(float current, float max)
    {
        float percent = current / max;
        Color col = Color.white;
        col.b = col.g = percent;
        material.color = col;		
	}
}
