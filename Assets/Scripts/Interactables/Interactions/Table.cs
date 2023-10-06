using UnityEngine;

public class Table : AbstractInteractable
{
    private Transform dropPoint;
    private SeatBehaviour seat;
    private bool occupied;

    private void Start(){
        dropPoint = Helper.FindChildWithTag(gameObject, "DropPoint");
    }
    public override void Interact(){
        if(dropPoint.childCount == 0)
        {
            seat = dropPoint.GetComponent<SeatBehaviour>();
            ManageSeat();
        }
    }

    public void Serve(Transform plate){
        seat.ServedDish = plate.GetComponent<Dish>();
        plate.SetParent(dropPoint);
        plate.localPosition = Vector3.zero;
        plate.localRotation = Quaternion.identity;
    }
    private void ManageSeat(){
        //if(seat.client.clientState == ClientBehaviour.CLIENT_STATES.Ready){
        //     seat.client.clientState = ClientBehaviour.CLIENT_STATES.Waiting;
        //     GameController.Instance.GetOrder(seat.client.dishData);
        //}
        if(PlayerRefac.Instance.heldObject.GetComponent<Dish>() != null){
            Debug.Log(seat.name);
            Serve(PlayerRefac.Instance.heldObject.transform);
            PlayerRefac.Instance.heldObject = null;
        }
        else if(occupied){
            Transform client = Helper.FindChildWithTag(gameObject, "Client");        
        }
    }
}