using UnityEngine;

public class Table : AbstractInteractable
{
    private enum STATES{
        READY,
        ORDERED,
        IN_USE,
        FAILED
    }
    private STATES state;
    private bool inUse;
    private Transform dropPoint;
    [SerializeField] Seat[] seats;
    DishData tableOrder;


    private void Start(){
        state = STATES.READY;
        seats = GetComponentsInChildren<Seat>();
        dropPoint = Helper.FindChildWithTag(gameObject, "DropPoint");
    }
    

    public override void Interact()
    {
        AbstractInteractable holding = PlayerRefac.Instance.heldObject.GetComponent<AbstractInteractable>();
        switch(state){
            case STATES.READY:
                if (holding.GetType() == typeof(Client)) SeatClient(holding.GetComponent<Client>());
                else if (holding != null) holding.ToPosition(dropPoint);
                else if (tableOrder != null) Order(tableOrder);
                break;
            case STATES.ORDERED:
                if(holding.GetType() == typeof(Client)) SeatClient(holding.GetComponent<Client>());
                if(holding.GetType() == typeof(Dish)){
                    if(holding.GetComponent<Dish>().dish == tableOrder) ServeDish(holding);
                    else OnFailure();
                }
                break;
            case STATES.IN_USE:
                OnSucces(holding);
            break;
        }
    }

    private void OnSucces(AbstractInteractable plate){
        GameController.Instance.SuccessfullDelivery(tableOrder, TableOccupants());
        Destroy(plate);
    }

    private void OnFailure(){
        GameController.Instance.FailledDelivery(TableOccupants());
        foreach(Seat data in seats){
            data.clientSeated.Exit();
        }
    }

    private int TableOccupants(){
        int seated = 0;
        foreach (Seat data in seats){
            if (data.occupied) seated++;
        }
        return seated;
    }

    private void EmptySeats(){
        foreach(Seat data in seats){
            data.occupied = false;
            data.clientSeated = null;
        }
    }

    private void SeatClient(Client client){
        if(TableOccupants() < 2){
            if(TableOccupants() == 0){
                tableOrder = ChooseOrder(client);
            }
            else if(client.order != tableOrder){
                //Tocar sound effect de erro
                return;
            }
            client.ToPosition(seats[TableOccupants()].seatPos);

            Vector3 lookPos = new Vector3(transform.position.x, client.transform.position.y, transform.position.z);
            client.transform.LookAt(lookPos);

            seats[TableOccupants()].occupied = true;
        }
    }

    private DishData ChooseOrder(Client client){
        return client.order;
    }

    private void Order(DishData order){
        GameController.Instance.GetOrder(order);
        state = STATES.ORDERED;
    }
    
    private void ServeDish(AbstractInteractable plate){
        plate.ToPosition(dropPoint);
        state = STATES.IN_USE;
    }
}