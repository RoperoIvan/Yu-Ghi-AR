using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class Ready_to_fight : Monsters
{

    enum RoundState { P1Selecting, P2Selecting, PreSummoned, Summoned};

    public GameObject monsters;
    public GameObject player1;
    public GameObject player2;

    public Button selectP1;
    public Button selectP2;
    public Button invoke_button;
    public Button fight_button;
    public Text select_cardP1;
    public Text select_cardP2;
    public Text prepare_fight;
    public AudioClip win_round_clip;
    public AudioClip water_summon_clip;
    public AudioClip fire_summon_clip;
    public AudioClip earth_summon_clip;
    public AudioClip dark_summon_clip;
    public AudioClip UI_audio_clip;
    public AudioClip UI_fight_audio_clip;
    AudioSource audio_source;
    AudioSource audio_source_p1;
    AudioSource audio_source_p2;

    GameObject monster_player1 = null;
    GameObject monster_player2 = null;

    RoundState state = RoundState.P1Selecting;

    private void Awake()
    {
        audio_source = GetComponent<AudioSource>();
        audio_source_p1 = player1.GetComponent<AudioSource>();
        audio_source_p2 = player2.GetComponent<AudioSource>();
    }
    void Start()
    {
        TurnPlayer1();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case RoundState.P1Selecting:
                monster_player1 = SearchTarget(monsters);
                if (monster_player1 != null)
                {
                    monster_player1 = CheckTargetInCamera(monster_player1);
                    if (!selectP1.IsActive())
                    {
                        selectP1.gameObject.SetActive(true);
                        select_cardP1.gameObject.SetActive(false);
                        ChooseInvokeSound(audio_source_p1, monster_player1);
                    }
                }
                if(monster_player1 == null && selectP1.IsActive())
                {
                    selectP1.gameObject.SetActive(false);
                    select_cardP1.gameObject.SetActive(true);

                }
                break;

            case RoundState.P2Selecting:
                monster_player2 = SearchTarget(monsters);
                if (monster_player2 != null)
                {
                    monster_player2 = CheckTargetInCamera(monster_player2);
                    if (!selectP1.IsActive())
                    {
                        selectP2.gameObject.SetActive(true);
                        select_cardP2.gameObject.SetActive(false);
                        ChooseInvokeSound(audio_source_p2, monster_player2);

                    }
                }
                if (monster_player2 == null && selectP2.IsActive())
                {
                    selectP2.gameObject.SetActive(false);
                    select_cardP2.gameObject.SetActive(true);
                }
                break;

            case RoundState.PreSummoned:

                if(CheckTargetInCamera(monster_player1) != null && CheckTargetInCamera(monster_player2) != null && !invoke_button.IsActive())
                {
                    invoke_button.gameObject.SetActive(true);
                    prepare_fight.gameObject.SetActive(false);
                }
                else if((CheckTargetInCamera(monster_player1) == null || CheckTargetInCamera(monster_player2) == null) && invoke_button.IsActive())
                {
                    invoke_button.gameObject.SetActive(false);
                    prepare_fight.gameObject.SetActive(true);
                }

                break;
            case RoundState.Summoned:
                break;
        }

    }
    //manage in buttons
    public void TurnPlayer1()
    {
        //audio_source.clip = UI_audio_clip;
        //audio_source.Play();
        invoke_button.gameObject.SetActive(false);
        select_cardP1.gameObject.SetActive(true);
        fight_button.gameObject.SetActive(false);
        select_cardP2.gameObject.SetActive(false);
        prepare_fight.gameObject.SetActive(false);
        selectP1.gameObject.SetActive(false);
        selectP2.gameObject.SetActive(false);

        state = RoundState.P1Selecting;
    }

    public void TurnPlayer2()
    {
        audio_source.clip = UI_audio_clip;
        audio_source.Play();
        invoke_button.gameObject.SetActive(false);
        select_cardP1.gameObject.SetActive(false);
        fight_button.gameObject.SetActive(false);
        select_cardP2.gameObject.SetActive(true);
        prepare_fight.gameObject.SetActive(false);
        selectP1.gameObject.SetActive(false);
        selectP2.gameObject.SetActive(false);

        state = RoundState.P2Selecting;
    }

    public void PreSummoned()
    {
        audio_source.clip = UI_audio_clip;
        audio_source.Play();
        invoke_button.gameObject.SetActive(false);
        select_cardP1.gameObject.SetActive(false);
        fight_button.gameObject.SetActive(false);
        select_cardP2.gameObject.SetActive(false);
        prepare_fight.gameObject.SetActive(true);
        selectP1.gameObject.SetActive(false);
        selectP2.gameObject.SetActive(false);

        state = RoundState.PreSummoned;
    }

    public GameObject SearchTarget(GameObject player)
    {
        //search target
        for (int i = 0; i < player.transform.childCount; ++i)
        {
            GameObject current_object = player.transform.GetChild(i).gameObject;

            //check if target is in camera
            if (current_object.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED)
            {
                return current_object;
            }
        }
        return null;
    }

    public GameObject CheckTargetInCamera(GameObject target)
    {
        if (target.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED)
        {
            return target;
        }
        return null;
    }

    public void InvokeDragon()
    {
        audio_source.clip = UI_fight_audio_clip;
        audio_source.Play();
        monster_player1.GetComponent<Invoke_dragon>().InitRing();
        monster_player2.GetComponent<Invoke_dragon>().InitRing();
        state = RoundState.Summoned;
        ChooseInvokeSound(audio_source_p1, monster_player1);
        ChooseInvokeSound(audio_source_p2, monster_player2);

    }

    public void Fight()
    {
        //take monsters types
        //audio_source.clip = UI_audio_clip;
        //audio_source.Play();
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
            player1.GetComponent<PointCounter>().WinRound();
        else if (winner == monster_player2)
            player2.GetComponent<PointCounter>().WinRound();

        NewRound();
        
    }

    void NewRound()
    {
        audio_source.clip = win_round_clip;
        audio_source.Play();
        monster_player1.GetComponent<Invoke_dragon>().PassRound();
        monster_player2.GetComponent<Invoke_dragon>().PassRound();
        monster_player1 = null;
        monster_player2 = null;

        TurnPlayer1();
    }
    void ChooseInvokeSound(AudioSource audio_src, GameObject monster)
    {
        MonsterTypes type_monster = monster.GetComponent<Invoke_dragon>().type;
        switch (state)
        {
            case RoundState.P1Selecting:
                switch (type_monster)
                {
                    case MonsterTypes.Fire:
                        //SOUND

                        break;
                    case MonsterTypes.Water:
                        //SOUND

                        break;
                    case MonsterTypes.Ground:
                        //SOUND

                        break;
                    case MonsterTypes.Dark:
                        //SOUND

                        break;
                }
                break;

            case RoundState.P2Selecting:
                switch (type_monster)
                {
                    case MonsterTypes.Fire:
                        //SOUND

                        break;
                    case MonsterTypes.Water:
                        //SOUND

                        break;
                    case MonsterTypes.Ground:
                        //SOUND

                        break;
                    case MonsterTypes.Dark:
                        //SOUND

                        break;
                }
                break;

            case RoundState.PreSummoned:
                switch (type_monster)
                {
                    case MonsterTypes.Fire:
                        //SOUND

                        break;
                    case MonsterTypes.Water:
                        //SOUND

                        break;
                    case MonsterTypes.Ground:
                        //SOUND

                        break;
                    case MonsterTypes.Dark:
                        //SOUND

                        break;
                }

                break;
            case RoundState.Summoned:
                switch (type_monster)
                {
                    case MonsterTypes.Fire:
                        //SOUND
                        audio_src.clip = fire_summon_clip;
                        audio_src.Play();
                        break;
                    case MonsterTypes.Water:
                        //SOUND
                        audio_src.clip = water_summon_clip;
                        audio_src.Play();
                        break;
                    case MonsterTypes.Ground:
                        //SOUND
                        audio_src.clip = earth_summon_clip;
                        audio_src.Play();
                        break;
                    case MonsterTypes.Dark:
                        //SOUND
                        audio_src.clip = dark_summon_clip;
                        audio_src.Play();
                        break;
                }
                break;
        }
       
    }
}
