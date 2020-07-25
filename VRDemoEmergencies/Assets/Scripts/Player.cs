using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> , IScreenAimHandler
{

    List<Interactable> interactableObjectsCurrentlyActivated;
    List<Interactable> pendingToRemoveinteractableObjectsCurrentlyActivated;

    private ScreenAim screenAim;
    // Start is called before the first frame update
    void Start()
    {
        screenAim = GameObject.FindObjectOfType<ScreenAim>();
        interactableObjectsCurrentlyActivated = new List<Interactable>();
        pendingToRemoveinteractableObjectsCurrentlyActivated = new List<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CommandInteract() {

            var interactable = screenAim.InteractWithObjectAimed();    }


}
