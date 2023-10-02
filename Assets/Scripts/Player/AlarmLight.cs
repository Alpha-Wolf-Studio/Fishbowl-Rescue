using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private GameObject lightObject;
    private bool lastStateAlarmOn = false;
    private bool isAlarmOn = false;

    private void Update ()
    {
        isAlarmOn = false;
        for (int i = 0; i < GameManager.Instance.Sharks.Count; i++)
        {
            if (GameManager.Instance.Sharks[i].SharkController.isFocus)
            {
                isAlarmOn = true;
            }
        }

        if (isAlarmOn)
        {
            if (!lastStateAlarmOn)
            {
                SetAlarm(true);
            }
        }
        else
        {
            if (lastStateAlarmOn)
            {
                SetAlarm(false);
            }
        }

        transform.Rotate(Vector3.up * (speed * Time.deltaTime));
        isAlarmOn = lastStateAlarmOn;
    }

    void SetAlarm (bool state)
    {
        lightObject.SetActive(state);
    }
}