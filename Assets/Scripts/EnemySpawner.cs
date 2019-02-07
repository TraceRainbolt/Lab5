using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


    public static EnemySpawner instance;         //A reference to our game control script so we can access it statically.

    public GameObject enemy;

    private int lockout = 0;

    public float minCooldown = 75;
    private float chance = 0.01f;

    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    void Update () {
        float spawn = Random.Range(0f, 1f);
        float timeMultiplier = ((GameControl.instance.ticks / 560));

        if (spawn < chance + timeMultiplier * 0.01f && lockout > minCooldown - timeMultiplier)
        {
            spawnEnemy();
            lockout = 0;
        }
        lockout++;
    }

    void spawnEnemy()
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
        Instantiate(enemy, new Vector3(x, y, 1), Quaternion.identity);
    }
}
