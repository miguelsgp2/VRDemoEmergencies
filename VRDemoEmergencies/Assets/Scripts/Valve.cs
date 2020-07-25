using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Valve : MonoBehaviour
{
    private float valveOpenessPerCent;
    public UnityEvent ValveOpenessModifiedEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        valveOpenessPerCent = 100;
        ValveOpenessModifiedEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OpenValve10pCent();
            Debug.Log("openning valve");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            CloseValve10pCent();
        }
    }

    public void OpenValve10pCent()
    {
        valveOpenessPerCent = Mathf.Clamp(valveOpenessPerCent + 10f, 0, 100);
        Debug.Log("valve openned");
        ValveOpenessModifiedEvent.Invoke();
    }

    public void CloseValve10pCent()
    {
        valveOpenessPerCent = Mathf.Clamp(valveOpenessPerCent - 10f, 0, 100);
        ValveOpenessModifiedEvent.Invoke();
    }

    public float GetValveOpenessPerCent()
    {
        return valveOpenessPerCent;
    }

}

public enum ValveActuatorType { OPEN_VALVE_ACTUATOR, CLOSE_VALVE_ACTUATOR}
