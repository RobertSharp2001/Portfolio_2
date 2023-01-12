using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Player : MonoBehaviour{
    public AudioClip[] clip;
    public AudioSource source;

    public void Playsound(int which) {
        if (which <= clip.Length) { source.PlayOneShot(clip[which], 1f); }
    }

    public void Playsound(int which, float volume) {
        if (which <= clip.Length) { source.PlayOneShot(clip[which], volume); }
        
    }
}
