using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public int speed = 1000000;
    public static int maxHealth = 20;
    public ParticleSystem killParticles;

    public int health = maxHealth;

    private Rigidbody2D rb;

    void Start()
    {
        Vector3 dir = (new Vector3(0, 0, 0) - transform.position).normalized;
        float dist = Vector3.Distance(new Vector3(0, 0, 1), transform.position);

        GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.1f, 0.1f, 1);

        rb = GetComponent<Rigidbody2D>();

        Destroy(transform.gameObject, 10f);

        rb.AddForce(dir * speed * dist);
    }

    public void HandleHit()
    {
        health--;
        GetComponent<SpriteRenderer>().color = new Color(0.9f * maxHealth / health, 0.1f * maxHealth / health, 0.1f, 1);
        if (health == 0)
        {
            Kill();
        }
        GameControl.instance.AddScore(100);
    }

    public void Kill()
    {
        ParticleSystem particles = Instantiate(killParticles, transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
        GameControl.instance.AddScore(1000);
        Destroy(particles.gameObject, 0.3f);
        GameControl.instance.AddScore(1000);
    }
}
