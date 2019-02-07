using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBehavior : MonoBehaviour {
    public ParticleSystem explosion;


    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.1f, 1);
        Destroy(gameObject, 10f);
    }

    public void Explode() {
        ParticleSystem hit = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(hit.gameObject, 0.43f);
        Destroy(gameObject);

        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, 4f);
        GameControl.instance.AddScore(1000);
        GameControl.instance.totalExplodes++;

        foreach (Collider2D result in results)
        {
            if (result.CompareTag("Enemy"))
            {
                result.GetComponent<EnemyBehavior>().Kill();
            }
            if (result.CompareTag("Health"))
            {
                result.GetComponent<HealthBehavior>().Kill();
            }
            if (result.CompareTag("Explosive"))
            {
                result.GetComponent<ExplosiveBehavior>().Explode();
            }
        }

    }
}
