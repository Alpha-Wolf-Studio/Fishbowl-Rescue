using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WaterManager : MonoBehaviourSingleton<WaterManager>
{
    [SerializeField] private GameObject waterLevel;
    [SerializeField] private GameObject sand;
    public float CurrentWater { get; private set; } = 100;
    [field: SerializeField] public float MaxWater { get; private set; } = 100;
    [field: SerializeField] public float MinWater { get; private set; } = 10;
    [field: SerializeField] public Vector3 WaterPosition => waterLevel.transform.position;
    [field: SerializeField] public Vector3 MinWaterPosition => sand.transform.position;

    [SerializeField] private float speedDecrease = 10;
    [SerializeField] private float amount = 10;
    [SerializeField] private float duration = 3.0f;

    protected override void OnAwaken ()
    {
        MaxWater = WaterPosition.y;
        MinWater = MinWaterPosition.y;
        CurrentWater = MaxWater;
    }

    private void Update ()
    {
        float deltaTime = Time.deltaTime;
        CurrentWater -= speedDecrease * deltaTime;
        CurrentWater = Mathf.Clamp(CurrentWater, MinWater, MaxWater);

        waterLevel.transform.position = new Vector3(transform.position.x, CurrentWater, transform.position.z);
    }

    public void AddWater ()
    {
        StartCoroutine(AddWaterOverTime());
    }

    private IEnumerator AddWaterOverTime ()
    {
        float initialValue = 0;
        float elapsedTime = 0;
        float finalValue = amount;
        float previousValue = CurrentWater;

        while (elapsedTime < duration)
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