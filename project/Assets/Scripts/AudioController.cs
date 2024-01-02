using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip[] audios;
    AudioSource[] audiosAS;
    [SerializeField] GameObject prefab;
    // Start is called before the first frame update
    public static AudioController instance;
    void Awake()
    {
        audiosAS = new AudioSource[audios.Length];
        instance = this;
    }
    public void PlaySound(int i = 0, float volume = 1f, bool isLoopback = false, bool repeat = false)
    {
        Play(audios[i], ref audiosAS[i]);
    }

    void Play(AudioClip clip, ref AudioSource audioSource, float volume = 1f, bool isLoopback = false, bool repeat = false)
    {
        if (audioSource != null && !repeat)
            return;
        audioSource = Instantiate(instance.prefab).GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = isLoopback;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}

