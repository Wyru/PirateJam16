using UnityEngine;
using System;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 144;
    }

    public static GameManager instance;

    public List<Upgrade> upgradeLibrary = new List<Upgrade>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddXP(int xp, PlayerStats playerStats)
    {
        playerStats.playerXP += xp;
        if (playerStats.playerXP >= playerStats.xpToLevelUp)
        {
            LevelUp(playerStats);
        }
    }

    private void LevelUp(PlayerStats playerStats)
    {
        playerStats.playerLevel++;
        playerStats.playerXP -= playerStats.xpToLevelUp;
        playerStats.xpToLevelUp *= 2;

        PauseGame();
        ShowUpgradeCards();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
    }

    private void ShowUpgradeCards()
    {
        // Implement UI to show upgrade options for the player to choose from
    }

    public void SelectUpgrade(Upgrade selectedUpgrade, PlayerStats playerStats)
    {
        ApplyUpgrade(selectedUpgrade);
        ResumeGame();
    }

    private void ApplyUpgrade(Upgrade upgrade)
    {
        Debug.Log("Applying upgrade: " + upgrade.upgradeName);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
    }

    public void ApplyDamageToPlayer(int damageAmount)
    {
        HealthController playerHealth = FindFirstObjectByType<HealthController>();
        
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    public void HealPlayer(int healAmount)
    {
        HealthController playerHealth = FindFirstObjectByType<HealthController>();
        
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
        }
    }
}
