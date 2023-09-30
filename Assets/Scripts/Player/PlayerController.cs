using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private WeaponPicker weaponPicker;
    [SerializeField] private WeaponHitter weaponHitter;

    private Rigidbody rigidbody;

    private void Awake ()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start () { }

    private void Update ()
    {
        float deltaTime = Time.fixedDeltaTime;

        Rotate(deltaTime);
        Movement(deltaTime);
        WeaponsAction(deltaTime);
    }

    private void OnDestroy () { }

    private void Rotate (float deltaTime)
    {
        if (!CanMove())
            return;

        if (Input.GetMouseButtonDown(0)) { }
    }

    private void Movement (float deltaTime)
    {
        if (!CanMove())
            return;

        if (Input.GetKey(KeyCode.LeftControl))
            MoveDown(deltaTime);
        if (Input.GetKey(KeyCode.Space))
            MoveUp(deltaTime);
        if (Input.GetKey(KeyCode.W))
            MoveForward(deltaTime);
        if (Input.GetKey(KeyCode.A))
            MoveLeft(deltaTime);
        if (Input.GetKey(KeyCode.S))
            MoveBack(deltaTime);
        if (Input.GetKey(KeyCode.D))
            MoveRight(deltaTime);
    }

    private void Move (float deltaTime, Vector3 velocity) => rigidbody.AddForce(velocity * deltaTime, playerStats.forceMode);
    private void MoveUp (float deltaTime) => Move(deltaTime, transform.up * playerStats.speedUp);
    private void MoveDown (float deltaTime) => Move(deltaTime, -transform.up * playerStats.speedDown);
    private void MoveForward (float deltaTime) => Move(deltaTime, transform.forward * playerStats.speedForward);
    private void MoveBack (float deltaTime) => Move(deltaTime, -transform.forward * playerStats.speedDirections);
    private void MoveLeft (float deltaTime) => Move(deltaTime, -transform.right * playerStats.speedDirections);
    private void MoveRight (float deltaTime) => Move(deltaTime, transform.right * playerStats.speedDirections);

    private void WeaponsAction (float deltaTime)
    {

    }

    private bool CanMove () => true;
}