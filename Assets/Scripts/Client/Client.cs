using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Client : AbstractInteractable
{
    public enum STATES{
        WAITING,
        ORDER,
        ORDERED,    
        EATING
    }
    [SerializeField] private STATES state;
    [SerializeField] public DishData order;
    [SerializeField] private GameObject bubble;
    public float waitTime;


    private void Start(){
        state = STATES.WAITING;
        order = ChooseOrder(GameController.Instance.menuData.menu);
    }

    public DishData ChooseOrder(List<DishData> menu){
        Debug.Log("choosing order");
        int rng = UnityEngine.Random.Range(0, menu.Count);
        DishData order = menu[rng];
        Debug.Log(order);
        return order;
    }

    public override void Interact(){
        if(state == STATES.WAITING){
            PickUp();
        }
    }

    public void Exit(){
        Destroy(gameObject);       
    }
    private void OnDestroy()
    {
        ClientSpawn.instance.clientsBeingServed--;
    }

}
