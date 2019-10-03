using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pickups;

    // Start is called before the first frame update
    void Start()
    {
        // Creates a pickup to start the game
        SpawnPickup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPickup()
    {
        // Instantiates a random pickup to the location of the PickupSpawn GameObject
        GameObject pickup = Instantiate(pickups[Random.Range(0, pickups.Length)]);
        pickup.transform.position = transform.position;
        pickup.transform.parent = transform;
    }

    IEnumerator RespawnPickup()
    {
        // Waits 20 seconds to spawn a new pickup
        yield return new WaitForSeconds(20);
        SpawnPickup();
    }

    public void PickupWasPickedUp()
    {
        StartCoroutine("RespawnPickup");
    }
}
