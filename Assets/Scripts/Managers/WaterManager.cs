using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WaterManager : MonoBehaviourSingleton<WaterManager>
{
    [SerializeField] private GameObject waterLevel;
    public float CurrentWater { get; private set; } = 100;
    [field: SerializeField] public float MaxWater { get; private set; } = 100;
    [field: SerializeField] public float MinWater { get; private set; } = 10;

    [SerializeField] private float speedDecrease = 10;
    [SerializeField] private float amount = 10;
    [SerializeField] private float duration = 3.0f;

    protected override void OnAwaken()
    {
        base.OnAwaken();
        CurrentWater = MaxWater;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        CurrentWater -= speedDecrease * deltaTime;
        CurrentWater = Mathf.Clamp(CurrentWater, MinWater, MaxWater);
        waterLevel.transform.position = new Vector3(transform.position.x, CurrentWater, transform.position.z);
    }

    private void RemoveWater(float amount)
    {
        CurrentWater -= amount;
        if (CurrentWater < MaxWater)
            CurrentWater = MinWater;
    }

    public void AddWater()
    {
        StartCoroutine(AddWaterOverTime());
    }

    private IEnumerator AddWaterOverTime()
    {
        float initialValue = 0;
        float elapsedTime = 0;
        float finalValue = amount;
        float previousValue = CurrentWater ;
        while (elapsedTime< duration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = Mathf.Clamp01(elapsedTime / duration);
            
            float currentValue = Mathf.Lerp(initialValue, finalValue, percentageComplete);

            CurrentWater = previousValue + currentValue;
            if (CurrentWater > MaxWater)
                CurrentWater = MaxWater;
            yield return null;
        }
    }
}