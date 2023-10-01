using System;
using UnityEngine;
using UnityEngine.UI;

public class UiControllerSettings : MonoBehaviour
{
    private const float maxVolume = 80;

    public event Action onSettingsCloseButtonClicked;

    [SerializeField] private UnityEngine.Audio.AudioMixer audioMixer;
    [SerializeField] private AnimationCurve logarithm;

    public CanvasGroup canvasGroup;
    [SerializeField] private Slider sliderVolumeGeneral;
    [SerializeField] private Slider sliderVolumeMusic;
    [SerializeField] private Slider sliderVolumeEffect;
    [SerializeField] private Toggle toggleInvertX;
    [SerializeField] private Toggle toggleInvertY;
    [SerializeField] private Toggle toggleFishEye;
    [SerializeField] private Button closeButton;
    [SerializeField] private PlayerStats playerStats;

    private void Awake ()
    {
        sliderVolumeGeneral.onValueChanged.AddListener(OnSliderVolumeGeneralChanged);
        sliderVolumeMusic.onValueChanged.AddListener(OnSliderVolumeMusicChanged);
        sliderVolumeEffect.onValueChanged.AddListener(OnSliderVolumeEffectChanged);
        closeButton.onClick.AddListener(OnSettingsCloseButtonClicked);
        toggleInvertX.onValueChanged.AddListener(OnToggleXClick);
        toggleInvertY.onValueChanged.AddListener(OnToggleYClick);
        toggleFishEye.onValueChanged.AddListener(OnToggleFishEye);
    }


    private void Start ()
    {
        toggleInvertX.SetIsOnWithoutNotify(playerStats.signX == 1);
        toggleInvertY.SetIsOnWithoutNotify(playerStats.signY == 1);
    }

    private void OnDestroy ()
    {
        sliderVolumeGeneral.onValueChanged.RemoveAllListeners();
        sliderVolumeMusic.onValueChanged.RemoveAllListeners();
        sliderVolumeEffect.onValueChanged.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        toggleInvertX.onValueChanged.RemoveAllListeners();
        toggleInvertY.onValueChanged.RemoveAllListeners();
        toggleFishEye.onValueChanged.RemoveAllListeners();
    }

    private void OnSliderVolumeGeneralChanged (float volume)
    {
        float newValue = logarithm.Evaluate(volume) * maxVolume - maxVolume;
        audioMixer.SetFloat("VolumeGeneral", newValue);
    }

    private void OnSliderVolumeMusicChanged (float volume)
    {
        float newValue = logarithm.Evaluate(volume) * maxVolume - maxVolume;
        audioMixer.SetFloat("VolumeMusic", newValue);
    }

    private void OnSliderVolumeEffectChanged (float volume)
    {
        float newValue = logarithm.Evaluate(volume) * maxVolume - maxVolume;
        audioMixer.SetFloat("VolumeEffect", newValue);
    }

    private void OnToggleXClick (bool newValue)
    {
        playerStats.signX = newValue ? 1 : -1;
    }

    private void OnToggleYClick (bool newValue)
    {
        playerStats.signY = newValue ? 1 : -1;
    }
    private void OnToggleFishEye(bool newValue)
    {
        playerStats.activateFishEye = newValue;
    }

    private void OnSettingsCloseButtonClicked () => onSettingsCloseButtonClicked?.Invoke();
}