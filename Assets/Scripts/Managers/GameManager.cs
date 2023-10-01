using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public float currentTime;
    private float score = 0;
    private float hit = 0;
    
    public void AddScore(float score)
    {
        this.score += score;
    } 
    public void AddHit()
    {
        this.hit++;
    }

    private void Update ()
    {
        currentTime += Time.deltaTime;
    }
}