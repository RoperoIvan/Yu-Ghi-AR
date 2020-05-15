﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class Ready_to_fight : Monsters
{
    public GameObject player1;
    public GameObject player2;
    public Button invoke_button;
    public Button fight_button;
    public Text select_card;

    // Start is called before the first frame update

    GameObject monster_player1 = null;
    GameObject monster_player2 = null;

    bool invoked = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (monster_player1 == null || monster_player2 == null)
        {
            invoke_button.gameObject.SetActive(false);
            select_card.gameObject.SetActive(true);
            fight_button.gameObject.SetActive(false);

        }
        else
        {
            if (!invoked)
            {
                invoke_button.gameObject.SetActive(true);
                select_card.gameObject.SetActive(false);
            }

        }
        if (!invoked)
        {
            //search in monsters to player1
            if (monster_player1 == null)
            {
                monster_player1 = SearchTarget(player1);
            }
            else
            {
                //checck if target follow into camera
                monster_player1 = CheckTargetInCamera(monster_player1);
            }
            //serch in mosters to player2
            if (monster_player2 == null)
            {
                monster_player2 = SearchTarget(player2);
            }
            else
            {
                monster_player2 = CheckTargetInCamera(monster_player2);
            }
        }
    }

    public GameObject SearchTarget(GameObject player)
    {
        //search target
        for (int i = 0; i < player.transform.childCount; ++i)
        {
            GameObject current_object = player.transform.GetChild(i).gameObject;

            //check if target is in camera
            if (current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.DETECTED ||
                current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED ||
               current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                return current_object;
            }
        }
        return null;
    }

    public GameObject CheckTargetInCamera(GameObject target)
    {
        if (target.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.DETECTED ||
                target.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED ||
               target.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            return target;
        }
        return null;
    }

    public void InvokeDragon()
    {
        monster_player1.GetComponent<Invoke_dragon>().InitRing();
        monster_player2.GetComponent<Invoke_dragon>().InitRing();
        invoked = true;

    }

    public void Fight()
    {
        //take monsters types
        MonsterTypes type_monster1 = monster_player1.GetComponent<Invoke_dragon>().type;
        MonsterTypes type_monster2 = monster_player2.GetComponent<Invoke_dragon>().type;
        GameObject winner = null;
        switch(type_monster1)
        {
            case MonsterTypes.Fire:
                switch (type_monster2)
                {
                    case MonsterTypes.Water:
                        winner = monster_player2;
                        break;
                    case MonsterTypes.Dark:
                        winner = monster_player1;
                        break;

                }
                break;
            case MonsterTypes.Water:
                switch (type_monster2)
                {
                    case MonsterTypes.Fire:
                        winner = monster_player1;
                        break;
                    case MonsterTypes.Ground:
                        winner = monster_player2;
                        break;
                }
                break;
            case MonsterTypes.Ground:
                switch (type_monster2)
                {
                    case MonsterTypes.Water:
                        winner = monster_player1;
                        break;
                    case MonsterTypes.Dark:
                        winner = monster_player2;
                        break;
                }
                break;
            case MonsterTypes.Dark:
                switch (type_monster2)
                {
                    case MonsterTypes.Fire:
                        winner = monster_player2; 
                        break;
                    case MonsterTypes.Ground:
                        winner = monster_player1;
                        break;
                }
                break;
        }
        if(winner == monster_player1)
            monster_player1.GetComponentInParent<PointCounter>().WinRound();
        else if (winner == monster_player2)
            monster_player2.GetComponentInParent<PointCounter>().WinRound();

        NewRound();
        
    }

    void NewRound()
    {
        monster_player1.GetComponent<Invoke_dragon>().PassRound();
        monster_player2.GetComponent<Invoke_dragon>().PassRound();
        monster_player1 = null;
        monster_player2 = null;
        invoked = false;
        invoke_button.gameObject.SetActive(false);
        select_card.gameObject.SetActive(false);
        fight_button.gameObject.SetActive(false);
    }
}
