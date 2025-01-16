using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private AudioSource effectSrc;
    [SerializeField] private AudioSource backGroundSrc;
    [SerializeField] private AudioClip backGroundMusic;

    private bool isBackgroundMusicMute = false;
    private bool isEffectsMute = false;

    private void Start()
    {
        PlayBG(backGroundMusic); // Optionally play default BG at the start
    }
    // Play background music
    public void PlayBG(AudioClip bgClip)
    {
        if (isBackgroundMusicMute || backGroundMusic == null) return;

        backGroundSrc.clip = bgClip;
        backGroundSrc.loop = true;
        backGroundSrc.Play();
    }

    // Stop background music
    public void StopBG()
    {
        backGroundSrc.Stop();
    }

    // Play sound effect
    public void PlayEffect(AudioClip effectClip)
    {
        if (isEffectsMute || effectClip == null) return;

        effectSrc.PlayOneShot(effectClip);
    }

    // Toggle background music on/off
    public void ToggleBG()
    {
        isBackgroundMusicMute = !isBackgroundMusicMute;

        if (isBackgroundMusicMute)
            backGroundSrc.Pause();
        else
            backGroundSrc.UnPause();
    }

    // Toggle sound effects on/off
    public void ToggleFX()
    {
        isEffectsMute = !isEffectsMute;
    }

    // Check if BG is muted
    public bool IsBackgroundMusicMute()
    {
        return isBackgroundMusicMute;
    }

    // Check if FX is muted
    public bool IsEffectsMute()
    {
        return isEffectsMute;
    }
}