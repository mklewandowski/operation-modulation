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

    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayMenuSound()
    {
        audioSource.PlayOneShot(MenuSound, 1f);
    }

    public void PlaySelectSound()
    {
        audioSource.PlayOneShot(SelectSound, 1f);
    }

    public void PlayCorrectSound()
    {
        audioSource.PlayOneShot(CorrectSound, 1f);
    }

    public void PlayWrongSound()
    {
        audioSource.PlayOneShot(WrongSound, 1f);
    }

    public void PlayInvalidSound()
    {
        audioSource.PlayOneShot(InvalidSound, 1f);
    }
    public void PlayMissSound()
    {
        audioSource.PlayOneShot(MissSound, 1f);
    }
    public void PlaySuccessSound()
    {
        audioSource.PlayOneShot(SuccessSound, 1f);
    }

}
