using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : Singleton<TimeController>
{
    public enum Days : int {Dia1, Dia2, Dia3 };
    public int contadorDias {get; private set;} //botei o get público pro resto do código poder saber que dia é  -alu
    private float timer = 0.00f;
    public float minutes {get; private set;}    //botei o get público pro resto do código poder saber que horas são  -alu
    private float seconds;
    private int endHour = 2; //dois minutos                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
    public float timeMultiplier = 1f; // permite acelerar o tempo no inspetor, pra ele nao fazer diferen�a tem que deixar em 2
    public Days currentDay {get; private set;}  //botei o get público pro resto do código poder saber que dia é  -alu
    public int dayCount;
    public int seasonsPassed;
    public TMP_Text dayCounter;
    public TMP_Text timerText;


    public void Start()
    {
        currentDay = Days.Dia1;
    }

    public void FixedUpdate()
    { 
        UpdateUI();

        if (timer >= 60)
        {
            timer = 0;
            minutes++;
            //UnityEngine.Debug.Log("Mais um minuto " + minutes); // Pra ver funcionando � s� ligar os Debug.Log ta desligado porque ele flooda o console
        }
        
        if (minutes == endHour)
        {
            minutes = 0;
            currentDay++;
            if((int)currentDay > 2){
                currentDay = 0;
            }
            contadorDias++; 
            UnityEngine.Debug.Log(contadorDias);
            if(contadorDias == 3){
                contadorDias = 0;
                ChangeWeek(); //informa que acabou a semana
            }
            UnityEngine.Debug.Log("Novo dia " + currentDay);// Pra ver funcionando � s� ligar os Debug.Log ta desligado porque ele flooda o console
            timer = 0; //reseta o dia que � equivalente a passar pro proximo dia. 
        }

        timer += Time.fixedDeltaTime * timeMultiplier;
        //UnityEngine.Debug.Log(timer); //Pra ver funcionando � s� ligar os Debug.Log ta desligado porque ele flooda o console
    }

    private void UpdateUI(){
        int timeLeft = Mathf.RoundToInt((60 * endHour) - (timer + minutes * 60));
        timerText.text = $"time left : {timeLeft}";
        dayCounter.text = $"Day : {currentDay}";
    }

    public void ChangeWeek(){ 
        UnityEngine.Debug.Log("ChangedWeek");
        if (seasonsPassed > 5){ //serve pra contar em qual estação a gente está
            seasonsPassed = 0;
        }
        seasonsPassed++; 
        contadorDias = 0;
        Events.Instance.ChangeSeason(seasonsPassed);
    }

    

    //public void CheckEventos()
    //{
    //Objetivo � criar uma fun��o que confira se exista um evento para o dia atraves do calendario. No calendario toda vez que o dia reseta � contado um dia e a partir desses dias � possivel setar eventos pra epocas especificas sem depender so do dia da semana
    //}


}




