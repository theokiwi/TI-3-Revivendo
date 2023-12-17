using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Client : AbstractInteractable
{
    //public Animator animator;
    public enum STATES{
        WAITING,
        SIT
    }
    [SerializeField] private STATES state;
    [SerializeField] public DishData order;
    [SerializeField] private Bubble bubble;
    [SerializeField] private Animator animator;
    public float waitTime;


    private void Start(){
        //Debug.Log("hiiii");
        state = STATES.WAITING;
        order = ChooseOrder(GameController.Instance.menuData.menu);
        bubble.Refresh(waitTime, order.interfaceIcon);
        bubble.Wake();
        bubble.Complete += OnFailure;
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
            animator.SetBool("Waiting_On", true);
            PickUp();
        }
    }

    public void Sit(){
        state = STATES.SIT;
        bubble.Hide(true);
    }

    public void Eat()
    {
        animator.SetBool("Eat_on", true);
    }

    public void Exit(){
        Destroy(gameObject);
    }

    public void OnFailure(){
        GameController.Instance.AddPoints(-200);
        Exit();
    }

    private void OnDestroy()
    {
        //ClientSpawn.instance.clientsBeingServed--;
    }

}
