using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 30f;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Runs the deathTimer method at the same time as other methods
        StartCoroutine("deathTimer");
    }

    // Update is called once per frame
    void Update()
    {
        // Move the missile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator deathTimer()
    {
        // Stop this script immediately and wait 10 seconds
        yield return new WaitForSeconds(10);
        // Destroy this missile
        Destroy(gameObject);
    }
}
