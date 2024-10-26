using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip blip;
    public AudioClip crash;
    public AudioClip blast;

    public static SoundEffects singleton;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }
}
