using UnityEngine;

[CreateAssetMenu(fileName = "Shark Stats", menuName = "Shark/SharkStats", order = 2)]
public class SharkStats : ScriptableObject
{
    public float life = 100;
    [Space(10)]
    public float patrolSpeed = 20.0f;
    public float chaseSpeed = 40.0f;
    public float rotationSpeed = 10.0f;
    public float detectionAngle = 45.0f;
    public float stopDistance = 5.0f;
}
