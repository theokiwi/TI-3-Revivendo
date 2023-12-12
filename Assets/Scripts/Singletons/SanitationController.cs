using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitationController : Singleton<SanitationController>
{
    //valor real de saneamento
    private float sanitationValue;
    //multiplicador do valor de saneamento (para propositos de balanceamento apenas)
    [SerializeField] private float satisfactionMultiplier;

    //valor levado em conta pela satisfacao dos clientes
    public float SatisfactionModifier{ get{ return sanitationValue * satisfactionMultiplier; }}

    //lista de objetos sujos
    private List<UnsanitaryObject> unsanitaryObjects;

    //adiciona e remove elementos
    public void AddUnsanitaryObject(UnsanitaryObject unsanitaryObject) 
    {
        unsanitaryObjects.Add(unsanitaryObject);
        sanitationValue -= unsanitaryObject.dirtiness;
    }
    public void RemoveUnsanitaryObject(UnsanitaryObject unsanitaryObject) 
    {
        unsanitaryObjects.Remove(unsanitaryObject);
        sanitationValue += unsanitaryObject.dirtiness;
    }

    //valores iniciais para um novo jogo
    public void ResetValues()
    {
        sanitationValue = 50f;
    }

    //mudanca de valores entre um dia e outro
    public void DayChange()
    {
        
    }
}
