using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private AudioClip weakHitSound;

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
        // Create a new missile instance and make sure it is oriented properly
        GameObject missile = Instantiate(missilePrefab);
        missile.transform.position = missileFireSpot.transform.position;
        missile.transform.rotation = missileFireSpot.transform.rotation;

        // Play the Fire animation
        robot.Play("Fire");

        // Play the fire sound
        GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    public void TakeDamage(int amount)
    {
        // Make sure the robot is alive
        if (isDead)
        {
            return;
        }

        // Subract the damage taken from the robot's health
        health -= amount;

        // Check if the robot is dead
        if (health <= 0)
        {
            isDead = true;

            // Play the Die animation
            robot.Play("Die");

            // Start the DestroyRobot coroutine
            StartCoroutine("DestroyRobot");

            // Play the death sound
            GetComponent<AudioSource>().PlayOneShot(deathSound);
        }
        else
        {
            // Play the weak hit sound
            GetComponent<AudioSource>().PlayOneShot(weakHitSound);
        }
    }

    IEnumerator DestroyRobot()
    {
        // Wait 1.5 seconds for the death animation then destroy the robot
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
