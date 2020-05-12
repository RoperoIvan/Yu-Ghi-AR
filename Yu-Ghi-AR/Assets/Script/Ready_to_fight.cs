using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class Ready_to_fight : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Button invoke_button;
    public Text select_card;

    // Start is called before the first frame update

    GameObject monster_player1 = null;
    GameObject monster_player2 = null;

    bool invoked = false;

    void Start()
    {
        if(invoke_button.enabled == true)
        {
            invoke_button.gameObject.SetActive(false);
            select_card.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (monster_player1 == null || monster_player2 == null)
        {
            invoke_button.gameObject.SetActive(false);
            select_card.gameObject.SetActive(true);
        }
        else
        {
            if (!invoked)
            {
                invoke_button.gameObject.SetActive(true);
                select_card.gameObject.SetActive(false);
                invoked = true;
            }

        }
        if (monster_player1 == null)
        {         
            //search target
            for(int i = 0; i < player1.transform.childCount; ++i)
            {
                GameObject current_object = player1.transform.GetChild(i).gameObject;
                //check if target is in camera
                if (current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.DETECTED ||
                    current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED ||
                   current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
                {
                    monster_player1 = current_object;
                    Debug.Log("Player1");
                    break;
                }
            }
        }

        if (monster_player2 == null)
        {
            for (int i = 0; i < player2.transform.childCount; ++i)
            {
                GameObject current_object = player2.transform.GetChild(i).gameObject;
                //check if target is in camera
                if (current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.DETECTED ||
                    current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED ||
                   current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
                {
                    monster_player2 = current_object;
                    Debug.Log("Player2");
                    break;
                }
            }
        }
    }

    public void InvokeDragon()
    {
        monster_player1.GetComponent<Invoke_dragon>().InitRing();
        monster_player2.GetComponent<Invoke_dragon>().InitRing();
    }

    public void Fight()
    {
       //start with monster 1
        
    }
}
