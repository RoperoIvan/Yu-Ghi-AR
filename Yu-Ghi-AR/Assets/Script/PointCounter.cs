using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    public Image v1_img;
    public Image v2_img;
    public Image v3_img;
    public Image d1_img;
    public Image d2_img;
    public Image d3_img;


    public Text player_wins_text;

    public int wins = 0;
    public int player_wins = 0;

    public GameObject otherPlayer;

    // Start is called before the first frame update
    void Start()
    {
        wins = 0;

        v1_img.enabled = false;
        v2_img.enabled = false;
        v3_img.enabled = false;

        d1_img.enabled = false;
        d2_img.enabled = false;
        d3_img.enabled = false;


        player_wins_text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (wins == 2)
        {
            Debug.Log("win");
            ShowWinText();
        }

        //win condition


    }

    public void WinRound()
    {
        ++wins;
        int total_rounds = wins + otherPlayer.GetComponent<PointCounter>().wins;
        
        switch(total_rounds)
        {
            case 1:
                v1_img.enabled = true;
                break;
            case 2:
                v2_img.enabled = true;
                break;
            case 3:
                v3_img.enabled = true;
                break;
        }

        otherPlayer.GetComponent<PointCounter>().LoseRound();
    }

    public void LoseRound()
    {
        int total_rounds = wins + otherPlayer.GetComponent<PointCounter>().wins;

        switch (total_rounds)
        {
            case 1:
                d1_img.enabled = true;
                break;
            case 2:
                d2_img.enabled = true;
                break;
            case 3:
                d3_img.enabled = true;
                break;
        }
    }

    public void ShowWinText()
    {
        player_wins_text.enabled = true;
    }
}
