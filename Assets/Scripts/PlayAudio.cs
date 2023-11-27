using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound()
    {
        _audioSource.Play();
    }
    public void StopSound()
    {
        _audioSource.Stop();
    }
}
