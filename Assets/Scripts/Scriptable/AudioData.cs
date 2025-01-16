using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AudioData", menuName = "Data/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    public AudioClip cardFlip;
    public AudioClip cardMatch;
    public AudioClip cardMismatch;
    public AudioClip gameWin;
}
