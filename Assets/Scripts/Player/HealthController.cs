using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        StartCoroutine(RegenerateHealth());
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found on the GameObject.");
        }
    }

    IEnumerator RegenerateHealth(){
        while (true){
            if (playerStats.playerHealth < playerStats.maxHealth){ 
                playerStats.playerHealth += playerStats.healthRegen;
                yield return new WaitForSeconds(1);
            } else {
                yield return null;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (playerStats != null)
        {
            playerStats.playerHealth -= damageAmount;
            
            if (playerStats.playerHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (playerStats != null)
        {
            playerStats.playerHealth = Mathf.Min(playerStats.playerHealth + healAmount, playerStats.maxHealth);
        }
    }

    private void Die()
    {
        // Add any death logic here, like respawning or game over
        Debug.Log("Player has died.");
    }
}