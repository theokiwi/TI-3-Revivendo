using System;
using UnityEngine;

public class KitchenChef : AbstractInteractable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Bubble[] cookingIndicators;
    private int dishesCooking;
    private void Start()
    {
        foreach(Bubble indicator in cookingIndicators)
        {
            indicator.Complete += FinishedCooking;
        }
    }

    private void FinishedCooking()
    {
        dishesCooking--;
        if(dishesCooking <= 0)
        {
            animator.SetBool("Cooking", false);
        }
    }

    public override void Interact(){
        try{
            GameController.Instance.StartCooking(GameController.Instance.orders.Dequeue());
            dishesCooking++;
            animator.SetBool("Cooking", true);
        }catch{}
    }
}
