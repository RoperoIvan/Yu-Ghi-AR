using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter : MonoBehaviour
{

    public int wins = 0;

    // Start is called before the first frame update
    void Start()
    {
        wins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (wins == 2)
            Debug.Log("win");
            //win condition
    }

    public void WinRound()
    {
        ++wins;
    }
}
