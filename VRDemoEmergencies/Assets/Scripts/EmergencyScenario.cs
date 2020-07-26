using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyScenario : MonoBehaviour
{
    public Transform playerHeadPosition;

    public IndicationsPanel myIndicationPanel;

    public EmergencyScenario nextEmergencyScenario;

    private bool isNextScenarioLoaded;

    // Start is called before the first frame update
    void Start()
    {
        isNextScenarioLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateScenario() {

        Player.instance.transform.position = playerHeadPosition.position;
        myIndicationPanel.gameObject.SetActive(true);


    }

    public void ScenarioCompleted()
    {
        if (isNextScenarioLoaded)
        {
            return;
        }
        Debug.Log("ScenarioCompleted");
        if(nextEmergencyScenario != null)
        nextEmergencyScenario.InitiateScenario();
        isNextScenarioLoaded = true;
    }
}

public abstract class EmergencyScenarioQuestObject : MonoBehaviour
{
    public EmergencyScenario myEmergencyScenario;

    private void QuestCompleted()
    {
        myEmergencyScenario.ScenarioCompleted();
    }
}