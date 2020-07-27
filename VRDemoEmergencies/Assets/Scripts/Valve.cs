using UnityEngine;
using UnityEngine.Events;

public class Valve : MonoBehaviour
{
    private float valveOpenessPerCent = 100f;
    public UnityEvent ValveOpenessModifiedEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        valveOpenessPerCent = 100;
        ValveOpenessModifiedEvent.Invoke();
    }

    public void OpenValve10pCent()
    {
        valveOpenessPerCent = Mathf.Clamp(valveOpenessPerCent + 10f, 0, 100);
        ValveOperated();
        //Debug.Log("valve openned");
        //AudioManager.instance.PlayEffect("ValveMoving");
        //ValveOpenessModifiedEvent.Invoke();
    }

    public void CloseValve10pCent()
    {
        valveOpenessPerCent = Mathf.Clamp(valveOpenessPerCent - 10f, 0, 100);
        ValveOperated();
        //ValveOpenessModifiedEvent.Invoke();
    }

    public float GetValveOpenessPerCent()
    {
        return valveOpenessPerCent;
    }

    private void ValveOperated()
    {
        if(valveOpenessPerCent != 0 || valveOpenessPerCent != 100)
        {
            AudioManager.instance.PlayEffect("ValveMoving",transform.position);
        }
        else
        {
            AudioManager.instance.PlayEffect("ValveFullyOperated",transform.position);
        }

        ValveOpenessModifiedEvent.Invoke();
    }

}

public enum ValveActuatorType { OPEN_VALVE_ACTUATOR, CLOSE_VALVE_ACTUATOR}
