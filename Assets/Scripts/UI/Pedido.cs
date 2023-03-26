using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedido : MonoBehaviour
{
    public float timer;
    public Pedido seg;
    public float reward;

    public Pedido(Pedido seg = null)
    {
        timer = 10f;
        this.seg = seg;
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }

    public bool IsReady()   //quando for fazer o "inventário" do garçom, implementa aqui a condicional de pegar o prato.
    {
        if (timer <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
