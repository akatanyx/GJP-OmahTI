using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundList : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySlash()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
    public void PlaySlash2()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }
    public void PlaySlide()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
    public void PlayRunning()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }
    public void StopRunning()
    {
        audioSource.Stop();
    }
    public void PlayReload()
    {
        audioSource.PlayOneShot(audioClips[4]);
    }
    public void PlayShoot()
    {
        audioSource.PlayOneShot(audioClips[5]);
    }
    //public void PlaySlash()
    //{
        
    //}
    //public void PlaySlash()
    //{
        
    //}
    //public void PlaySlash()
    //{
        
    //}
}
