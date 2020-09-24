using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    [Header("DeathSound")]
    [SerializeField] AudioClip _deathSound = null;

    [Header("GameOver Sound")]
    [SerializeField] AudioClip _gameOverSound = null;
    private void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
        
        // if we found something valid, continue
        if (playerShip != null)
        {
            // Upon collision, activate the powerUp sound
            AudioHelper.PlayClip2D(_deathSound, 1);
            // Destroy player
            playerShip.Kill();

            StartCoroutine(RestartGame());
        }
    }

    IEnumerator RestartGame()
    {
        // wait for the required duration
        yield return new WaitForSeconds(1);
        // Play game over sound
        AudioHelper.PlayClip2D(_gameOverSound, 1);
        // wait for the required duration
        yield return new WaitForSeconds(1);
        //reset
        GameInput.ReloadLevel();
    }
}
