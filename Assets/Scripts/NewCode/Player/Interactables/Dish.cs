using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Dish : _Interactable
{
    public DishData dish;

    public override void Interact(){
        PickUp();
        PlayerRefac.Instance.rightClick = new Throw();
        Debug.Log(PlayerRefac.Instance.interaction.GetType());
    }

    public void OnCollisionEnter(Collision other){
        
    }
}
