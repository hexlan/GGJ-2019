using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        var flute = GameObject.FindGameObjectWithTag("Flute");

        if(flute && flute.GetComponent<AudioSource>().isPlaying)
        {
            _audioSource.mute = true;
        } else
        {
            _audioSource.mute = false;
        }
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
