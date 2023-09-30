using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public float currentTime;

    private void Update ()
    {
        currentTime += Time.deltaTime;
    }
}