using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cozinha : UIScreen
{
    private List<UIElement> dropdowns;


    // Encontra os dorpdowns do menu da cozinha
    protected override void OnPopup()
    {
        foreach (var element in elements)
        {
            if (element.CompareTag("Dropdowns"))
                dropdowns.Add(element);
        }

        RefreshList();
    }

    // Mantem a lista ordenada e atualizada enquanto o player interage com o menu da cozinha.
    public void RefreshList()
    {
        foreach (UIElement dropdown in dropdowns)
        {
            dropdown.UpdateUI();
        }
    }

    // Locka o prato escolhido e inicia o preparo.
    public static void ConfirmOrder(int index)
    {
        DishData dish = GameController.Instance.orders[index];

        GameController.Instance.StartCooking(dish);

        GameController.Instance.orders.RemoveAt(index);
    }
}
