using UnityEngine;
using UnityEngine.UI;

public class Invoke_dragon : Monsters
{
    public ParticleSystem ring;
    public ParticleSystem hide_dragon;
    public GameObject dragon;
    public Button fight_button;
    float timer;
    public float magic_cicle_time = 4.0f;
    public float appear_dragon = 2.0f;
    float actual_time;
    public ParticleSystem shooting;
    public MonsterTypes type = MonsterTypes.NoType;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer >= appear_dragon *0.5 && Time.time - timer <= ((appear_dragon * 0.5)+0.2f))
        {
            hide_dragon.gameObject.SetActive(true);
        }
        if (Time.time - timer >= appear_dragon)
        {
            dragon.SetActive(true);
        }
        if (Time.time - timer >= magic_cicle_time && Time.time - timer <= (magic_cicle_time+0.2f))
        {
            ring.gameObject.SetActive(false);
            hide_dragon.gameObject.SetActive(false);
            fight_button.gameObject.SetActive(true);
        }
    }

    public void InitRing()
    {
        ring.gameObject.SetActive(true);
        this.gameObject.GetComponent<Invoke_dragon>().enabled = true;
        ring.Play();
        timer = Time.time;
    }

    public void PassRound()
    {
        dragon.SetActive(false);
        this.gameObject.GetComponent<Invoke_dragon>().enabled = false;

    }

    public void Attack()
    {
        dragon.GetComponent<Animator>().SetBool("isShooting", true);
        shooting.gameObject.SetActive(true);
        shooting.Stop();
        shooting.Play();
    }

    public void SetMonsterWin(bool win)
    {
        dragon.GetComponent<Animator>().SetBool("Win", win);
       
    }

    public bool isAnimFinished(string animName)
    {
        if (dragon.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animName) && dragon.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length >
            dragon.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
            return false;

        return true;
    }
}
