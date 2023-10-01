using UnityEngine;

[CreateAssetMenu(fileName = "Shark Stats", menuName = "Shark/SharkStats", order = 2)]
public class SharkStats : ScriptableObject
{
    public float life = 100;

    [Header("Generals")] 
    public LayerMask layerMaskHidePlayer;
    public float minDist = 5;
    public float maxDistVision = 80;
    public float minRadiusDetection = 15;
    public float speedRotation = 10;
    public float damage = 20.0f;

    [Header("Patrol")]
    public float speedPatrolMovement = 10;
    public float sphereRadius = 200;
    public float detectionAngle = 45;

    [Header("Follow")]
    public float speedFollowMovement = 15;

    [Header("Follow")]
    public float speedRunMovement = 25;
    public float distanceRun = 35;
}