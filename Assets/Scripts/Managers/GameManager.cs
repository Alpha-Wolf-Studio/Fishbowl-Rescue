using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] private List<Shark> sharks = new List<Shark>();
    public List<Shark> Sharks => sharks;

    public float currentTime;
    public int FishCount { get; private set; } = 0;
    public int SharkHits { get; private set; } = 0;
    [SerializeField] private PlayerController player;
    [SerializeField] private UIControllerInGame uiController;
    [SerializeField] private WaterManager waterManager;
    private bool isPaused;

    protected override void OnAwaken()
    {
        Cursor.lockState = CursorLockMode.Locked;
        bool isPaused = false;
        player.OnPauseInput.AddListener(TogglePause); 
        player.OnPlayerDeath.AddListener(GameOver); 
        uiController.OnPausedGame.AddListener(TogglePause); 
        waterManager.OnWaterEnded.AddListener(GameOver);
    }

    protected override void OnDestroyed()
    {
        waterManager.OnWaterEnded.RemoveAllListeners();
        player.OnPlayerDeath.RemoveAllListeners();
        player.OnPauseInput.RemoveListener(TogglePause);
        uiController.OnPausedGame.RemoveListener(TogglePause); 
    }

    public void AddScore(int score)
    {
        this.FishCount += score;
    } 
    public void AddHit()
    {
        this.SharkHits++;
    }

    private void Update ()
    {
        currentTime += Time.deltaTime;
    }
    private void TogglePause()
    {
        isPaused = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        uiController.SetPauseMenu(isPaused);
    }
    private void GameOver()
    {
        uiController.SetGameOver();
    }
}