using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : Singleton<TimeController>
{
    public enum Days : int { Segunda, Terca, Quarta, Quinta, Sexta };
    public int contadorDias {get; private set;} //botei o get público pro resto do código poder saber que dia é  -alu
    private float timer = 0.00f;
    public float minutes {get; private set;}    //botei o get público pro resto do código poder saber que horas são  -alu
    private float seconds;
    private int endHour = 5; //cinco minutos
    public float timeMultiplier = 1.00f; // permite acelerar o tempo no inspetor, pra ele nao fazer diferen�a tem que deixar em 1
    public Days currentDay {get; private set;}  //botei o get público pro resto do código poder saber que dia é  -alu
    public void Start()
    {
        currentDay = Days.Segunda;
    }

    public void FixedUpdate()
    { 
        if (seconds == 0)
        {
            minutes += 1;
            //Debug.Log("Mais um minuto " + minutes); Pra ver funcionando � s� ligar os Debug.Log ta desligado porque ele flooda o console
        }
        
        if (minutes == endHour)
        {
            currentDay += 1;
            contadorDias += 1; 
            //Debug.Log("Novo dia" + currentDay); Pra ver funcionando � s� ligar os Debug.Log ta desligado porque ele flooda o console
            timer = 0; //reseta o dia que � equivalente a passar pro proximo dia. 
        }

        timer += Time.deltaTime * timeMultiplier;
        seconds = Mathf.Round(timer % 60);
        //Debug.Log(seconds); Pra ver funcionando � s� ligar os Debug.Log ta desligado porque ele flooda o console
    }

    //public void CheckEventos()
    //{
    //Objetivo � criar uma fun��o que confira se exista um evento para o dia atraves do calendario. No calendario toda vez que o dia reseta � contado um dia e a partir desses dias � possivel setar eventos pra epocas especificas sem depender so do dia da semana
    //}


}




