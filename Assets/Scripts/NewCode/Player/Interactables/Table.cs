using UnityEngine;

public class Table : _Interactable
{
    public Transform dropPoint;

    private void Start(){
        dropPoint = Helper.FindChildWithTag(gameObject, "DropPoint");
    }
    public override void Interact(){
        if(dropPoint.childCount == 0)
        {
            ManageSeat(dropPoint.GetComponent<SeatBehaviour>());
        }
    }

    private void ManageSeat(SeatBehaviour seat){
    //    if (seat.client.clientState == ClientBehaviour.CLIENT_STATES.Ready){
    //        seat.client.clientState = ClientBehaviour.CLIENT_STATES.Waiting;
    //        GameController.Instance.GetOrder(seat.client.dishData);
    //    }
         if(PlayerRefac.Instance.heldObject.GetComponent<Dish>() != null){
            Debug.Log(seat.name);
            seat.ServedDish = PlayerRefac.Instance.heldObject.GetComponent<Dish>();
            PlayerRefac.Instance.heldObject.transform.SetParent(dropPoint);
            PlayerRefac.Instance.heldObject.transform.localPosition = Vector3.zero;
            PlayerRefac.Instance.heldObject.transform.localRotation = Quaternion.identity;
            PlayerRefac.Instance.heldObject = null;
        }
    }
}