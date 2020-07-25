using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowControlledByValve : MonoBehaviour
{
    public ParticleSystem waterFlowParticleSystem;
    public Valve associatedValve;
    private int maxParticlesFlow = 200;
    private int currentParticlesFlow;
    // Start is called before the first frame update
    void Start()
    {
        currentParticlesFlow = maxParticlesFlow;
        associatedValve.ValveOpenessModifiedEvent.AddListener(OnValveOpenessModifiedTriggered);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValveOpenessModifiedTriggered()
    {
        Debug.Log("chaning particle system");
        currentParticlesFlow = Mathf.RoundToInt(maxParticlesFlow * associatedValve.GetValveOpenessPerCent() / 100f);

        var emission = waterFlowParticleSystem.emission;
        emission.rateOverTime = (float)currentParticlesFlow;
    }
}
