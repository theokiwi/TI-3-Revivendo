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
    [SerializeField] private Seat[] seats = new Seat[2];
    [SerializeField] private DishData tableOrder;
    private Transform dropPoint;
    public bool isDirty;
    [SerializeField] private Bubble tableBubble;
    [SerializeField] private UnsanitaryObject tableDirt;


    private void Start(){
        occupants = 0;
        state = STATES.EMPTY;
        seats = GetComponentsInChildren<Seat>();
        dropPoint = Helper.FindChildWithTag(gameObject, "DropPoint");
        //tableBubble.Hide();
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
                if(isDirty){
                    MiniGamesManager.instance.StartMiniGame("Table", gameObject.GetComponent<Table>());
                    GetComponentInChildren<UnsanitaryObject>().Clean();
                }
                else if(PlayerRefac.Instance.heldObject == null){}
                else if (holding.GetType() == typeof(Client)){
                    SeatClient(holding.GetComponent<Client>());
                    state = STATES.READY;
                    return;
                }
            break;
            case STATES.READY:
                if(holding == null){
                    if (tableOrder != null){ 
                    Order(tableOrder);
                    state = STATES.ORDERED;
                    Debug.Log("Ordered");
                    return;
                    }
                }
                else if (holding.GetType() == typeof(Client)) SeatClient(holding.GetComponent<Client>());
                break;
            case STATES.ORDERED:
                /*if(tableBubble.sleepy){
                    tableBubble.Refresh(15, tableOrder.interfaceIcon);
                    tableBubble.Wake();
                }*/
                if(PlayerRefac.Instance.heldObject == null) { }
                else if(holding.GetType() == typeof(Client)) SeatClient(holding.GetComponent<Client>());
                else if(holding.GetType() == typeof(Dish)){
                    ServeDish(holding, tableOrder);
                    //tableBubble.Sleep();
                }
                break;
            case STATES.IN_USE:
                //Has no function so far
            break;
        }
    }

    private void EmptySeats(){
        foreach(Seat data in seats){
            if(data.occupied){
                data.occupied = false;
                data.clientSeated.Exit();
            }
        }
        state = STATES.EMPTY;
        occupants = 0;
        if(UnityEngine.Random.Range(0, 10) < 3)
        { 
            isDirty = true;
            Instantiate(tableDirt,transform);
        }
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
            client.GetComponent<Collider>().enabled = false;
            seats[occupants].clientSeated = client;
            seats[occupants].occupied = true;
            occupants++;

            Vector3 lookPos = new Vector3(transform.position.x, client.transform.position.y, transform.position.z);
            client.transform.LookAt(lookPos);
        }
        else Debug.Log("Esta mesa ja esta cheia");
    }

    private DishData ChooseOrder(Client client) {
        //tableBubble.gameObject.SetActive(true);
        return client.order;
    }

    private void Order(DishData order) {GameController.Instance.GetOrder(order);}
    
    private void ServeDish(AbstractInteractable plate, DishData ordered){
        plate.ToPosition(dropPoint);
        DishData served = plate.GetComponent<Dish>().dish;
        if(served == ordered) GameController.Instance.SuccessfullDelivery(tableOrder, occupants);
        else GameController.Instance.FailledDelivery(occupants, ordered);
        Destroy(plate.gameObject);
        EmptySeats();
    }
}