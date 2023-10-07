using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class PlaySound : MonoBehaviour
{

    public InitializeManager _im;

    public void PlaySoundSFX(AudioClip clip)
    {
        EazySoundManager.PlaySound(clip);
    }



    public void BeginMusicTheme(AudioClip musicClip)
    {
        EazySoundManager.PlayMusic(musicClip);
    }



    public void CountdownFinished()
    {
        _im.EnableAI();
    }
}
