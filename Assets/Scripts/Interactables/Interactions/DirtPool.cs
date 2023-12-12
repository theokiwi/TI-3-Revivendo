using UnityEngine;
[RequireComponent(typeof(UnsanitaryObject))]
public class DirtPool : AbstractInteractable
{
    public override void Interact()
    {
        PickUp();
    }
}
