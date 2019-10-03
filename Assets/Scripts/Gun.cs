using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float zoomFactor;
    public int range;
    public int damage;

    private float zoomFOV;
    private float zoomSpeed = 6;

    public float fireRate;
    protected float lastFireTime;

    public Ammo ammo;
    public AudioClip liveFire;
    public AudioClip dryFire;

    // Start is called before the first frame update
    void Start()
    {
        zoomFOV = Constants.CameraDefaultZoom / zoomFactor;
        lastFireTime = Time.time - 10;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomFOV, zoomSpeed * Time.deltaTime);
        }
        else
        {
            Camera.main.fieldOfView = Constants.CameraDefaultZoom;
        }
    }

    protected void Fire()
    {
        // Play the proper sounds depending on the player's ammo
        if (ammo.HasAmmo(tag))
        {
            GetComponent<AudioSource>().PlayOneShot(liveFire);
            ammo.ConsumeAmmo(tag);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(dryFire);
        }

        // Play the fire animation
        GetComponentInChildren<Animator>().Play("Fire");

        // Creates a ray along the camera's viewport with length of the range of the current weapon, checks if it hits anything and if it does, processes the hit
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            processHit(hit.collider.gameObject);
        }
    }

    private void processHit(GameObject hitObject)
    {
        if (hitObject.GetComponent<Player>() != null)
        {
            hitObject.GetComponent<Player>().TakeDamage(damage);
        }

        if (hitObject.GetComponent<Robot>() != null)
        {
            hitObject.GetComponent<Robot>().TakeDamage(damage);
        }
    }
}
