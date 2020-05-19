using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMiniGame : MonoBehaviour
{
    public int points = 0;
    public GameObject healthBar;
    SimpleHealthBar gmae;
    float value = 100;
    float timer = 100f;
    // Start is called before the first frame update
    void Start()
    {
        //timer = Time.realtimeSinceStartup;
        gmae = healthBar.GetComponent<SimpleHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(value < 100)
        {
            value += 0.01f;
            gmae.UpdateBar(value, 100);
        }

    }
    public void PressPoints()
    {
        if(value > 0)
            gmae.UpdateBar((value -=2),100);
    }
}
