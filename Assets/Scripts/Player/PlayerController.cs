using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerWeaponHitterStats playerHitterStats;
    [Header("References")]
    [SerializeField] private WeaponPicker weaponPicker;
    [SerializeField] private WeaponHitter weaponHitter;
    private ModelReference model;
    [Header("Variables")]
    [SerializeField] private KeyCode pauseKey = KeyCode.P;
    private bool canShoot;
    private Rigidbody rigidbody;
    private Vector3 finalPos;
    float rotationZ = 0;

    public UnityEvent OnPlayerDeath { get; } = new UnityEvent();
    public UnityEvent OnPauseInput { get; } = new UnityEvent();
    public PlayerStats PlayerStats => playerStats;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        weaponHitter.playerHitterStats = playerHitterStats;
        weaponPicker.playerHitterStats = playerHitterStats;
        playerStats.currentLife = playerStats.maxLife;
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
        CheckPauseInput();
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

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        float rotationX = mouseY * playerStats.speedRotation * deltaTime * playerStats.signY;
        float rotationY = mouseX * playerStats.speedRotation * deltaTime * playerStats.signX;

        rigidbody.AddTorque(rotationX, rotationY, 0, playerStats.forceMode);
        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 rotationAngles = rigidbody.rotation.eulerAngles;
            rotationZ = rotationAngles.z +(1 * playerStats.speedRotationRoll * deltaTime);
            rigidbody.MoveRotation(Quaternion.Euler(rotationAngles.x, rotationAngles.y, rotationZ));
        }

        if (Input.GetKey(KeyCode.E))
        {
            Vector3 rotationAngles = rigidbody.rotation.eulerAngles;
            rotationZ = rotationAngles.z + (-1 * playerStats.speedRotationRoll * deltaTime);
            rigidbody.MoveRotation(Quaternion.Euler(rotationAngles.x, rotationAngles.y, rotationZ));
        }
    }

    private void FixRotation()
    {
        //Vector3 rot = transform.rotation.eulerAngles;
        //rot.z = 0;
        //transform.rotation = Quaternion.Euler(rot);
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

    private void CheckPauseInput()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            OnPauseInput.Invoke();
        }
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
            CalculateShootEndPosition(out RaycastHit hit);

            MarkHit(hit);

            if (Input.GetMouseButtonDown(0))
            {
                weaponHitter.Shoot(hit, finalPos);
            }

            if (Input.GetMouseButtonDown(1))
            {
                weaponPicker.Shoot(hit, finalPos);
            }
        }
    }

    private void MarkHit(RaycastHit hit)
    {
        if (hit.collider)
        {
            ModelReference newModelHit = hit.collider.GetComponent<ModelReference>();

            if (newModelHit && newModelHit != model)
            {
                if (model)
                    model.DeactivateOutline();

                model = newModelHit;
                model.ActivateOutline();
            }
        }
        else if (model)
        {
            model.DeactivateOutline();
            model = null;
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

    public void ReceiveDamage(float damage)
    {
        playerStats.currentLife -= damage;
        if (playerStats.currentLife < 1)
        {
            OnPlayerDeath.Invoke();
        }
    }

    private bool CanMove() => true;
}