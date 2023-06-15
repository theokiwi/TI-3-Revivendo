using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBehaviour : MonoBehaviour
{
    [HideInInspector] public DishData dishData;
    public enum CLIENT_STATES
    {
        Ready,
        Waiting,
        Eating
    }

    public CLIENT_STATES clientState;

    [SerializeField] private GameObject angryIcon;

    [SerializeField] private List<DishData> dishList; //lista dos pedidos que esse cliente pode pedir

    private void Start() 
    {
        clientState = CLIENT_STATES.Ready;

        //Escolhe um pedido aleatorio
        dishData =  dishList[Random.Range(0, dishList.Count)];
    }

    public void Served(DishData dish)
    {
        if (dish == dishData)
        {
            clientState = CLIENT_STATES.Eating;
            Invoke("FinishEating", dish.eatTime);
        }
        else
        {
            angryIcon.SetActive(true);
            Destroy(gameObject, 2);
        }
    }
    private void FinishEating()
    {
        GameController.Instance.AddMoney(dishData.price);
        Destroy(gameObject);
    }
}
