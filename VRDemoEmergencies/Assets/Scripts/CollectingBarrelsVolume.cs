using UnityEngine;

public class CollectingBarrelsVolume : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
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
}
