using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
      [SerializeField] GameObject gameOverScreen;
      [SerializeField] GameObject successScreen;

    public static GameManager gameManager;

    private void Awake()
    {
        gameManager = this;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void SuccessScreen()
    {
        successScreen.SetActive(true);
    }

    public void GameOverButton()
    {
        SceneManager.LoadScene(0);
    }
}
