using UnityEngine;

public class CollectingBarrelsVolume : MonoBehaviour
{
    private AudioSource alarmAudioSource;
    private AudioSource audioSource;

    private void Start()
    {
        //alarmAudioSource = AudioManager.instance.
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ToxicChemical>())
        {
            Debug.Log("Collection Correct");
            audioSource.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ToxicChemical>())
        {
            Debug.Log("Collection Correct");
            audioSource.Play();
        }
    }

    private void StartAlarm()
    {

    }
}
