using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameUI gameUI;
    public GameObject player;
    public int score;
    public int waveCountdown;
    public bool isGameOver;

    private static Game singleton;

    [SerializeField]
    RobotSpawn[] spawns;

    public int enemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the singleton
        singleton = this;

        StartCoroutine("increaseScoreEachSecond");  // Starts passive score increase coroutine
        isGameOver = false;                         // Makes sure the game isn't over before it begins
        Time.timeScale = 1;                         // Sets the time scale
        waveCountdown = 30;                         // Will start the next wave countdown in 30 seconds
        enemiesLeft = 0;                            // Initialize the number of enemies before we spawn any
        StartCoroutine("updateWaveTimer");          // Starts wave timer tracking coroutine (This also spawns robots)

        // Spawn the initial set of robots
        SpawnRobots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRobots()
    {
        // Spawn one robot on each spawn and increment the number of enemiesLeft
        foreach (RobotSpawn spawn in spawns)
        {
            spawn.SpawnRobot();
            enemiesLeft++;
        }

        // Update the enemiesLeft UI element
        gameUI.SetEnemyText(enemiesLeft);
    }

    // Decrements/resets the wave timer, spawns robots if necessary (once every 30 seconds)
    private IEnumerator  updateWaveTimer()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1f);
            waveCountdown--;
            gameUI.SetWaveText(waveCountdown);

            // Spawn the next wave, reset the countdown
            if (waveCountdown <= 0)     // I changed this from == to <= since that would account for any bugs/glitches which result in a negative waveCountdown
            {
                SpawnRobots();
                waveCountdown = 30;
                gameUI.ShowNewWaveText();
            }
        }
    }

    // This is called when robots are killed, decrements enemiesLeft, updates the UI and gives the player a bonus if they clear all robots before the timer runs out
    public static void RemoveEnemy()
    {
        // Add the score for killing the enemy
        singleton.AddRobotKillToScore();

        singleton.enemiesLeft--;
        singleton.gameUI.SetEnemyText(singleton.enemiesLeft);

        // Give the player a bonus for clearing the wave before the timer runs out
        if (singleton.enemiesLeft == 0)
        {
            singleton.score += 50;
            singleton.gameUI.ShowWaveClearBonus();
        }
    }

    // Called when robot dies, adds 10 to score, updates score text in UI
    public void AddRobotKillToScore()
    {
        score += 10;
        gameUI.SetScoreText(score);
    }

    // Passively increases score by 1 for every second the player survives
    IEnumerator increaseScoreEachSecond()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1);
            score += 1;
            gameUI.SetScoreText(score);
        }
    }
}
