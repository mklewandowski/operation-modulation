using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    AudioClip MenuSound;

    [SerializeField]
    AudioClip SelectSound;

    [SerializeField]
    AudioClip CorrectSound;

    [SerializeField]
    AudioClip WrongSound;

    [SerializeField]
    AudioClip InvalidSound;

    [SerializeField]
    AudioClip MissSound;

    [SerializeField]
    AudioClip SuccessSound;

    [SerializeField]
    AudioClip MusicTrack;

    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        audioSource.clip = MusicTrack;
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayMenuSound()
    {
        if (!Globals.AudioOn) return;
        audioSource.PlayOneShot(MenuSound, 1f);
    }

    public void PlaySelectSound()
    {
        if (!Globals.AudioOn) return;
        audioSource.PlayOneShot(SelectSound, 1f);
    }

    public void PlayCorrectSound()
    {
        if (!Globals.AudioOn) return;
        audioSource.PlayOneShot(CorrectSound, 1f);
    }

    public void PlayWrongSound()
    {
        if (!Globals.AudioOn) return;
        audioSource.PlayOneShot(WrongSound, 1f);
    }

    public void PlayInvalidSound()
    {
        if (!Globals.AudioOn) return;
        audioSource.PlayOneShot(InvalidSound, 1f);
    }
    public void PlayMissSound()
    {
        if (!Globals.AudioOn) return;
        audioSource.PlayOneShot(MissSound, 1f);
    }
    public void PlaySuccessSound()
    {
        if (!Globals.AudioOn) return;
        audioSource.PlayOneShot(SuccessSound, 1f);
    }

}
