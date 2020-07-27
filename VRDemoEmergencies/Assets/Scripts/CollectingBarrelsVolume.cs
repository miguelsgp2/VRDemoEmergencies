using UnityEngine;

public class CollectingBarrelsVolume : EmergencyScenarioQuestObject
{
    private AudioSource alarmAudioSource;

    private void Start()
    {
        //alarmAudioSource = AudioManager.instance.
        alarmAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ToxicChemical>())
        {
            Debug.Log("Collection Correct");
            alarmAudioSource.Stop();
            //myEmergencyScenario.ScenarioCompleted();
            QuestCompleted();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ToxicChemical>())
        {
            Debug.Log("Collection Correct");
            alarmAudioSource.Play();
        }
    }

    private void StartAlarm()
    {

    }
}
