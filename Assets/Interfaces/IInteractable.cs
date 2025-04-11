using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    InteractionManager InteractableManager { get; set; }
    public void ActiveInteractable();
    public void Interact();

}