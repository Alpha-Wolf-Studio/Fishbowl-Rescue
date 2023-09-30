using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [field: SerializeField] private List<IInteractable> waterTaps = new List<IInteractable>();
    [SerializeField] private PlayerController player;

    [Space(10)]
    [SerializeField] private float minPlayerDistance = 30.0f;

    private void Start ()
    {
        for (int i = 0; i < waterTaps.Count; i++)
        {
            waterTaps[i].onInteract.AddListener(OnInteractWaterTap);
            StartCoroutine(SpawnWaterTap(waterTaps[i]));
        }
    }

    private IEnumerator SpawnWaterTap (IInteractable interactable)
    {
        yield return new WaitForSeconds(2.0f);
        bool availableSpawn = false;
        int randomIndex = 0;
        int counter = 0;

        while (!availableSpawn)
        {
            counter++;
            randomIndex = Random.Range(0, spawnPoints.Count);
            if (spawnPoints[randomIndex].childCount == 0)
            {
                if (counter > 100 || Vector3.Distance(player.transform.position, spawnPoints[randomIndex].position) < minPlayerDistance)
                {
                    availableSpawn = true;
                }
            }
        }

        Transform spawn = spawnPoints[randomIndex];
        interactable.Activate(spawn);
    }

    private void OnInteractWaterTap(IInteractable interactable)
    {
        StartCoroutine(SpawnWaterTap(interactable));
    }
}