using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public float currentTime;
    public int FishCount { get; private set; } = 0;
    public int SharkHits { get; private set; } = 0;
    [SerializeField] private PlayerController player;
    [SerializeField] private UIControllerInGame uiController;
    private bool isPaused;

    protected override void OnAwaken()
    {
        bool isPaused = false;
        player.OnPauseInput.AddListener(TogglePause); 
        uiController.OnPausedGame.AddListener(TogglePause); 
    }

    protected override void OnDestroyed()
    {
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
        uiController.SetPauseMenu(isPaused);
    }
}