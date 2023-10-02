using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    public bool IsEnable { get; private set; }
    private AudioSource audioSource;
    private Transform target;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play (Transform origin, AudioClip clip)
    {
        gameObject.SetActive(true);
        IsEnable = true;
        target = origin;
        transform.position = origin.position;
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void Update ()
    {
        if (target)
            transform.position = target.position;

        if (!audioSource.isPlaying)
        {
            target = null;
            gameObject.SetActive(false);
            IsEnable = false;
        }
    }
}