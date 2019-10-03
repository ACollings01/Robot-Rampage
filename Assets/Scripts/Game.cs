using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game singleton;

    [SerializeField]
    RobotSpawn[] spawns;

    public int enemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the singleton
        singleton = this;
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
    }
}
