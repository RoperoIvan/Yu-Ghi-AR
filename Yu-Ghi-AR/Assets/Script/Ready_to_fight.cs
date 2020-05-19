using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System;


public class Ready_to_fight : Monsters
{

    enum RoundState { P1Selecting, P2Selecting, PreSummoned, Summoned, Nothingness};
    public enum MiniGames { ChargeAttack, FastAttack};

    public GameObject player1;
    public GameObject player2;

    public Button selectP1;
    public Button selectP2;
    public Button invoke_button;
    public Button fight_button;
    public Button minipress_button_p1;
    public Button minipress_button_p2;
    public UnityEngine.UI.Image minibar_p1;
    public UnityEngine.UI.Image minibar_p2;
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
    GameObject winner = null;
    RoundState state = RoundState.P1Selecting;
    MiniGames mini_games = MiniGames.FastAttack;

    //2nd minigame
    public Button p1_pink_button;
    public Button p2_pink_button;
    public Button p1_blue_button;
    public Button p2_blue_button;
    public GameObject icons;

    private void Awake()
    {
        audio_source = GetComponent<AudioSource>();
        audio_source_p1 = player1.GetComponent<AudioSource>();
        audio_source_p2 = player2.GetComponent<AudioSource>();
    }
    void Start()
    {
        minipress_button_p1.gameObject.SetActive(false);
        minipress_button_p2.gameObject.SetActive(false);
        minibar_p1.gameObject.SetActive(false);
        minibar_p2.gameObject.SetActive(false);
        TurnPlayer1();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case RoundState.P1Selecting:
                monster_player1 = SearchTarget(player1);
                if (monster_player1 != null)
                {
                    monster_player1 = CheckTargetInCamera(monster_player1);
                    if (!selectP1.IsActive())
                    {
                        selectP1.gameObject.SetActive(true);
                        select_cardP1.gameObject.SetActive(true);
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
                monster_player2 = SearchTarget(player2);
                if (monster_player2 != null)
                {
                    monster_player2 = CheckTargetInCamera(monster_player2);
                    if (!selectP1.IsActive())
                    {
                        selectP2.gameObject.SetActive(true);
                        select_cardP2.gameObject.SetActive(true);
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
            case RoundState.Nothingness:
                if (fight_button.enabled)
                    state = RoundState.Summoned;
                break;
            case RoundState.Summoned:
                ManageMiniGames();
                break;
        }

    }

    private void ManageMiniGames()
    {
        switch (mini_games)
        { 
            case MiniGames.ChargeAttack:
                if(minipress_button_p1.gameObject.GetComponent<PressMiniGame>().GetValue() >= 100)
                {
                    monster_player1.GetComponent<Invoke_dragon>().Attack();
                    monster_player2.GetComponent<Invoke_dragon>().SetMonsterWin(false);
                    minipress_button_p1.GetComponent<PressMiniGame>().ResetBar();
                    minipress_button_p2.GetComponent<PressMiniGame>().ResetBar();
                    minipress_button_p1.gameObject.SetActive(false);
                    minipress_button_p2.gameObject.SetActive(false);
                    minibar_p1.gameObject.SetActive(false);
                    minibar_p2.gameObject.SetActive(false);
                    player1.GetComponent<PointCounter>().WinRound();
                    audio_source.clip = win_round_clip;
                    audio_source.Play();
                    Invoke("NewRound", 3f);
                }
                if (minipress_button_p2.gameObject.GetComponent<PressMiniGame>().GetValue() >= 100)
                {
                    monster_player2.GetComponent<Invoke_dragon>().Attack();
                    monster_player1.GetComponent<Invoke_dragon>().SetMonsterWin(false);
                    minipress_button_p1.GetComponent<PressMiniGame>().ResetBar();
                    minipress_button_p2.GetComponent<PressMiniGame>().ResetBar();
                    minipress_button_p1.gameObject.SetActive(false);
                    minipress_button_p2.gameObject.SetActive(false);
                    minibar_p1.gameObject.SetActive(false);
                    minibar_p2.gameObject.SetActive(false);
                    player2.GetComponent<PointCounter>().WinRound();
                    audio_source.clip = win_round_clip;
                    audio_source.Play();
                    Invoke("NewRound", 3f);
                }
                break;
            case MiniGames.FastAttack:
                if (p1_pink_button.gameObject.GetComponent<SpeedMiniGame>().GetValue() >= 100)
                {
                    monster_player1.GetComponent<Invoke_dragon>().Attack();
                    monster_player2.GetComponent<Invoke_dragon>().SetMonsterWin(false);
                    p1_pink_button.GetComponent<SpeedMiniGame>().ResetBar();
                    p2_pink_button.GetComponent<SpeedMiniGame>().ResetBar();
                    p1_blue_button.GetComponent<SpeedMiniGame>().ResetBar();
                    p2_blue_button.GetComponent<SpeedMiniGame>().ResetBar();
                    p1_pink_button.gameObject.SetActive(false);
                    p2_pink_button.gameObject.SetActive(false);
                    p1_blue_button.gameObject.SetActive(false);
                    p2_blue_button.gameObject.SetActive(false);
                    icons.GetComponent<MyColor>().Disactive2Icons();
                    minibar_p1.gameObject.SetActive(false);
                    minibar_p2.gameObject.SetActive(false);
                    player1.GetComponent<PointCounter>().WinRound();
                    audio_source.clip = win_round_clip;
                    audio_source.Play();
                    Invoke("NewRound", 3f);

                }
                if (p2_pink_button.gameObject.GetComponent<SpeedMiniGame>().GetValue() >= 100)
                {
                    monster_player2.GetComponent<Invoke_dragon>().Attack();
                    monster_player1.GetComponent<Invoke_dragon>().SetMonsterWin(false);
                    p1_pink_button.GetComponent<SpeedMiniGame>().ResetBar();
                    p2_pink_button.GetComponent<SpeedMiniGame>().ResetBar();
                    p1_blue_button.GetComponent<SpeedMiniGame>().ResetBar();
                    p2_blue_button.GetComponent<SpeedMiniGame>().ResetBar();
                    p1_pink_button.gameObject.SetActive(false);
                    p2_pink_button.gameObject.SetActive(false);
                    p1_blue_button.gameObject.SetActive(false);
                    p2_blue_button.gameObject.SetActive(false);
                    icons.GetComponent<MyColor>().Disactive2Icons();
                    minibar_p1.gameObject.SetActive(false);
                    minibar_p2.gameObject.SetActive(false);
                    player2.GetComponent<PointCounter>().WinRound();
                    audio_source.clip = win_round_clip;
                    audio_source.Play();
                    Invoke("NewRound", 3f);
                }
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
        monster_player2.GetComponent<Invoke_dragon>().shooting.gameObject.SetActive(false);
        monster_player1.GetComponent<Invoke_dragon>().shooting.gameObject.SetActive(false);

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
        ChooseInvokeSound(audio_source_p1, monster_player1);
        ChooseInvokeSound(audio_source_p2, monster_player2);
        state = RoundState.Nothingness;
    }

    public void Fight(MiniGames m_game, float p1_value, float p2_value)
    {
        switch (m_game)
        {
            case MiniGames.ChargeAttack:
                PressMiniGame press1 = GameObject.Find("Button_MiniGame_P1").GetComponent<PressMiniGame>();
                PressMiniGame press2 = GameObject.Find("Button_MiniGame_P2").GetComponent<PressMiniGame>();
                press1.val = p1_value;
                press2.val = p2_value;
                break;
            case MiniGames.FastAttack:
                p1_pink_button.GetComponent<SpeedMiniGame>().val = p1_value * 8f;
                p2_pink_button.GetComponent<SpeedMiniGame>().val = p2_value * 8f;
                p1_blue_button.GetComponent<SpeedMiniGame>().val = p1_value * 8f;
                p2_blue_button.GetComponent<SpeedMiniGame>().val = p2_value * 8f;
                break;
        }
    }

    void NewRound()
    {

        monster_player1.GetComponent<Invoke_dragon>().PassRound();
        monster_player2.GetComponent<Invoke_dragon>().PassRound();
        TurnPlayer1();
        monster_player1 = null;
        monster_player2 = null;
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

    public void ManageAttacks()
    {
        state = RoundState.Summoned;

        int num = UnityEngine.Random.Range(0, 2);

        if (num == 0)
        {
            mini_games = MiniGames.ChargeAttack;
        }
        else if (num == 1)
        {
            mini_games = MiniGames.FastAttack;
        }
        //Random minigame
        ActiveMiniGames();
       
    }

    private void ActiveMiniGames()
    {
        switch (mini_games)
        {
            case MiniGames.ChargeAttack:
                minipress_button_p1.gameObject.SetActive(true);
                minipress_button_p2.gameObject.SetActive(true);
                minibar_p1.gameObject.SetActive(true);
                minibar_p2.gameObject.SetActive(true);
                break;
            case MiniGames.FastAttack:
                p1_pink_button.gameObject.SetActive(true);
                p2_pink_button.gameObject.SetActive(true);
                p1_blue_button.gameObject.SetActive(true);
                p2_blue_button.gameObject.SetActive(true);
                minibar_p1.gameObject.SetActive(true);
                minibar_p2.gameObject.SetActive(true);
                icons.GetComponent<MyColor>().Randomitzate();
                break;
        }
        MonsterTypes type_monster1 = monster_player1.GetComponent<Invoke_dragon>().type;
        MonsterTypes type_monster2 = monster_player2.GetComponent<Invoke_dragon>().type;
        switch (type_monster1) //Managing the relations between types 
        {
            case MonsterTypes.Fire:
                switch (type_monster2)
                {
                    case MonsterTypes.Water:
                        Fight(mini_games, 2f, 4f);
                        break;
                    case MonsterTypes.Dark:
                        Fight(mini_games, 4f, 2f);
                        break;

                }
                break;
            case MonsterTypes.Water:
                switch (type_monster2)
                {
                    case MonsterTypes.Fire:
                        Fight(mini_games, 4f, 2f);

                        break;
                    case MonsterTypes.Ground:
                        Fight(mini_games, 2f, 4f);
                        break;
                }
                break;
            case MonsterTypes.Ground:
                switch (type_monster2)
                {
                    case MonsterTypes.Water:
                        Fight(mini_games, 4f, 2f);

                        break;
                    case MonsterTypes.Dark:
                        Fight(mini_games, 2f, 4f);
                        break;
                }
                break;
            case MonsterTypes.Dark:
                switch (type_monster2)
                {
                    case MonsterTypes.Fire:
                        Fight(mini_games, 2f, 4f);
                        break;
                    case MonsterTypes.Ground:
                        Fight(mini_games, 4f, 2f);
                        break;
                }
                break;
        }
    }
}
