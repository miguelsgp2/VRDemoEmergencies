using UnityEngine;

public class GrabbableObject : MonoBehaviour, Interactable
{
    // Components in GameObject
    Rigidbody rigidBody;


    ScreenAim screenAim;
    private bool isGrabbed;

    private float distaceToPlayerWhenGrabbed = 10f;

    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        screenAim = GameObject.FindObjectOfType<ScreenAim>();
        playerTransform = GameObject.FindObjectOfType<Player>().transform;
        isGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            //Debug.Log("grabbed");
            MoveToPositionOfScreenAim();
        }
    }

    private void MoveToPositionOfScreenAim()
    {
        
        var directionToPlayer = playerTransform.rotation * Vector3.forward;
        var targetPosition = playerTransform.position + directionToPlayer * distaceToPlayerWhenGrabbed;

        targetPosition = new Vector3(targetPosition.x, Mathf.Clamp(targetPosition.y, 1.0f, Mathf.Infinity), targetPosition.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);

    }

    private void GrabbOrUngrabb()
    {
        if (isGrabbed)
        {
            UnGrab();
        }
        else
        {
            Grab();
        }
    }

    private void Grab()
    {
        isGrabbed = true;
        rigidBody.useGravity = false;
    }

    private void UnGrab()
    {
        isGrabbed = false;
        rigidBody.useGravity = true;
        rigidBody.velocity = Vector3.zero;
    }

    public void Interact()
    {
        GrabbOrUngrabb();
    }
}
