using UnityEngine;

public class FlowControlledByValve : EmergencyScenarioQuestObject
{
    public ParticleSystem waterFlowParticleSystem;
    public Valve associatedValve;
    public AudioSource waterAudioSource;
    private float waterAudioSourceMaxVol = 0.4f;
    private int maxParticlesFlow = 200;
    private float maxparticlesSpeed = 12f;
    private int currentParticlesFlow;
    // Start is called before the first frame update
    void Start()
    {
        associatedValve.ValveOpenessModifiedEvent.AddListener(OnValveOpenessModifiedTriggered);
        OnValveOpenessModifiedTriggered();
    }

    private void OnValveOpenessModifiedTriggered()
    {
        UpdateWaterSoundvolume();

        var valveOpenessPerCent = associatedValve.GetValveOpenessPerCent();

        currentParticlesFlow = Mathf.RoundToInt(maxParticlesFlow * valveOpenessPerCent / 100f);
        var emission = waterFlowParticleSystem.emission;
        emission.rateOverTime = (float)currentParticlesFlow;

        var particlesSpeed = maxparticlesSpeed * (associatedValve.GetValveOpenessPerCent() / 100f);
        var main = waterFlowParticleSystem.main;
        main.startSpeed = particlesSpeed;

        if(valveOpenessPerCent <= 0)
        {
            //myEmergencyScenario.ScenarioCompleted();
            QuestCompleted();
        }
    }

    private void UpdateWaterSoundvolume()
    {
        var valveOpenessPerCent = associatedValve.GetValveOpenessPerCent();

        var currentVolume = waterAudioSourceMaxVol * valveOpenessPerCent / 100f;
        Debug.Log("currentVolume " + currentVolume);

        waterAudioSource.volume = currentVolume;
    }
}
