using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public float currentTime;
    private float score = 0;
    
    public void AddScore(float score)
    {
        this.score += score;
    }

    private void Update ()
    {
        currentTime += Time.deltaTime;
    }
}