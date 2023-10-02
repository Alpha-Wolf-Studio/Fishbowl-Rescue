using System.Collections;
using CustomPlayFabAPI;
using CustomSceneSwitcher.Examples.Scripts;
using UnityEngine;
using TMPro;

public class UiScreenMainMenu : MonoBehaviour
{
    [SerializeField] private ChangeSceneUI changeSceneUI;
    [SerializeField] private UiControllerMainMenu controllerMainMenu;
    [SerializeField] private UiControllerSettings controllerSettings;
    [SerializeField] private UiControllerCredits controllerCredits;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float durationFade = 1.0f;
    [SerializeField] private TMP_Text textFishes;
    [SerializeField] private TMP_Text textShark;
    [SerializeField] private TMP_Text textTime;

    private IEnumerator switchPanel;
    private int stateLoaded = 0;

    private void Awake ()
    {
        controllerMainMenu.onPlayButtonClicked += ControllerMainMenu_onPlayButtonClicked;
        controllerMainMenu.onSettingsButtonClicked += ControllerMainMenu_onSettingsButtonClicked;
        controllerMainMenu.onCreditsButtonClicked += ControllerMainMenu_onCreditsButtonClicked;
        controllerSettings.onSettingsCloseButtonClicked += ControllerSettings_onSettingsCloseButtonClicked;
        controllerCredits.onCreditsCloseButtonClicked += ControllerCredits_onCreditsCloseButtonClicked;
    }

    private void Start ()
    {
        CustomPlayFabSingleton.Instance.OnClientDataGetResult += Instance_OnLoginResult;
        SetPanel(controllerMainMenu.canvasGroup, true);
        SetPanel(controllerSettings.canvasGroup, false);
        SetPanel(controllerCredits.canvasGroup, false);
    }

    private void Update ()
    {
        if (stateLoaded == 1)
        {
            stateLoaded = 2;
            textFishes.text = CustomPlayFabSingleton.Instance.UserData.SharkHits.ToString();
            textShark.text = CustomPlayFabSingleton.Instance.UserData.FishesCapture.ToString();
            textTime.text = ConvertSecondsToTimer(CustomPlayFabSingleton.Instance.UserData.TimePlayed);
        }
    }

    private void OnDestroy ()
    {
        CustomPlayFabSingleton.Instance.OnClientDataGetResult -= Instance_OnLoginResult;
        controllerMainMenu.onPlayButtonClicked -= ControllerMainMenu_onPlayButtonClicked;
        controllerMainMenu.onSettingsButtonClicked -= ControllerMainMenu_onSettingsButtonClicked;
        controllerMainMenu.onCreditsButtonClicked -= ControllerMainMenu_onCreditsButtonClicked;
        controllerSettings.onSettingsCloseButtonClicked -= ControllerSettings_onSettingsCloseButtonClicked;
        controllerCredits.onCreditsCloseButtonClicked -= ControllerCredits_onCreditsCloseButtonClicked;
    }

    private string ConvertSecondsToTimer(float time)
    {
        float minutes = time / 60;
        float seconds = time % 60;
        string mins = minutes < 10 ? $"0{minutes:F0}" : minutes.ToString("F0");
        string secs = seconds < 10 ? $"0{seconds:F0}" : seconds.ToString("F0");
        return $"{mins}:{secs}";
    }

    private void Instance_OnLoginResult(bool isLoaded)
    {
        if (isLoaded)
        {
            stateLoaded = 1;
        }
    }

    private void ControllerMainMenu_onPlayButtonClicked () => changeSceneUI.ChangeScene();
    private void ControllerMainMenu_onSettingsButtonClicked () => SwitchController(durationFade, controllerSettings.canvasGroup, controllerMainMenu.canvasGroup);
    private void ControllerMainMenu_onCreditsButtonClicked () => SwitchController(durationFade, controllerCredits.canvasGroup, controllerMainMenu.canvasGroup);

    private void ControllerMainMenu_onExitButtonClicked ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Debug.Log("No Exit");
#else
        Application.Quit();
#endif
    }

    private void ControllerCredits_onCreditsCloseButtonClicked () => SwitchController(durationFade, controllerMainMenu.canvasGroup, controllerCredits.canvasGroup);
    private void ControllerSettings_onSettingsCloseButtonClicked () => SwitchController(durationFade, controllerMainMenu.canvasGroup, controllerSettings.canvasGroup);

    private void SwitchController (float duration, CanvasGroup on, CanvasGroup off)
    {
        if (switchPanel != null)
            StopCoroutine(switchPanel);

        switchPanel = SwitchPanel(duration, on, off);
        StartCoroutine(switchPanel);
    }

    private IEnumerator SwitchPanel (float duration, CanvasGroup on, CanvasGroup off)
    {
        SetPanel(off, true);
        SetPanel(on, false);

        off.blocksRaycasts = false;
        off.interactable = false;

        float currentDuration = 0;
        while (currentDuration < duration)
        {
            currentDuration += Time.unscaledDeltaTime;
            float fade = animationCurve.Evaluate(currentDuration / duration);
            on.alpha = fade;
            off.alpha = 1 - fade;
            yield return null;
        }

        SetPanel(on, true);
    }

    private void SetPanel (CanvasGroup canvasGroup, bool isEnable)
    {
        canvasGroup.alpha = isEnable ? 1 : 0;
        canvasGroup.blocksRaycasts = isEnable;
        canvasGroup.interactable = isEnable;
    }
}