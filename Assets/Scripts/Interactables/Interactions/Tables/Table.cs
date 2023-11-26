using System;
using UnityEngine;

public class Table : AbstractInteractable
{
    private enum STATES{
        EMPTY,
        READY,
        ORDERED,
        IN_USE
    }
    [SerializeField] private int occupants;
    [SerializeField] private STATES state;
    [SerializeField] private Seat[] seats;
    [SerializeField] private DishData tableOrder;
    private Transform dropPoint;
    private bool isDirty;
    private ProgressBar tableBubble;


    private void Start(){
        occupants = 0;
        state = STATES.READY;
        seats = GetComponentsInChildren<Seat>();
        dropPoint = Helper.FindChildWithTag(gameObject, "DropPoint");
    }
    
    private void OnCollisionEnter(Collision other){
        if(other.transform.CompareTag("Client")){
            SeatClient(other.transform.GetComponent<Client>());
        }
    }

    public override void Interact()
    {
        AbstractInteractable holding;
        try {holding = PlayerRefac.Instance.heldObject.GetComponent<AbstractInteractable>();}
        catch {holding = null;}

        switch(state){
            case STATES.EMPTY:
                //if(isDirty){
                //    //Mini-game de limpar mesa
                //}
                if (holding.GetType() == typeof(Client)){
                    SeatClient(holding.GetComponent<Client>());
                    state = STATES.READY;
                }
            break;
            case STATES.READY:
                if(holding == null){
                    if (tableOrder != null){ 
                    Order(tableOrder);
                    state = STATES.ORDERED;
                    Debug.Log("Ordered");
                    }
                }
                else if (holding.GetType() == typeof(Client)) SeatClient(holding.GetComponent<Client>());
                break;
            case STATES.ORDERED:
                if(holding.GetType() == typeof(Client)) SeatClient(holding.GetComponent<Client>());
                else if(holding.GetType() == typeof(Dish)){
                    state = STATES.IN_USE;
                    ServeDish(holding, tableOrder);
                }
                break;
            case STATES.IN_USE:
                //Has no function so far
            break;
        }
    }

    private void EmptySeats(){
        foreach(Seat data in seats){
            data.occupied = false;
            data.clientSeated.Exit();
            data.clientSeated = null;
        }
        state = STATES.EMPTY;
        occupants = 0;
        if(UnityEngine.Random.Range(0, 10) < 2.5) isDirty = true;
    }

    private void SeatClient(Client client){
        if(occupants < 2){
            if(occupants == 0){
                tableOrder = ChooseOrder(client);
                state = STATES.READY;
            }
            else if(client.order != tableOrder){
                Debug.Log("O pedido deste cliente difere do pedido da mesa!");
                return;
                //Tocar sound effect de erro
            }
            client.ToPosition(seats[occupants].seatPos);
            seats[occupants].clientSeated = client;
            seats[occupants].occupied = true;
            occupants++;

            Vector3 lookPos = new Vector3(transform.position.x, client.transform.position.y, transform.position.z);
            client.transform.LookAt(lookPos);
        }
        else Debug.Log("Esta mesa ja esta cheia");
    }

    private DishData ChooseOrder(Client client) {return client.order;}

    private void Order(DishData order) {GameController.Instance.GetOrder(order);}
    
    private void ServeDish(AbstractInteractable plate, DishData ordered){
        plate.ToPosition(dropPoint);
        DishData served = plate.GetComponent<Dish>().dish;
        if(served == ordered) GameController.Instance.SuccessfullDelivery(tableOrder, occupants);
        else GameController.Instance.FailledDelivery(occupants);
        EmptySeats();
    }
}