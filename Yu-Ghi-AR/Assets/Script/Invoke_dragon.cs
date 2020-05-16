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

    public MonsterTypes type = MonsterTypes.NoType;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer >= appear_dragon *0.5)
        {
            hide_dragon.gameObject.SetActive(true);
        }
        if (Time.time - timer >= appear_dragon)
        {
            dragon.SetActive(true);
        }
        if (Time.time - timer >= magic_cicle_time)
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
}
