using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBehaviour : MonoBehaviour
{
    [SerializeField] private float playerServingRange;

    public enum CLIENT_STATES
    {
        Ready,
        Waiting
    }
    public CLIENT_STATES clientState;

    public Pedido pedido;

    private void Update()
    {
        if (PlayerCheck())
        {
            if (clientState == CLIENT_STATES.Ready)
            {
                SendPedido();
                clientState = CLIENT_STATES.Waiting;
            }

            if (clientState == CLIENT_STATES.Waiting && pedido.IsReady())
            {
                FinishEating();
            }
        }
    }

    private bool PlayerCheck()
    {
        return Vector3.Distance(transform.position, FindFirstObjectByType<Player>().transform.position) <= playerServingRange;
    }

    private void SendPedido()
    {
        ListaController.AddPedido(this);
    }
    private void FinishEating()
    {
        //gamemanager dinheiros adicionar blá blá
        GameController.instance.AddMoney(pedido.reward);
        Destroy(gameObject);
    }
}
