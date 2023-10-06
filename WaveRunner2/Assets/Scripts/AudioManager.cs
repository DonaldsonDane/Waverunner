using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class AudioManager : MonoBehaviour
{
    public EazySoundManagerDemo easySoundManager;

    public AudioClip mainTheme;

    // Start is called before the first frame update

    void Awake()
    {
        EazySoundManager.PlayMusic(mainTheme);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
