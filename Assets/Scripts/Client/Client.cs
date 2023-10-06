using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Client : AbstractInteractable
{
    [SerializeField]private ClientSO data;
    public enum STATES{
        READY,
        WAITING,
        EATING
    }
    private STATES state;
    private DishData ordered;

    public override void Interact(){
        PickUp();
    }

    private void OnCollisionEnter(Collision other){
        if(other.collider.CompareTag("Table")){
            
        }
    }

}
