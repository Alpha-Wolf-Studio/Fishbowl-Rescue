using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiPlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Image imageWater;
    [SerializeField] private Image imageLife;
    [SerializeField] private TMP_Text textFishAmount;
    [SerializeField] private TMP_Text textSharkHitted;
    [SerializeField] private TMP_Text textTimer;

    private void Update ()
    {
        float waterNormalize = WaterManager.Instance.CurrentWater / WaterManager.Instance.MaxWater;
        float lifeNormalize = playerController.PlayerStats.currentLife / playerController.PlayerStats.maxLife;

        imageWater.fillAmount = waterNormalize;
        imageLife.fillAmount = lifeNormalize;
        textTimer.text = ConvertSecondsToTimer(GameManager.Instance.currentTime);
    }

    private string ConvertSecondsToTimer (float time)
    {
        float minutes = time / 60;
        float seconds = time % 60;
        string mins = minutes < 10 ? $"0{minutes:F0}" : minutes.ToString("F0");
        string secs = seconds < 10 ? $"0{seconds:F0}" : seconds.ToString("F0");
        return $"{mins}:{secs}";
    }

    public void UpdateFishAmount (int newValue) => textFishAmount.text = newValue.ToString();
    public void UpdateSharkHitted (int newValue) => textSharkHitted.text = newValue.ToString();
}