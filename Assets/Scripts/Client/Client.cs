using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Client : AbstractInteractable
{
    public enum STATES{
        ORDER,
        WAITING,
        EATING
    }
    [SerializeField] private STATES state;
    [SerializeField] public DishData order;
    [SerializeField] private GameObject bubble;


    private void Start(){
        state = STATES.ORDER;
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
        if(state == STATES.ORDER){
            PickUp();
        }
    }

    public void Exit(){
        
    }

}
