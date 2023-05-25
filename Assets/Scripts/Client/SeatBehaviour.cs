using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatBehaviour : MonoBehaviour
{
    //colisor pro player detectar (temporario)
    [SerializeField] private Collider col;
    //posição pra ir o cliente
    [SerializeField] private Transform seat;
    public ClientBehaviour client;
    public DishData servedDish;
    
}
