using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Animator robot;

    [SerializeField]
    GameObject missilePrefab;

    [SerializeField]
    private string robotType;

    public int health;
    public int range;
    public float fireRate;

    public Transform missileFireSpot;
    UnityEngine.AI.NavMeshAgent agent;

    private Transform player;
    private float timeLastFired;

    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        // Makes sure the robot is alive and sets up both its NavMeshAgent and target (the player)
        isDead = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the robot is dead
        if (isDead)
        {
            return;
        }
        
        // Make the robot look at the player and move towards the player using the navmesh
        transform.LookAt(player);
        agent.SetDestination(player.position);
    
        // If the robot is close enough to the player and hasn't fired too recently, fire
        if (Vector3.Distance(transform.position, player.position) < range &&
            Time.time - timeLastFired > fireRate)
        {
            timeLastFired = Time.time;
            Fire();
        }
    }

    private void Fire()
    {
        GameObject missile = Instantiate(missilePrefab);
        missile.transform.position = missileFireSpot.transform.position;
        missile.transform.rotation = missileFireSpot.transform.rotation;
        robot.Play("Fire");
    }
}
