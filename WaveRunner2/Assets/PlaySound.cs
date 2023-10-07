using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class PlaySound : MonoBehaviour
{
    public void PlaySoundSFX(AudioClip clip)
    {
        EazySoundManager.PlaySound(clip);
    }
}
