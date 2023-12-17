using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//se eu apaguei coisas que eram supostas de em algum universo paralelo serem usados pra alguma coisa, voces que lutem pq no momento tava servindo pra nada -alu
public class TimeController : Singleton<TimeController>
{
    public int contadorDias {get; private set;} //botei o get público pro resto do código poder saber que dia é  -alu
    private float timer = 0.00f;
    public float minutes {get; private set;}    //botei o get público pro resto do código poder saber que horas são  -alu
    private int endHour = 2; //dois minutos                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
    public float timeMultiplier = 20f; // permite acelerar o tempo no inspetor, pra ele nao fazer diferen�a tem que deixar em 2
    public int dayCount;
    public int seasonsPassed = 0;
    public TMP_Text dayCounter;
    public TMP_Text timerText;
    public int timeLeft;


    public void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeWeek();
        }
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
            EndDay();
        }

        timer += Time.fixedDeltaTime * timeMultiplier;
        //UnityEngine.Debug.Log(timer); //Pra ver funcionando � s� ligar os Debug.Log ta desligado porque ele flooda o console
    }
    public void EndDay()
    {
        minutes = 0;
        contadorDias++;
        if (contadorDias >= 3)
        {
            contadorDias = 0;
            ChangeWeek(); //informa que acabou a semana
        }
        //Debug.Log("Novo dia " + currentDay);// Pra ver funcionando � s� ligar os Debug.Log ta desligado porque ele flooda o console
        timer = 0; //reseta o dia que � equivalente a passar pro proximo dia. 
        if(Events.Instance.ReturnSeason() == Events.Seasons.FestaJulina)
        {
            MiniGamesManager.instance.StartMiniGame("Bonfire");
        }
        else if(Events.Instance.ReturnSeason() == Events.Seasons.Halloween)
        {
            MiniGamesManager.instance.StartMiniGame("Halloween");
        }
        else if (Events.Instance.ReturnSeason() == Events.Seasons.Valentines)
        {
            MiniGamesManager.instance.StartMiniGame("Valentine");
        }
        else
        {
            GameController.Instance.GameOver();
        }
    }

    private void UpdateUI(){
        timeLeft = Mathf.RoundToInt((60 * endHour) - (timer + minutes * 60));
        timerText.text = $"time left : {timeLeft}";
        dayCounter.text = $"Day : {contadorDias}";
    }

    public void ChangeWeek(){ 
        Debug.Log("ChangedWeek");
        if (seasonsPassed > 5){ //serve pra contar em qual estação a gente está
            seasonsPassed = 0;
        }
        seasonsPassed++;
        Events.Instance.ChangeSeason();
    }

    

    //public void CheckEventos()
    //{
    //Objetivo � criar uma fun��o que confira se exista um evento para o dia atraves do calendario. No calendario toda vez que o dia reseta � contado um dia e a partir desses dias � possivel setar eventos pra epocas especificas sem depender so do dia da semana
    //}


}




