using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cozinha : MonoBehaviour
{
    private GameObject[] dropdowns;
    private TMP_Dropdown menu;
    private List<DishData> orders;


    // Encontra os dorpdowns do menu da cozinha
    private void OnEnable()
    {
        dropdowns = GameObject.FindGameObjectsWithTag("Dropdown");
        menu = gameObject.GetComponent<TMP_Dropdown>();

        RefreshList();
    }

    // Mantem a lista ordenada e atualizada enquanto o player interage com o menu da cozinha.
    public void RefreshList()
    {
        orders = GameController.Instance.orders;
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        options.Clear();
        menu.ClearOptions();

        foreach(DishData data in orders)
        {
            options.Add( new TMP_Dropdown.OptionData(data.dishType, data.interfaceIcon));
        }

        menu.AddOptions(options);
    }

    // Locka o prato escolhido e inicia o preparo.
    public void ConfirmOrder()
    {
        int index = menu.value;
        DishData dish = orders[index];

        GameController.Instance.StartCooking(dish);

        orders.RemoveAt(index);
        foreach(GameObject data in dropdowns)
        {
            Cozinha cozinha = data.GetComponent<Cozinha>();
            cozinha.RefreshList();
        }
    }
}
