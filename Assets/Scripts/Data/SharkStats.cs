using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shark Stats", menuName = "Shark/SharkStats", order = 1)]
public class SharkStats : ScriptableObject
{
    public float life = 100;
    [Space(10)]
    public float attackCooldown = 1.5f;
    public float speedForward = 40;
    public float speedDirections = 20;
    public float speedUp = 30;
    public float speedDown = 30;
}