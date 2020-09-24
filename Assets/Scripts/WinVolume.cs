using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinVolume : MonoBehaviour
{
    [Header("GameWin Sound and Visuals")]
    [SerializeField] AudioClip _gameWinSound = null;
    [SerializeField] ParticleSystem _WinParticle = null;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;

    Collider _colliderToDeactivate = null;

    private void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();

        // if we found something valid, continue
        if (playerShip != null)
        {
            _colliderToDeactivate = GetComponent<Collider>();
            // Play the win animation
            Collect();
            DisableObject();

            // Destroy player
            playerShip.Kill();

            // Restart the game
            StartCoroutine(RestartGame());
        }
    }


    void Collect()
    {
        _WinParticle.Play();
    }


    void DisableObject()
    {
        // disable collider, so it can't be retriggered
        _colliderToDeactivate.enabled = false;
        // disable visuals, to simulate deactivated
        _visualsToDeactivate.SetActive(false);
    }

    IEnumerator RestartGame()
    {
        // Play game Win sound
        AudioHelper.PlayClip2D(_gameWinSound, 1);
        // wait for the required duration
        yield return new WaitForSeconds(2);
        //reset
        GameInput.ReloadLevel();
    }
}
