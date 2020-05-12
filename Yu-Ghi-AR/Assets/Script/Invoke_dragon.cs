using UnityEngine;

public class Invoke_dragon : MonoBehaviour
{
    public ParticleSystem ring;
    public ParticleSystem hide_dragon;
    public GameObject dragon;

    float timer;
    public float magic_cicle_time = 4.0f;
    public float appear_dragon = 2.0f;
    float actual_time;

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
        }
    }

    public void InitRing()
    {
        ring.gameObject.SetActive(true);
        this.gameObject.GetComponent<Invoke_dragon>().enabled = true;
        ring.Play();
        timer = Time.time;
    }
}
