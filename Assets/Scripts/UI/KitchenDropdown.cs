using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitchenDropdown : UIElement
{
    private TMP_Dropdown menu;
    //lista de pedidos a ser mostrada
    private List<DishData> orders;
    //botao que recebe a funcao da cozinha
    [SerializeField] private ButtonFunction button;

    public override void UpdateUI()
    {
        //pega a lista de pedidos e atribui cada um a uma opcao de dropdown
        orders = GameController.Instance.orders;
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        options.Clear();
        menu.ClearOptions();

        foreach (DishData data in orders)
        {
            options.Add(new TMP_Dropdown.OptionData(data.dishType, data.interfaceIcon));
        }
        menu.AddOptions(options);

        //atribui ao onclick do botao a funcao de confirmar pedido
        button.ButtonAction = () => { Cozinha.ConfirmOrder(menu.value); };
    }
}
