using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private List<AudioComponent> audios = new List<AudioComponent>();
    [SerializeField] private AudioMixerGroup effectGroup;

    public void PlaySound (Transform origin, AudioClip clip)
    {
        AudioComponent audio = audios.Find(t => !t.IsEnable);
        if (audio == null)
            audio = CreateNewAudio();

        audio.Play(origin, clip);
    }

    private AudioComponent CreateNewAudio ()
    {
        GameObject go = new GameObject("CreatedAudio");
        go.transform.parent = transform;

        AudioComponent audioComponent = go.AddComponent<AudioComponent>();

        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = effectGroup;
        audioSource.playOnAwake = false;

        return audioComponent;
    }
}