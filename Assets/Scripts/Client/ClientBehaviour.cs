using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBehaviour : MonoBehaviour
{
    [HideInInspector] public DishData dishData;
    [SerializeField] GameObject readyImg;
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
        Invoke("Angrify", 25);
    }

    private void FixedUpdate()
    {
        if(clientState != CLIENT_STATES.Ready)
        {
            readyImg.SetActive(false);
        }
    }

    public bool Served(DishData dish)
    {
        if (dish == dishData)
        {
            clientState = CLIENT_STATES.Eating;
            Invoke("FinishEating", dish.eatTime);
            return true;
        }
        else
        {
            Angrify();
            return false;
        }
    }
    private void FinishEating()
    {
        
        GameController.Instance.AddMoney(dishData.price);
        Destroy(gameObject);
    }
    private void Angrify()
    {
        angryIcon.SetActive(true);
        GameController.Instance.LoseClient();
        Destroy(gameObject, 2);
    }
}
