using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float interacPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private IInteractable interactable; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactPoint.position, interacPointRadius, colliders, interactableMask);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (!interactionPromptUI.IsDisplayed) interactionPromptUI.Setup(interactable.InterationPrompt);

                if (Input.GetKeyUp(KeyCode.F)) interactable.Interact(this);
            }
        }
        else
        {
            if (interactable != null) interactable = null;
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPoint.position, interacPointRadius);
    }
}
