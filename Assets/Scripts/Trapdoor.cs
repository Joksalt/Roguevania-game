using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var em = ps.emission;
        em.enabled = true;

        this.GetComponent<SpriteRenderer>().enabled = false;

        this.GetComponent<AudioSource>().Play();
    }
}
