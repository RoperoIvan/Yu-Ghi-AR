using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedMiniGame : Monsters
{
    public GameObject healthBar;
    SimpleHealthBar gmae;
    public float val = 16f;
    float value = 0f;

    public ButtomColor button_color = ButtomColor.Pink;

    public GameObject Center_Button;

    public Button team_button;

    // Start is called before the first frame update
    void Start()
    {
        gmae = healthBar.GetComponent<SimpleHealthBar>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void PressPoints()
    {
        if (Center_Button.GetComponent<MyColor>().GetColor() == button_color)
        {
            if (value <= 100)
            {
                value += val;
                gmae.UpdateBar(value, 100);
            }
        }
        else
        {
            if (value >= 0)
            {
                value -= val;
                gmae.UpdateBar(value, 100);
            }
        }

        team_button.GetComponent<SpeedMiniGame>().value = value;
        Center_Button.GetComponent<MyColor>().Disactive2Icons();

        if (value < 100)
        {
            Invoke("NextButton", 1f);
        }
    }

    public void NextButton()
    {
        Center_Button.GetComponent<MyColor>().Randomitzate();
    }
    public void ResetBar()
    {
        val = 16f;
        value = 0f;
        gmae.UpdateBar(0f, 100);
    }
    public float GetValue()
    {
        return value;
    }

    
}
