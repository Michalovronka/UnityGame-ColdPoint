using System;
using JetBrains.Annotations;
using UnityEngine;

public class CharactersBasicBehaviour : MonoBehaviour
{
    public DeathManager deathManager;
    PlayerController _playerInstance;


    private void Start()
    {
        _playerInstance = FindFirstObjectByType<PlayerController>();
    }

    public bool Hit(Collider2D hitInfo, string damageDoneBy)
    {
        

        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {

            Destroy(gameObject);
            deathManager.TriggerDeath();
            return false;
        }
        
        BotController enemy = hitInfo.GetComponent<BotController>();
        if (enemy != null && damageDoneBy == "PlayerBullet")
        {
            Debug.Log(gameObject + "was hit by " + damageDoneBy);
            Destroy(gameObject);
            enemy.weapon.Drop(enemy.weapon);
            if (_playerInstance)
            {
                _playerInstance = FindFirstObjectByType<PlayerController>();
                _playerInstance.AddCombo(); 
                _playerInstance.CheckWinCondition();
            }
            return false;
        }

        return true;
    }


}
