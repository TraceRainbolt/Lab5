using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
    public ParticleSystem healthParticles;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.8f, 0.1f, 1);

        Destroy(transform.gameObject, 10f);
    }

    public void Kill()
    {
        ParticleSystem particles = Instantiate(healthParticles, transform.position, Quaternion.identity);
        Destroy(particles.gameObject, 0.3f);
        Destroy(gameObject);
    }
}
