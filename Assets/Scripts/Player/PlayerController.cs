using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerWeaponHitterStats playerHitterStats;
    [SerializeField] private WeaponPicker weaponPicker;
    [SerializeField] private WeaponHitter weaponHitter;

    private bool canShoot;
    private Rigidbody rigidbody;
    private Vector3 finalPos;
    public PlayerStats PlayerStats => playerStats;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        weaponHitter.playerHitterStats = playerHitterStats;
        weaponPicker.playerHitterStats = playerHitterStats;
        canShoot = true;
    }

    void Start()
    {
        weaponHitter.IsWeaponActive.AddListener(IsShooting);
        weaponPicker.IsWeaponActive.AddListener(IsShooting);
    }

    private void OnDestroy()
    {
        weaponHitter.IsWeaponActive.RemoveListener(IsShooting);
        weaponPicker.IsWeaponActive.RemoveListener(IsShooting);
    }

    private void Update()
    {
        FixRotation();
        float deltaTime = Time.fixedDeltaTime;

        Rotate(deltaTime);
        Movement(deltaTime);
        WeaponsAction(deltaTime);
    }

    private bool HasHittedSomething(out RaycastHit hit)
    {
        bool hasHitted = Physics.Raycast(transform.position, transform.forward, out hit, playerHitterStats.range);
        if (hasHitted)
        {
            Debug.Log(hit.collider.name);
        }

        return hasHitted;
    }


    private void Rotate(float deltaTime)
    {
        if (!CanMove())
            return;

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            float mouseY = Input.GetAxisRaw("Mouse Y");
            float rotacionX = mouseY * playerStats.speedRotation * deltaTime * playerStats.signY;
            float rotacionY = mouseX * playerStats.speedRotation * deltaTime * playerStats.signX;
            rigidbody.AddTorque(rotacionX, rotacionY, 0, playerStats.forceMode);
        }
    }

    private void FixRotation()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
    }

    private void IsShooting(bool state)
    {
        canShoot = !state;
    }

    private void Movement(float deltaTime)
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

    private void Move(float deltaTime, Vector3 velocity) =>
        rigidbody.AddForce(velocity * deltaTime, playerStats.forceMode);

    public void MoveByAttack(Vector3 velocity, ForceMode forceMode) =>
        rigidbody.AddForce(velocity, forceMode);

    private void MoveUp(float deltaTime) => Move(deltaTime, transform.up * playerStats.speedUp);
    private void MoveDown(float deltaTime) => Move(deltaTime, -transform.up * playerStats.speedDown);
    private void MoveForward(float deltaTime) => Move(deltaTime, transform.forward * playerStats.speedForward);
    private void MoveBack(float deltaTime) => Move(deltaTime, -transform.forward * playerStats.speedDirections);
    private void MoveLeft(float deltaTime) => Move(deltaTime, -transform.right * playerStats.speedDirections);
    private void MoveRight(float deltaTime) => Move(deltaTime, transform.right * playerStats.speedDirections);

    private void WeaponsAction(float deltaTime)
    {
        if (canShoot)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                CalculateShootEndPosition(out RaycastHit hit);
                weaponHitter.Shoot(hit, finalPos);
            }

            if (Input.GetKey(KeyCode.E))
            {
                CalculateShootEndPosition(out RaycastHit hit);
                weaponPicker.Shoot(hit, finalPos);
            }
        }
    }

    private void CalculateShootEndPosition(out RaycastHit hit)
    {
        if (HasHittedSomething(out hit))
        {
            finalPos = hit.collider.transform.position;
        }
        else
        {
            finalPos = transform.position + transform.forward * playerHitterStats.range;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * playerHitterStats.range);
    }

    private bool CanMove() => true;
}