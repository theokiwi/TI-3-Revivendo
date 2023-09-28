using UnityEngine;

public class TrashCan : _Interactable
{
    public override void Interact(){
        if(PlayerRefac.Instance.heldObject != null){
            GameController.Instance.GetOrder(PlayerRefac.Instance.heldObject.GetComponent<Dish>().dish);
            Destroy(PlayerRefac.Instance.heldObject.gameObject);
        }
    }
}
