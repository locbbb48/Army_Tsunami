using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public AudioSource BgrkAudioSource;
    public AudioSource VfxAudioSource1shot;
    public AudioSource VfxAudioSource;

    public AudioClip Click;

    public AudioClip BrgkClip;
    public AudioClip StartGameClip;

    public AudioClip CreateAnPlayer;
    public AudioClip DestroyAnPlayer;

    public AudioClip HumanMove;
    public AudioClip HumanDestroy;

    public AudioClip Campfire;
    public AudioClip FireBall;

    public AudioClip Clouds;
    public AudioClip Tesla;

    private void Awake()
    {
        instance = this;
        PlaySfxAudio1shot(StartGameClip);
        BgrkAudioSource.clip = BrgkClip;
        BgrkAudioSource.Play();
    }

    public void PlaySfxAudio1shot(AudioClip SfxClip)
    {
        VfxAudioSource1shot.clip = SfxClip;
        VfxAudioSource1shot.PlayOneShot(VfxAudioSource1shot.clip);
    }
    public void PlaySfxAudio(AudioClip SfxClip)
    {
        VfxAudioSource.clip = SfxClip;
        VfxAudioSource.Play();
    }

    public void PauseSfxAudio(AudioClip SfxClip)
    {
        if (SfxClip != null)
        {
            VfxAudioSource.clip = SfxClip;
            VfxAudioSource.Pause();
        }
    }
}
