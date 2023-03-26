using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public enum Days : int { Segunda, Terca, Quarta, Quinta, Sexta };
    private int contadorDias = 1;
    private float timer = 0.00f;
    private float minutes = -1;
    private float seconds;
    private int endHour = 5; //cinco minutos
    public float timeMultiplier = 1.00f; // permite acelerar o tempo no inspetor, pra ele nao fazer diferença tem que deixar em 1
    Days currentDay;
    public void Start()
    {
        currentDay = Days.Segunda;
    }

    public void FixedUpdate()
    { 
        if (seconds == 0)
        {
            minutes += 1;
            //Debug.Log("Mais um minuto " + minutes); Pra ver funcionando é só ligar os Debug.Log ta desligado porque ele flooda o console
        }
        
        if (minutes == endHour)
        {
            currentDay += 1;
            contadorDias += 1; 
            //Debug.Log("Novo dia" + currentDay); Pra ver funcionando é só ligar os Debug.Log ta desligado porque ele flooda o console
            timer = 0; //reseta o dia que é equivalente a passar pro proximo dia. 
        }

        timer += Time.deltaTime * timeMultiplier;
        seconds = Mathf.Round(timer % 60);
        //Debug.Log(seconds); Pra ver funcionando é só ligar os Debug.Log ta desligado porque ele flooda o console
    }

    //public void CheckEventos()
    //{
    //Objetivo é criar uma função que confira se exista um evento para o dia atraves do calendario. No calendario toda vez que o dia reseta é contado um dia e a partir desses dias é possivel setar eventos pra epocas especificas sem depender so do dia da semana
    //}


}




