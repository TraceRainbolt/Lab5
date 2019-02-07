using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehavior : MonoBehaviour
{

    public ParticleSystem powerupExplode;

    void Start()
    {
        Vector3 dir = (new Vector3(0, 0, 0) - transform.position).normalized;
        GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.7f, 0.9f, 1);

        Destroy(transform.gameObject, 10f);
    }

    public void Kill()
    {
        ParticleSystem particles = Instantiate(powerupExplode, transform.position, Quaternion.identity);
        Destroy(particles.gameObject, 0.3f);
        Destroy(gameObject);
    }
}
