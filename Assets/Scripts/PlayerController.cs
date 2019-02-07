using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public GameObject laser;
    public GameObject tractorBeam;
    public ParticleSystem laserShoot;
    public ParticleSystem tractorBeamShoot;
    public ParticleSystem powerupParticles;
    public ParticleSystem healthParticles;
    public ParticleSystem hitParticles;
    public ParticleSystem shipExplosion;

    public int health = 10;

    public Sprite defaultSprite;
    public Sprite gun2Sprite;
    public Sprite gun4Sprite;

    private Vector3 mousePosition;
    private int powerUp = 0;

    private static int initCooldown = 4;
    private int cooldown = initCooldown;

    private Color currColor;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 perpendicular = Vector3.Cross(transform.position - mousePos, Vector3.forward);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, perpendicular);
        transform.rotation = rot;

        Vector3 offsetPos = 0.6f * transform.right;

        if (Input.GetButton("Fire1") && cooldown == 0 && !Input.GetButton("Fire2"))
        {
            ParticleSystem particles = Instantiate(laserShoot, transform.position + offsetPos * 0.9f, Quaternion.identity);
            Destroy(particles.transform.gameObject, 0.3f);
            Instantiate(laser, transform.position + offsetPos, rot);

            if (powerUp == 0)
            {
                GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }

            if (powerUp > 0)
            {
                Quaternion rot2 = Quaternion.LookRotation(Vector3.back, perpendicular);
                GameObject powerLaser = Instantiate(laser, transform.position + offsetPos * -1, rot2);
                ParticleSystem particles2 = Instantiate(laserShoot, transform.position + offsetPos * -0.9f, Quaternion.identity);
                Destroy(particles2.transform.gameObject, 0.3f);
                Destroy(powerLaser.transform.gameObject, 1f);
                powerUp--;

                if (powerUp <= 300)
                {
                    GetComponent<SpriteRenderer>().sprite = gun2Sprite;
                }

            }
            if (powerUp > 300)
            {
                Vector3 offsetPosR = 0.6f * transform.up;
                GameObject powerLaser3 = Instantiate(laser, transform.position + offsetPosR, rot);
                Quaternion rotationAmount = Quaternion.Euler(0, 0, 90);
                Quaternion rotationAmount2 = Quaternion.Euler(0, 0, -90);
                powerLaser3.transform.rotation *= rotationAmount;

                ParticleSystem particles3 = Instantiate(laserShoot, transform.position + offsetPosR, Quaternion.identity);
                ParticleSystem particles4 = Instantiate(laserShoot, transform.position - offsetPosR, Quaternion.identity);
                Destroy(particles3.transform.gameObject, 0.3f);
                Destroy(particles4.transform.gameObject, 0.3f);

                GameObject powerLaser4 = Instantiate(laser, transform.position - offsetPosR, rot);
                powerLaser4.transform.rotation *= rotationAmount2;

                GetComponent<SpriteRenderer>().sprite = gun4Sprite;

            }
        }
        if (Input.GetButton("Fire2") && cooldown == 0)
        {
            ParticleSystem particles = Instantiate(tractorBeamShoot, transform.position + offsetPos * 0.9f, Quaternion.identity);
            Instantiate(tractorBeam, transform.position + offsetPos, rot);
            Destroy(particles.transform.gameObject, 0.3f);
        }
        cooldown--;
        if (cooldown < 0)
            cooldown = initCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            powerUp += 130;
            ParticleSystem particles = Instantiate(powerupParticles, transform.position, Quaternion.identity);
            Destroy(particles.gameObject, 0.3f);
            GetComponent<SpriteRenderer>().color = new Color(0, 0.7f, 0.9f, 1);
            currColor = new Color(10f / health, health / 10f, health / 10f);
            StartCoroutine(ChangeColorBack(0.1f, currColor));
            GameControl.instance.AddScore(10000);
            GameControl.instance.totalPowerups++;
        }
        if (collision.CompareTag("Health"))
        {
            Destroy(collision.gameObject);
            health += 2;
            if (health > 10)
                health = 10;
            ParticleSystem particles = Instantiate(healthParticles, transform.position, Quaternion.identity);
            Destroy(particles.gameObject, 0.3f);
            currColor = new Color(10f / health, health / 10f, health / 10f);
            GetComponent<SpriteRenderer>().color = new Color(0, 0.7f, 0, 1);
            StartCoroutine(ChangeColorBack(0.1f, currColor));
            GameControl.instance.AddScore(10000);
            GameControl.instance.totalHeals++;
        }
        if (collision.CompareTag("Enemy"))
        {
            health--;
            if (health == 0)
            {
                Instantiate(shipExplosion, transform.position, Quaternion.identity);
                transform.position = new Vector3(100000, 0, 0);
                BlowUpAll();
                GameControl.instance.EndGame();
            }

            Destroy(collision.gameObject);
            ParticleSystem particles = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(particles.gameObject, 0.3f);
            GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.6f, 0, 1);
            currColor = new Color(10f / health, health / 10f, health / 10f);
            StartCoroutine(ChangeColorBack(0.1f, currColor));
        }
    }

    IEnumerator ChangeColorBack(float time, Color color)
    {
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = color;
    }

    private void BlowUpAll()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyBehavior>().Kill();
        }

        GameObject[] powerUps;
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        foreach (GameObject powerUp in powerUps)
        {
            powerUp.GetComponent<PowerUpBehavior>().Kill();
        }

        GameObject[] healths;
        healths = GameObject.FindGameObjectsWithTag("Health");

        foreach (GameObject health in healths)
        {
            health.GetComponent<HealthBehavior>().Kill();
        }

        GameObject[] explosives;
        explosives = GameObject.FindGameObjectsWithTag("Explosive");

        foreach (GameObject explosive in explosives)
        {
            explosive.GetComponent<ExplosiveBehavior>().Explode();
        }

        GameObject.FindGameObjectWithTag("Spawner").SetActive(false);
    }
}
