using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMiniGame : MonoBehaviour
{
    public GameObject healthBar;
    SimpleHealthBar gmae;
    public float val = 2f;
    float value = 0f;
    // Start is called before the first frame update
    void Start()
    {
        gmae = healthBar.GetComponent<SimpleHealthBar>();
    }
    // Update is called once per frame
    void Update()
    {
        if(value > 0)
        {
            value -= 0.01f;
            gmae.UpdateBar(value, 100);
        }

    }
    public void PressPoints()
    {
        if(value <= 100)
        {
            value += val;
            gmae.UpdateBar(value, 100);
        }
           
    }
    public void ResetBar()
    {
        val = 2f;
        value = 0f;
        gmae.UpdateBar(0f, 100);
    }
    public float GetValue()
    {
        return value;
    } 
}
