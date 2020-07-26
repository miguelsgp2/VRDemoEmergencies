using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixer audioMixer;
    public GameObject audioSourcePrefab;
    // The list of references to SoundEffect assets
    public SoundEffect[] effects;
    public AudioClip[] backgroundMusic;

    private GameObject backgroundMusicPlayerGO;

// A dictionary that maps the names of SoundEffectsto the objects themselves,
// to make it faster to look them up.
    private Dictionary<string, SoundEffect> _effectDictionary;
    private Dictionary<string, AudioClip> _backgroundMusicDictionary;

    // A reference to the current audio listener which we use to place audio clips.

    private AudioListener _listener;

    private void Awake()
    {
        // When the manager wakes up, build a dictionary of named sounds, so
        // that we can wuickly access them when needed

        _effectDictionary = new Dictionary<string, SoundEffect>();
        foreach(var effect in effects)
        {
            Debug.LogFormat("registered effect {0}", effect.name);
            _effectDictionary[effect.name] = effect;
        }
        _backgroundMusicDictionary = new Dictionary<string, AudioClip>();
        foreach (var song in backgroundMusic)
        {
            Debug.LogFormat("registered song {0}", song.name);
            _backgroundMusicDictionary[song.name] = song;
        }
    }


    // Plays a sound effect by name, at the same position as the audio listener.
    public void PlayEffect(string effectName)
    {
        // If we don't currently have a listener (ot the reference we had was destroyed)
        // find a new one
        if(_listener == null)
        {
            _listener = FindObjectOfType<AudioListener>();
        }

        // Play the effect at the listener's position
        PlayEffect(effectName, _listener.transform.position);
    }

    public void PlayEffect(string effectName, string audioMixerGroup)
    {
        // If we don't currently have a listener (ot the reference we had was destroyed)
        // find a new one
        if (_listener == null)
        {
            _listener = FindObjectOfType<AudioListener>();
        }

        // Play the effect at the listener's position
        PlayEffect(effectName, _listener.transform.position,audioMixerGroup);
    }

    // Plays a sound effect by name, at a specified position in the world
    public void PlayEffect(string effectName, Vector3 worldPosition)
    {
        PlayEffect(effectName, worldPosition, "Master"); //Play in Effects group by default if there is no group selected.
    }

    public void PlayEffect(string effectName, Vector3 worldPosition, string audioMixerGroup)
    {
        // Does the sound effect exists?
        if (_effectDictionary.ContainsKey(effectName) == false)
        {
            Debug.LogWarningFormat("Effect {0} is not registered.", effectName);
            return;
        }

        // Get a clip from the effect
        var clip = _effectDictionary[effectName].GetRandomClip();

        // Make sure it wasn't null
        if (clip == null)
        {
            Debug.LogWarningFormat("Effect {0} has no clips to play.", effectName);
            return;
        }
        // Play the selected clip a the specified point
        //var tempGO = new GameObject(); // GameObject.Instantiate(GameObject());
        var tempGO = Instantiate(audioSourcePrefab);
        tempGO.name ="TempAudio"; // create the temp object
        tempGO.transform.position = worldPosition; // set its position
        var aSource = tempGO.GetComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        // set other aSource properties here, if desired
        //var audioMixer = Resources.Load("Master") as AudioMixer;

        aSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(audioMixerGroup)[0];
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
    }

    public void PlayEffect(string effectName, Transform parentSource, string audioMixerGroup)
    {
        // Does the sound effect exists?
        if (_effectDictionary.ContainsKey(effectName) == false)
        {
            Debug.LogWarningFormat("Effect {0} is not registered.", effectName);
            return;
        }

        // Get a clip from the effect
        var clip = _effectDictionary[effectName].GetRandomClip();

        // Make sure it wasn't null
        if (clip == null)
        {
            Debug.LogWarningFormat("Effect {0} has no clips to play.", effectName);
            return;
        }
        // Play the selected clip a the specified point
        //var tempGO = new GameObject(); // GameObject.Instantiate(GameObject());
        var tempGO = Instantiate(audioSourcePrefab);
        tempGO.name = "TempAudio"; // create the temp object
        tempGO.transform.SetParent(parentSource); // set its position
        var aSource = tempGO.GetComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        // set other aSource properties here, if desired
        //var audioMixer = Resources.Load("Master") as AudioMixer;

        aSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(audioMixerGroup)[0];
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
    }

    public void PlayBackgroundMusic(string songName)
    {
        // If the object that will play background music doesn't exist create it
        if(backgroundMusicPlayerGO == null)
        {
            backgroundMusicPlayerGO = Instantiate(audioSourcePrefab);
            backgroundMusicPlayerGO.name = "BackgroundMusicAudioSource";
            // This object is child of the camera so source is in the listener

            backgroundMusicPlayerGO.transform.SetParent(Camera.main.transform);
        }
        // Get the song clip
        var songClip = _backgroundMusicDictionary[songName];

        var audioSource = backgroundMusicPlayerGO.GetComponent<AudioSource>();
        audioSource.clip = songClip;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        audioSource.loop = true;
        audioSource.Play();
        StartCoroutine(FadeVolume(0f, 1f, 3f, audioSource));

        //audioSource.clip = 
    }
    
    private IEnumerator FadeVolume(float initialVol, float finalVol, float fadeTime, AudioSource audioSource)
    {
        if (fadeTime <= 0)
        {
            Debug.Log("FadeTime for FadeVolume cannot be less or equal than 0");
            yield break;
        }
        int numberIterations = 10;
        float delayTimeIteration = fadeTime / numberIterations;
        WaitForSecondsRealtime waitForSecondsRealTimeIteration = new WaitForSecondsRealtime(delayTimeIteration);
        
        float direction;    // This control volumen is increasing or decreasing
       if(finalVol>initialVol)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        audioSource.volume = initialVol;

       while((direction)*audioSource.volume <= (direction) * finalVol)
        {
            yield return waitForSecondsRealTimeIteration;
            audioSource.volume += (finalVol - initialVol) / numberIterations;
        }
    }

    // Change Vol

    public void ChangeMixerMasterVolume(float _volume)
    {
        float dBVolume = LinearToDecibel(_volume);
        audioMixer.SetFloat("MasterVolume", dBVolume);
    }

    public void ChangeMixerMusicVolume(float _volume)
    {
        float dBVolume = LinearToDecibel(_volume);
        audioMixer.SetFloat("MusicVolume", dBVolume);
    }

    public void ChangeMixerEffectsVolume(float _volume)
    {
        float dBVolume = LinearToDecibel(_volume);
        audioMixer.SetFloat("EffectsVolume", dBVolume);
    }

    public void ChangeGameEffectsVolume(float _volume)
    {
        float dBVolume = LinearToDecibel(_volume);
        audioMixer.SetFloat("GameEffectsVolume", dBVolume);
    }

    public void ChangeUIEffectsVolume(float _volume)
    {
        float dBVolume = LinearToDecibel(_volume);
        audioMixer.SetFloat("UIEffectsVolume", dBVolume);
    }

    public void ChangeVoiceEffectsVolume(float _volume)
    {
        float dBVolume = LinearToDecibel(_volume);
        audioMixer.SetFloat("VoiceEffectsVolume", dBVolume);
    }

    // Get volume
    public float GetMixerMasterVolume()
    {

        float outValue;
        audioMixer.GetFloat("MasterVolume", out outValue);
        return DecibelToLinear(outValue);
    }

    public float GetMixerMusicVolume()
    {
        float outValue;
        audioMixer.GetFloat("MusicVolume", out outValue);
        return DecibelToLinear(outValue);
    }

    public float GetMixerEffectsVolume()
    {
        float outValue;
        audioMixer.GetFloat("EffectsVolume", out outValue);
        return DecibelToLinear(outValue);
    }

    public float GetGameEffectsVolume()
    {
        float outValue;
        audioMixer.GetFloat("GameEffectsVolume", out outValue);
        return DecibelToLinear(outValue);
    }

    public float GetUIEffectsVolume()
    {
        float outValue;
        audioMixer.GetFloat("UIEffectsVolume", out outValue);
        return DecibelToLinear(outValue);
    }

    public float GetVoiceEffectsVolume()
    {
        float outValue;
        audioMixer.GetFloat("VoiceEffectsVolume", out outValue);
        return DecibelToLinear(outValue);
    }





    private float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);

        return linear;
    }
}
