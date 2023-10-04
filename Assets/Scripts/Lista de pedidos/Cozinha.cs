using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cozinha : AbstractInteractable
{

    // Locka o prato escolhido e inicia o preparo.
    public override void Interact()
    {
        if(GameController.Instance.orders.Count > 0) GameController.Instance.StartCooking(GameController.Instance.orders.Dequeue());
    }
}
