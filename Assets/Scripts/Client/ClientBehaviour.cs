using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBehaviour : MonoBehaviour
{
    public CLIENT_STATES clientState;
    public enum CLIENT_STATES
    {
        READY,
        WAITING,
        EATING
    }
    [HideInInspector] public DishData dishData;
    [SerializeField] private List<DishData> dishList; //lista dos pedidos que esse cliente pode pedir
    [SerializeField] GameObject readyImg;


    private void Start() 
    {
        clientState = CLIENT_STATES.READY;

        //Escolhe um pedido aleatorio
        dishData =  dishList[Random.Range(0, dishList.Count)];
    }

    private void FixedUpdate()
    {
        if(clientState != CLIENT_STATES.READY)
        {
            readyImg.SetActive(false);
        }
    }

    public bool Served(DishData dish)
    {
        if (dish == dishData)
        {
            clientState = CLIENT_STATES.EATING;
            Invoke("FinishEating", dish.eatTime);
            return true;
        }
        else
        {
            Anger();
            return false;
        }
    }
    private void FinishEating()
    {
        
        GameController.Instance.AddMoney(dishData.price);
        Destroy(gameObject);
    }
    private void Anger()
    {
    }
}
