using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WaterManager : MonoBehaviourSingleton<WaterManager>
{
    [SerializeField] private GameObject waterLevel;
    [SerializeField] private GameObject sand;
    public float CurrentWater { get; private set; } = 100;
    [field: SerializeField] public float MaxWater { get; private set; } = 100;
    [field: SerializeField] public float MinWater { get; private set; } = 10;
    [field: SerializeField] public Vector3 WaterPosition => waterLevel.transform.position;
    [Range(1, 10)]
    [SerializeField] private float WaterLevelGameOver = 0.3f;
    [field: SerializeField] public Vector3 MinWaterPosition => sand.transform.position;
    [field: SerializeField] public UnityEvent OnWaterEnded = new UnityEvent();
    [field: SerializeField] private float waterThreshold; 
    [SerializeField] private float speedDecrease = 10;
    [SerializeField] private float amount = 10;
    [SerializeField] private float duration = 3.0f;

    protected override void OnAwaken()
    {
        MaxWater = WaterPosition.y;
        MinWater = MinWaterPosition.y;
        CurrentWater = MaxWater; 
        waterThreshold = MaxWater - MinWater;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        CurrentWater -= speedDecrease * deltaTime;
        CurrentWater = Mathf.Clamp(CurrentWater, MinWater, MaxWater);

        waterLevel.transform.position = new Vector3(transform.position.x, CurrentWater, transform.position.z);
        
        if (CurrentWater < (MinWater /WaterLevelGameOver))
        {
            OnWaterEnded.Invoke();
            CurrentWater += MaxWater;
        }
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