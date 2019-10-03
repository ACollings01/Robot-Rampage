using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    Sprite redReticle;
    [SerializeField]
    Sprite yellowReticle;
    [SerializeField]
    Sprite blueReticle;
    [SerializeField]
    Image reticle;
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text armorText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text pickupText;
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private Text enemyText;
    [SerializeField]
    private Text waveClearText;
    [SerializeField]
    private Text newWaveText;
    [SerializeField]
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the armor and health text at the start of the game
        SetArmorText(player.armor);
        SetHealthText(player.health);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Setters for different text values of the ui
    public void SetArmorText(int armor)
    {
        armorText.text = "Armor: " + armor;
    }

    public void SetHealthText(int health)
    {
        healthText.text = "Health: " + health;
    }

    public void SetAmmoText(int ammo)
    {
        ammoText.text = "Ammo: " + ammo;
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "" + score;
    }

    public void SetWaveText(int time)
    {
        waveText.text = "Next Wave: " + time;
    }

    public void SetEnemyText(int enemies)
    {
        enemyText.text = "Enemies: " + enemies;
    }


    // Shows wave clear bonus, starts a coroutine to hide it
    public void ShowWaveClearBonus()
    {
        waveClearText.GetComponent<Text>().enabled = true;
        StartCoroutine("hideWaveClearBonus");
    }

    // Hides waveclear bonus after 4 seconds
    IEnumerator hideWaveClearBonus()
    {
        yield return new WaitForSeconds(4);
        waveClearText.GetComponent<Text>().enabled = false;
    }

    // Sets and shows the pickup text, stops and starts the coroutine to hide the pickup text so it doesn't end early if picking up multiple pickups
    public void SetPickUpText(string text)
    {
        pickupText.GetComponent<Text>().enabled = true;
        pickupText.text = text;
        // Restart the Coroutine so it doesn’t end early
        StopCoroutine("hidePickupText");
        StartCoroutine("hidePickupText");
    }

    // Hides pickup text after 4 seconds
    IEnumerator hidePickupText()
    {
        yield return new WaitForSeconds(4);
        pickupText.GetComponent<Text>().enabled = false;
    }

    // Shows the new wave text, starts the coroutine to hide it
    public void ShowNewWaveText()
    {
        StartCoroutine("hideNewWaveText");
        newWaveText.GetComponent<Text>().enabled = true;
    }

    // Hides new wave text after 4 seconds
    IEnumerator hideNewWaveText()
    {
        yield return new WaitForSeconds(4);
        newWaveText.GetComponent<Text>().enabled = false;
    }

    public void UpdateReticle()
    {
        switch (GunEquipper.activeWeaponType)
        {
            case Constants.Pistol:
                reticle.sprite = redReticle;
                break;
            case Constants.Shotgun:
                reticle.sprite = yellowReticle;
                break;
            case Constants.AssaultRifle:
                reticle.sprite = blueReticle;
                break;
            default:
                return;
        }
    }
}
