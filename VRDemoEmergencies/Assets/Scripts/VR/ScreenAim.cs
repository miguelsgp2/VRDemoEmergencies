using UnityEngine;

public class ScreenAim : MonoBehaviour
{

    public LayerMask InteractableLayerMask;

    public Interactable InteractWithObjectAimed()
    {
        var objTransform = GetTransformCollidedByRayCastPerpendicularToScreen();
        if(objTransform != null)
        {
            var interactableObj = TryToInteractWithObject(objTransform);
            return interactableObj;
        }
        else
        {
            AudioManager.instance.PlayEffect("ErrorNoInteraction");
        }
        return null;
    }

    private Transform GetTransformCollidedByRayCastPerpendicularToScreen()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(transform.position);

        if (Physics.Raycast(ray,  out hit, Mathf.Infinity,InteractableLayerMask))
        {

            Transform objectHit = hit.transform;
            Debug.Log("objectHit " + objectHit.gameObject.name);
            return objectHit;
        }
        return null;
    }

    private Interactable TryToInteractWithObject(Transform objectTransform)
    {
        if(objectTransform == null)
        {
            return null;
        }
            if(objectTransform.GetComponent<Interactable>() != null)
            {
                var interactableComponent = objectTransform.GetComponent<Interactable>();
                interactableComponent.Interact();
            return interactableComponent;
            }
        return null;
    }
}

public interface IScreenAimHandler
{
    void CommandInteract();
}