using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour {

    public int thrust = 100;
    public ParticleSystem hitParticles;

    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        Destroy(transform.gameObject, 1f);
		rb.AddForce(transform.right * thrust);
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            ParticleSystem hit = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(hit.gameObject, 0.3f);
            collision.gameObject.GetComponent<EnemyBehavior>().HandleHit();
            GameControl.instance.totalEnemies++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    { 
        if (collider.CompareTag("Explosive"))
        {
            collider.GetComponent<ExplosiveBehavior>().Explode();
        }
    }
}
