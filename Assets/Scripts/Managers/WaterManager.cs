using System;
using UnityEngine;

public class WaterManager : MonoBehaviourSingleton<WaterManager>
{

    public float CurrentWater { get; private set; } = 100;
    [field: SerializeField] public float MaxWater { get; private set; } = 100;
    [field: SerializeField] public float MinWater { get; private set; } = 10;

    private float speedDecrease = 10;

    private void Update ()
    {
        
    }

    private void RemoveWater (float amount)
    {
        CurrentWater -= amount;
        if (CurrentWater < MaxWater)
            CurrentWater = MinWater;
    }

    public void AddWater (float amount)
    {
        CurrentWater += amount;
        if (CurrentWater > MaxWater)
            CurrentWater = MaxWater;
    }
}