using CustomPlayFabAPI;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UiControllerMainMenu : MonoBehaviour
{
    public event Action onPlayButtonClicked;
    public event Action onSettingsButtonClicked;
    public event Action onCreditsButtonClicked;

    public CanvasGroup canvasGroup;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button creditsButton;

    [SerializeField] private Button game1Button;
    [SerializeField] private Button game2Button;
    [SerializeField] private Button game3Button;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingButton.onClick.AddListener(OnSettingsButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        Cursor.lockState = CursorLockMode.None;
        game1Button.onClick.AddListener(OnGame1ButtonClicked);
        game2Button.onClick.AddListener(OnGame2ButtonClicked);
        game3Button.onClick.AddListener(OnGame3ButtonClicked);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        settingButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
        game1Button.onClick.RemoveAllListeners();
        game2Button.onClick.RemoveAllListeners();
        game3Button.onClick.RemoveAllListeners();
    }

    private void OnPlayButtonClicked() => onPlayButtonClicked?.Invoke();
    private void OnSettingsButtonClicked() => onSettingsButtonClicked?.Invoke();
    private void OnCreditsButtonClicked() => onCreditsButtonClicked?.Invoke();

    private void OnGame1ButtonClicked ()
    {
        Debug.Log("Click 1");
        Application.OpenURL("https://ldjam.com/events/ludum-dare/54/$371816");
    }

    private void OnGame2ButtonClicked ()
    {
        Debug.Log("Click 2");
        Application.OpenURL("");
    }

    private void OnGame3ButtonClicked ()
    {
        Debug.Log("Click 3");
        Application.OpenURL("https://ldjam.com/events/ludum-dare/54/$371819");
    }
}