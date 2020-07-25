using UnityEngine;

public class ValveActuator : MonoBehaviour, Interactable
{
    public ValveActuatorType valveActuatorType;

    public Valve associatedValve;

    public void Interact()
    {
        switch (valveActuatorType)
        {
            case ValveActuatorType.OPEN_VALVE_ACTUATOR:
                associatedValve.OpenValve10pCent();
                break;
            case ValveActuatorType.CLOSE_VALVE_ACTUATOR:
                associatedValve.CloseValve10pCent();
                break;
            default:
                Debug.Log("Error valve actuator not set trying to interact "+ gameObject.name);
                break;
        }
    }


}
