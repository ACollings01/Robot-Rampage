using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject[] robots;

    private int timesSpawned;
    private int healthBonus = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRobot()
    {
        // Increment number of times spawned
        timesSpawned++;
        // Increase health bonus to increase difficulty
        healthBonus += 1 * timesSpawned;
        // Create a new robot and assign its health and position
        GameObject robot = Instantiate(robots[Random.Range(0, robots.Length)]);
        robot.transform.position = transform.position;
        robot.GetComponent<Robot>().health += healthBonus;
    }
}
