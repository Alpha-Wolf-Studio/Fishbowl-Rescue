using System;
using CustomSceneSwitcher.Switcher;
using CustomSceneSwitcher.Switcher.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIControllerInGame : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private CanvasGroup pausedGameCanvas;
    [SerializeField] private CanvasGroup inGameCanvas;
    [SerializeField] private CanvasGroup endScreenCanvas;
    [SerializeField] private CanvasGroup settingsCanvas;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button backButton;
    [Header("SceneChangers")]
    [SerializeField] private SceneChangeData menuSceneData;
    [SerializeField] private SceneChangeData resetSceneData;
    public UnityEvent OnPausedGame { get; } = new UnityEvent();

    private void Awake()
    {
        resumeButton.onClick.AddListener(ResumePlay);
        resetButton.onClick.AddListener(ResetScene);
        settingsButton.onClick.AddListener(TurnSettingsOn);
        menuButton.onClick.AddListener(ReturnToMenu);
        backButton.onClick.AddListener(GoBackToPauseMenu);
    }

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveListener(ResumePlay);
        resetButton.onClick.RemoveListener(ResetScene);
        settingsButton.onClick.RemoveListener(TurnSettingsOn);
        menuButton.onClick.RemoveListener(ReturnToMenu);
        backButton.onClick.RemoveListener(GoBackToPauseMenu);
    }


    private void SetCanvasState(CanvasGroup canvas, bool state)
    {
        canvas.alpha = state ? 1 : 0;
        canvas.interactable = state;
        canvas.blocksRaycasts = state;
    }


    public void SetPauseMenu(bool isGamePaused)
    {
        Time.timeScale = isGamePaused ? 0 : 1;
        SetCanvasState(pausedGameCanvas, isGamePaused);
        SetCanvasState(inGameCanvas, !isGamePaused);
        SetCanvasState(settingsCanvas, false);
    }

    public void SetGameOver()
    {
        Time.timeScale = 0;
        SetCanvasState(pausedGameCanvas, false);
        SetCanvasState(inGameCanvas, false);
        SetCanvasState(settingsCanvas, false);
        SetCanvasState(endScreenCanvas, true);
    }

    #region Buttons

    private void ResumePlay()
    {
        OnPausedGame.Invoke();
    }

    private void ResetScene()
    {
        Time.timeScale = 1;
        SceneSwitcher.ChangeScene(resetSceneData);
    }

    private void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneSwitcher.ChangeScene(menuSceneData);
    }

    private void TurnSettingsOn()
    {
        SetCanvasState(pausedGameCanvas, false);
        SetCanvasState(inGameCanvas, false);
        SetCanvasState(endScreenCanvas, false);
        SetCanvasState(settingsCanvas, true);
    }

    private void GoBackToPauseMenu()
    {
        SetCanvasState(pausedGameCanvas, true);
        SetCanvasState(settingsCanvas, false);
    }

    #endregion
}