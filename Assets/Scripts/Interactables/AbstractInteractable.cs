using UnityEngine;

public abstract class AbstractInteractable : MonoBehaviour
{
    public abstract void Interact();
    protected virtual void PickUp(){
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.useGravity = false;
        //rb.constraints = RigidbodyConstraints.FreezeAll;


        if(PlayerRefac.Instance.heldObject != null){
            Transform dropPos = transform;
            GameObject drop = PlayerRefac.Instance.heldObject.gameObject;
            drop.transform.SetParent(null);
            drop.transform.position = dropPos.position;
            drop.transform.rotation = Quaternion.identity;
            PlayerRefac.Instance.heldObject = null;
        }
        Transform parent = PlayerRefac.Instance.holdPos;
        ToPosition(parent);
        //transform.SetParent(parent);
        //transform.position = parent.position;
        //transform.rotation = Quaternion.identity;
        PlayerRefac.Instance.heldObject = gameObject;
        PlayerRefac.Instance.rightClick = new Throw();
    }

    public virtual void ToPosition(Transform pos){
        if(gameObject == PlayerRefac.Instance.heldObject) PlayerRefac.Instance.heldObject = null;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.SetParent(pos);
        transform.position = pos.position;
        transform.rotation = Quaternion.identity;
    }
}
