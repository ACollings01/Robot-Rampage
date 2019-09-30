using UnityEngine;

public class Pistol : Gun
{
    protected override void Update()
    {
        base.Update();
        // Shotgun and pistol are semi-automatic
        if (Input.GetMouseButtonDown(0) && (Time.time - lastFireTime) > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}