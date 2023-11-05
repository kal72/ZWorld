using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InterationPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        UIController.Instance.OpenCrafting(true);
        return true;
    }
}
