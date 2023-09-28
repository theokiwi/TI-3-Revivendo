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
    private Dish _servedDish;
    public Dish ServedDish
    {
        get
        {
            return _servedDish;
        }
        set
        {
            if(client)
            {
                if(client.Served(value.dish))
                {
                    _servedDish = value;
                    Invoke("ClearDish", value.dish.eatTime);
                }else
                {
                    Invoke("ClearDish", 2);
                }
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
