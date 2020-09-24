using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;
    [SerializeField] GameObject thrusterRight;
    [SerializeField] GameObject thrusterLeft;

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    // use forces to build momentum forward/backward
    void MoveShip()
    {
        // S/Down = -1, W/Up = 1, None = 0. Scale by moveSpeed
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _moveSpeed;
        // combine our direction with our calculated amount
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        // apply the movement to the physics object
        _rb.AddForce(moveDirection);
    }

    void TurnShip()
    {
        // A/Left - 01, D/Right = 1, None = 0. Scale by turnSpeed
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;
        // specify an axis to apply our turn amount (x,y,z) as a rotation
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // spin the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void Kill()
    {
        Debug.Log("Player has been killed!");
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        // If W is pressed, activate both particle systems
        if (Input.GetKey(KeyCode.W))
        {
            ToggleThrustersActive();
        }
        else if (Input.GetKey(KeyCode.A)) // If A is pressed, activate the right particle system.
        {
            ToggleThrusterRight();
        }
        else if (Input.GetKey(KeyCode.D)) // If D is pressed, activate the left particle system.
        {
            ToggleThrusterLeft();
        }
        else if (Input.GetKeyUp(KeyCode.W)) // If W key is released, set both thrusters to false.
        {
            thrusterRight.SetActive(false);
            thrusterLeft.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.A)) // If A key is released, set Right thruster to false.
        {
            thrusterRight.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.D)) // If D key is released, set Left thruster to false.
        {
            thrusterLeft.SetActive(false);
        }
    }

    void ToggleThrustersActive() // Toggle both thrusters on.
    {
        thrusterRight.SetActive(true);
        thrusterLeft.SetActive(true);
    }

    void ToggleThrusterRight() // Toggle Right thruster on and turn off left thruster.
    {
        thrusterRight.SetActive(true);
        thrusterLeft.SetActive(false);
    }

    void ToggleThrusterLeft() // Toggle Left Thruster on and turn off right thruster.
    {
        thrusterLeft.SetActive(true);
        thrusterRight.SetActive(false);
    }
    
}
