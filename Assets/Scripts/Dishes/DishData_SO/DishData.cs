using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DishData", menuName = "Scriptable Objects/Dish Data")] //deixa criar arquivo aprertando botão direito na janela projeto
public class DishData : ScriptableObject
{
    public string dishType;
    public int price;   //quanto dinheiro dá
    public float preparationTime; //quanto tempo pra cozinhar
    public float eatTime; //quanto tempo pra terminarem de comer (se pá mudo pro client depois)
    public GameObject inPlateObj;    //Prefab pra ser instanciado como prato (SÓ VISUAL)
    public Sprite interfaceIcon;    //ícone pra instanciar na interface
}
