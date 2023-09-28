using UnityEngine;

public abstract class AbstractInteractable : MonoBehaviour
{
    public abstract void Interact();
    protected virtual void PickUp(){
        if(PlayerRefac.Instance.heldObject != null){
            Transform dropPos = transform;
            GameObject drop = PlayerRefac.Instance.heldObject.gameObject;
            drop.transform.SetParent(null);
            drop.transform.position = dropPos.position;
            drop.transform.rotation = Quaternion.identity;
            PlayerRefac.Instance.heldObject = null;
        }
        Transform parent = PlayerRefac.Instance.holdPos;

        transform.SetParent(parent);
        transform.position = parent.position;
        transform.rotation = Quaternion.identity;
        PlayerRefac.Instance.heldObject = gameObject;
    } 
}
