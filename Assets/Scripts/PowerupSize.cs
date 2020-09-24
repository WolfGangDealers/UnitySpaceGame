using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSize : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float _powerupDuration = 5;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;

    [Header("Sound and Visuals")]
    [SerializeField] AudioClip _powerUpSound = null;
    [SerializeField] ParticleSystem _collectParticle = null;
    [SerializeField] AudioClip _depowered = null;

    [Header("Thrusters")]
    [SerializeField] ParticleSystem _thruster1 = null;
    [SerializeField] ParticleSystem _thruster2 = null;

    Collider _colliderToDeactivate = null;
    bool _poweredUp = false;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        EnableObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();


        // if we have a valid player and not already powered up
        if (playerShip != null && _poweredUp == false)
        {
            // Upon collision, activate the powerUp sound
            AudioHelper.PlayClip2D(_powerUpSound, 1);

            // start powerup timer. Restart, if it's already started
            StartCoroutine(PowerupSequence(playerShip));
        }
    }

    IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        // set boolean for detecting lockout
        _poweredUp = true;

        ActivatePowerup(playerShip);
        // simulate this object being disabled. We don't
        // REALLY want to disable it, because we still need
        // script behavior to continue functioning
        DisableObject();
        Collect();

        // wait for the required duration
        yield return new WaitForSeconds(_powerupDuration);
        //reset
        DeactivatePowerup(playerShip);
        EnableObject();

        // set boolean to release lockout
        _poweredUp = false;
    }



    void ActivatePowerup(PlayerShip playerShip)
    {
        if (playerShip != null)
        {
            // powerup player
            playerShip.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
            _thruster1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            _thruster2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }


    void DeactivatePowerup(PlayerShip playerShip)
    {
        // revert player powerup. - will subtract
        playerShip.transform.localScale = new Vector3(1, 1, 1);
        _thruster1.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _thruster2.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        AudioHelper.PlayClip2D(_depowered, 1);
    }

    public void DisableObject()
    {
        // disable collider, so it can't be retriggered
        _colliderToDeactivate.enabled = false;
        // disable visuals, to simulate deactivated
        _visualsToDeactivate.SetActive(false);
    }

    public void EnableObject()
    {
        // enable collider, so it can be retriggered
        _colliderToDeactivate.enabled = true;
        // enable visuals again, to draw player attention
        _visualsToDeactivate.SetActive(true);
    }

    void Collect()
    {
        _collectParticle.Play();
    }
}
