using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatBehaviour : MonoBehaviour
{
    //colisor pro player detectar (temporario)
    [SerializeField] private Collider col;
    //posição pra ir o cliente
    public Transform seat;
    public ClientBehaviour client;
    private DishContainer _servedDish;
    public DishContainer ServedDish
    {
        get
        {
            return _servedDish;
        }
        set
        {
            if(client)
            {
                client.Served(value.dish);
                _servedDish = value;
                Invoke("ClearDish", value.dish.eatTime);
            }
            else
            {
                _servedDish = null;
            }
        }
    }

    public void ClearDish()
    {
        Destroy(_servedDish.gameObject);
    }
}
