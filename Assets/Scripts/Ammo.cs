using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    GameUI gameUI;

    [SerializeField]
    private int pistolAmmo = 20;
    [SerializeField]
    private int shotgunAmmo = 10;
    [SerializeField]
    private int assaultRifleAmmo = 50;

    public Dictionary<string, int> tagToAmmo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        // Creates the dictionary with all accepted types of ammo
        tagToAmmo = new Dictionary<string, int>
        {
            { Constants.Pistol, pistolAmmo },
            { Constants.Shotgun, shotgunAmmo},
            { Constants.AssaultRifle, assaultRifleAmmo},
        };
    }

    public void AddAmmo(string tag, int ammo)
    {
        // Checks to see that the dictionary holding ammo types and numbers contains that type of ammo
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }

        // Adds the amount of ammo picked up to the reserve in the dictionary
        tagToAmmo[tag] += ammo;
    }

    public bool HasAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }

        // Returns true if the player has ammo, false if player has no ammo
        return tagToAmmo[tag] > 0;
    }

    public int GetAmmo(string tag)
    {
        // Checks to see if the passed gun type exists
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }

        // Returns the amount of ammo associated with that type of gun
        return tagToAmmo[tag];
    }

    public void ConsumeAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }

        // Decrement the given ammo reserve
        tagToAmmo[tag]--;
    }
}
