using UnityEngine;

public class Table : AbstractInteractable
{
    private Transform dropPoint;
    private bool occupied;

    private void Start(){
        dropPoint = Helper.FindChildWithTag(gameObject, "DropPoint");
    }
    public override void Interact(){
        if(dropPoint.childCount == 0)
        {
            ManageSeat(dropPoint.GetComponent<SeatBehaviour>());
        }
    }

    private void Serve(Transform plate, SeatBehaviour seat){
        seat.ServedDish = plate.GetComponent<Dish>();
        plate.SetParent(dropPoint);
        plate.localPosition = Vector3.zero;
        plate.localRotation = Quaternion.identity;
    }
    private void ManageSeat(SeatBehaviour seat){
        //if(seat.client.clientState == ClientBehaviour.CLIENT_STATES.Ready){
        //     seat.client.clientState = ClientBehaviour.CLIENT_STATES.Waiting;
        //     GameController.Instance.GetOrder(seat.client.dishData);
        //}
        if(PlayerRefac.Instance.heldObject.GetComponent<Dish>() != null){
            Debug.Log(seat.name);
            Serve(PlayerRefac.Instance.heldObject.transform, seat);
            PlayerRefac.Instance.heldObject = null;
        }
        else if(!occupied){
            Transform client = Helper.FindChildWithTag(gameObject, "Client");           
        }
    }
}