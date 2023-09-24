using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager instance;

    public TextMeshProUGUI WinText;

    private bool GameOver;

    private void Awake()
    {
        instance = this;
    }
    public void GameWin(string PlayerName)
    {
        if (GameOver) return;
        GameOver = true;
        if (WinText) WinText.text = PlayerName + " Win!";
        StartCoroutine(RestartTimer());
    }

    IEnumerator RestartTimer()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
