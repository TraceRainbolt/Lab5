using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject powerUp;
    public int speed = 300;

    void Update()
    {
        float spawn = Random.Range(0f, 1f);
        if (spawn < 0.003)
        {
            spawnPowerUp();
        }
    }

    void spawnPowerUp()
    {
        float pos = Random.Range(0f, 1f);

        int x = Random.Range(-10, 10);
        int y = Random.Range(-6, 6);

        if (pos < 0.25f)
        {
            y = 5;
        }
        else if (pos < 0.5)
        {
            x = 10;
        }
        else if (pos < 0.75)
        {
            y = -5;
        }
        else
        {
            x = -10;
        }
        GameObject powerUpSpawned = Instantiate(powerUp, new Vector3(x, y, 1), Quaternion.identity);

        if (pos < 0.25f)
        {
            powerUpSpawned.GetComponent<Rigidbody2D>().AddForce(Vector3.down * speed);
        }
        else if (pos < 0.5)
        {
            powerUpSpawned.GetComponent<Rigidbody2D>().AddForce(Vector3.left * speed);
        }
        else if (pos < 0.75)
        {
            powerUpSpawned.GetComponent<Rigidbody2D>().AddForce(Vector3.up * speed);
        }
        else
        {
            powerUpSpawned.GetComponent<Rigidbody2D>().AddForce(Vector3.right * speed);
        }
    }
}
