// An asset that contains a collection of audio clips.
// Source Unity Game Development Cookbook
using UnityEngine;

[CreateAssetMenu]

public class SoundEffect : ScriptableObject
{
    // the list of AudioClips that might be played when this sound is played.

    public AudioClip[] clips;

    // Randomly selects an AudioClip from the 'clips' array, if one is available
    public AudioClip GetRandomClip()
    {
        if(clips.Length == 0)
        {
            return null;
        }
        return clips[Random.Range(0, clips.Length)];
    }
}