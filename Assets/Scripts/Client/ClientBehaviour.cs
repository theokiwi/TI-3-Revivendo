using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBehaviour : MonoBehaviour
{
    public DishData dishData;
    public enum CLIENT_STATES
    {
        Ready,
        Waiting,
        Eating
    }
    public CLIENT_STATES clientState;

    private void Start() 
    {
        clientState = CLIENT_STATES.Ready;
    }

    private void SendPedido()
    {
        GameController.Instance.GetOrder(dishData);
    }
    private void FinishEating()
    {
        GameController.Instance.AddMoney(dishData.price);
        Destroy(gameObject);
    }
}
