using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyColor : Monsters
{
    ButtomColor Color;

    public GameObject PinkIcon;
    public GameObject BlueIcon;

    public void Randomitzate()
    {
        int num = Random.RandomRange(0, 2);

        if(num == 0)
        {
            Color = ButtomColor.Pink;
            PinkIcon.SetActive(true);
            BlueIcon.SetActive(false);
        }
        else if (num == 1)
        {
            Color = ButtomColor.Blue;
            PinkIcon.SetActive(false);
            BlueIcon.SetActive(true);
        }
    }

    public ButtomColor GetColor()
    {
        return Color;
    }

    public void Disactive2Icons()
    {
        PinkIcon.SetActive(false);
        BlueIcon.SetActive(false);
    }
}
