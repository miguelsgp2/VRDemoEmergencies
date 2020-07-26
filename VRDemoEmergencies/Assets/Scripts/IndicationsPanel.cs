using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicationsPanel : MonoBehaviour
{
    public List<GameObject> textGameObjects;
    public EmergencyScenario emergencyScenario;
    public IndicationsPanel nextIndicaitonsPanel;
    int indexActions;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var textObject in textGameObjects)
        {
            textObject.SetActive(false);
        }
        indexActions = 0;
        NextAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            NextAction();
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                NextAction();
            }
        }
    }

    private void NextAction()
    {
        if(indexActions < textGameObjects.Count)
        {
            ActivateNextText();
            return;
            
        }

        if(nextIndicaitonsPanel == null && emergencyScenario == null)
        {
            this.gameObject.SetActive(false);
            return;
        }

        if(nextIndicaitonsPanel != null)
        {
            nextIndicaitonsPanel.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            return;
        }

        if (emergencyScenario != null)
        {
            emergencyScenario.InitiateScenario();
            this.gameObject.SetActive(false);
        }
    }

    private void ActivateNextText()
    {
        textGameObjects[indexActions].gameObject.SetActive(true);
        indexActions++;
    }
}
