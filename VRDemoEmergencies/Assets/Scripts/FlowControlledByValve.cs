using UnityEngine;

public class FlowControlledByValve : MonoBehaviour
{
    public ParticleSystem waterFlowParticleSystem;
    public Valve associatedValve;
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
        //Debug.Log("chaning particle system");
        currentParticlesFlow = Mathf.RoundToInt(maxParticlesFlow * associatedValve.GetValveOpenessPerCent() / 100f);
        var emission = waterFlowParticleSystem.emission;
        emission.rateOverTime = (float)currentParticlesFlow;

        var particlesSpeed = maxparticlesSpeed * (associatedValve.GetValveOpenessPerCent() / 100f);
        var main = waterFlowParticleSystem.main;
        main.startSpeed = particlesSpeed;
    }
}
