using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouchInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InterationPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        DropContainer container = GetComponent<DropContainer>();
        if(container != null)
        {
            UIController.Instance.ShowTransferInventory(true, container);
            return true;
        }

        Debug.Log("container is null");
        return false;
    }

}
