using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeamBehavior : MonoBehaviour {

    public int thrust = 100;
    public int pull = 10;
    public ParticleSystem hitParticles;

    private bool firstHit = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(transform.gameObject, 1f);
        rb.AddForce(transform.right * thrust);
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PowerUp") 
            || collider.gameObject.CompareTag("Health")
            || collider.gameObject.CompareTag("Explosive"))
        {
            if (firstHit)
            {
                collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                firstHit = false;
            }

            Vector3 dir = (new Vector3(0, 0, 0) - collider.gameObject.transform.position).normalized;
            Destroy(transform.gameObject);
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pull);

            ParticleSystem hit = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(hit.gameObject, 0.3f);

        }
    }
}
 