using System;
using UnityEngine;

public class KitchenChef : AbstractInteractable
{
    [SerializeField] public Animator animator;
    [SerializeField] private Bubble[] cookingIndicators;
    public int dishesCooking;
    private void Start()
    {
        foreach(Bubble indicator in cookingIndicators)
        {
            indicator.Complete += FinishedCooking;
        }
    }

    public void FinishedCooking()
    {
        dishesCooking--;
        if(dishesCooking <= 0)
        {
            animator.SetBool("Cooking", false);
        }
    }

    public override void Interact(){
        if (GameController.Instance.orders.Count > 0)
        {
            try
            {
                MiniGamesManager.instance.StartMiniGame("Counter");
                //GameController.Instance.StartCooking(GameController.Instance.orders.Dequeue());
                //dishesCooking++;
                //animator.SetBool("Cooking", true);
            }
            catch { }
        }
    }
}
