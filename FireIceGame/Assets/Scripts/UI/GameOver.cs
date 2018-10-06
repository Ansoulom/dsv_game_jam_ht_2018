﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public void PlayAgain () {
        SceneManager.LoadScene("Level");
    }

    public void MainMenu () {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame () {
        Application.Quit();
    }
}
