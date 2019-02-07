using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;         //A reference to our game control script so we can access it statically.

    public bool gameOver = false;               //Is the game over?

    public Text scoreText;
    public Text finalScoreText;
    public Text gameOverText;
    public Text healText;
    public Text explodeText;
    public Text enemiesText;
    public Text powerUpsText;

    private int score = 0;

    public long ticks = 0;

    public int totalHeals = 0;
    public int totalExplodes = 0;
    public int totalPowerups = 0;
    public int totalEnemies = 0;

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

    void Update()
    {
        //If the game is over and the player has pressed some input...
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            //...reload the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        ticks++;
    }

    public void EndGame()
    {
        scoreText.gameObject.SetActive(false);
        finalScoreText.text = "Final Score: " + score.ToString();
        healText.text = "Health Packs Used: " + totalHeals.ToString();
        explodeText.text = "Mines Exploded: " + totalExplodes.ToString();
        enemiesText.text = "Powerups Collected: " + totalPowerups.ToString();
        powerUpsText.text = "Enemies Killed: " + totalEnemies.ToString();
        explodeText.gameObject.SetActive(true);
        healText.gameObject.SetActive(true);
        finalScoreText.gameObject.SetActive(true);
        enemiesText.gameObject.SetActive(true);
        powerUpsText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}