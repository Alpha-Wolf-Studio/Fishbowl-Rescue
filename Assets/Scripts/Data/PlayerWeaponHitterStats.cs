using UnityEngine;

[CreateAssetMenu(fileName = "WeaponHitterStats", menuName = "Player/WeaponHitterStats", order = 2)]
public class PlayerWeaponHitterStats : ScriptableObject
{
    public float range = 100;
    [Range(0.1f,2.0f)]
    public float timeUntilTarget = 2;
    [Range(0.1f,2.0f)]
    public float timeUntilComeback = 2; 
    [Range(0.1f,2.0f)]
    public float waitTime = 2;
    public float cooldown = 20;
    
}