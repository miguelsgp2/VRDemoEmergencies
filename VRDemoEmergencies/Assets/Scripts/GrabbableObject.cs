using UnityEngine;

public class GrabbableObject : MonoBehaviour, Interactable
{
    // Components in GameObject
    Rigidbody rigidBody;


    ScreenAim screenAim;
    private bool isGrabbed;

    private float distaceToPlayerWhenGrabbed = 6f;

    Transform playerTransform;
    public float forceProportionalController = 100f;
    public float maxSpeedGrabbed = 3;

    private float hitSoundDelay = 1f;
    private float hitSoundTimer;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        screenAim = GameObject.FindObjectOfType<ScreenAim>();
        playerTransform = GameObject.FindObjectOfType<Player>().transform;
        isGrabbed = false;
        hitSoundTimer = hitSoundDelay;
    }

    // Update is called once per frame
    void Update()
    {
        hitSoundTimer -= Time.deltaTime;
        //if (isGrabbed)
        //{
        //    //Debug.Log("grabbed");
        //    MoveToPositionOfScreenAim();
        //}
    }

    private void FixedUpdate()
    {
        if (isGrabbed)
        {
            //Debug.Log("grabbed");
            //MoveToPositionOfScreenAim();
            PushToPositionRelativeToScreenAim();
        }
    }
    private void PushToPositionRelativeToScreenAim()
    {

        var directionToPlayer = playerTransform.rotation * Vector3.forward;
        var targetPosition = playerTransform.position + directionToPlayer * distaceToPlayerWhenGrabbed;

        targetPosition = new Vector3(targetPosition.x, Mathf.Clamp(targetPosition.y, 1.0f, Mathf.Infinity), targetPosition.z);

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);

        var errorPosition = targetPosition - transform.position;

        var forceToApply = errorPosition * forceProportionalController;

        rigidBody.AddForce(forceToApply);
        rigidBody.velocity = errorPosition.magnitude * Mathf.Clamp(rigidBody.velocity.magnitude, 0f, maxSpeedGrabbed) * rigidBody.velocity.normalized;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(hitSoundTimer <= 0f)
        {
            hitSoundTimer = hitSoundDelay;
            AudioManager.instance.PlayEffect("MetalBarrelHit",transform.position);
        }
    }
}
