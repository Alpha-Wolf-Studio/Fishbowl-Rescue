using System.Collections.Generic;
using UnityEngine;

public class OxygenBubbles : MonoBehaviour
{
    [SerializeField] private Bubble prefabBubble;
    [SerializeField] private List<Bubble> bubbles = new List<Bubble>();

    [SerializeField] private float spawnTime;
    [SerializeField] private float maxSpawnTime = 2.0f;

    private void Update ()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime > maxSpawnTime)
        {
            spawnTime = 0;
            SpawnBubble();
        }
    }

    private void SpawnBubble ()
    {
        Bubble bubble = bubbles.Find(b => !b.gameObject.activeSelf);

        if (bubble == null)
        {
            bubble = Instantiate(prefabBubble, Vector3.zero, Quaternion.identity, transform);
            bubble.transform.localPosition = Vector3.zero;
            bubbles.Add(bubble);
        }
        
        bubble.Activate();
    }
}