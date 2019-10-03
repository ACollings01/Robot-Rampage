﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Loads the battle scene
    public void StartGame()
    {
        SceneManager.LoadScene("Battle");
    }

    // Exits the app
    public void Quit()
    {
        Application.Quit();
    }
}
