using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WavesGameMode : MonoBehaviour
{
    [SerializeField] private Life playerLife;
    [SerializeField] private Life BaseLife;
    private bool isPlayerDead = false;
    private bool isBaseDestroyed = false;

    void Start()
    {
        playerLife.onDeath.AddListener(OnPlayerDied);

        BaseLife.onDeath.AddListener(OnBaseDestroyed);

        EnemiesManager.instance.onChanged.AddListener(CheckWinCondition);
        WavesManager.instance.onChanged.AddListener(CheckWinCondition);
    }

    void OnPlayerDied()
    {
        isPlayerDead = true;
        Debug.Log("Player has died.");
        SceneManager.LoadScene("LoseScreen");
    }

    void OnBaseDestroyed()
    {
        isBaseDestroyed = true;
        Debug.Log("Base has been destroyed.");
        SceneManager.LoadScene("WinScreen");
    }

    void CheckWinCondition()
    {
        if (!isPlayerDead && !isBaseDestroyed &&
            EnemiesManager.instance.enemies.Count <= 0 &&
            WavesManager.instance.waves.Count <= 0)
        {
            Debug.Log("Win condition met. Loading WinScreen...");
            SceneManager.LoadScene("WinScreen");
        }
    }
}
