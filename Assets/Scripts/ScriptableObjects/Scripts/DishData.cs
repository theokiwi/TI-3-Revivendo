using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dish Data", menuName = "Scriptable Objects/Dish Data")] //deixa criar arquivo aprertando botão direito na janela projeto
public class DishData : ScriptableObject
{
    public int price;   //quanto dinheiro dá
    public int preparationTime; //quanto tempo pra cozinhar
    public GameObject inPlateObj;    //Prefab pra ser instanciado como prato (SÓ VISUAL)
    public Sprite interfaceIcon;    //ícone pra instanciar na interface
}
