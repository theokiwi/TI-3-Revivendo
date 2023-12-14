using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class GameController : Singleton<GameController>
{
    public MenuSO menuData;
    public Queue<DishData> orders;
    public Queue<GameObject> plates;
    [SerializeField] TMP_Text[]  moneyText;
    [SerializeField] TMP_Text pointsText;
    [SerializeField] TMP_Text numberOfOrders;
    [SerializeField] GameObject endScreen;

    private bool _paused = true;
    public bool _IsPaused;

    public int money;
    public int points;

    public int lostClients;

    [SerializeField] Dispenser[] dispensers;
    [SerializeField] KitchenChef chef;

    private void Start()
    {
        //inicialização movida para o método StartGame()
        StartGame();
    }

    //reinicia todos os valores para o início de um novo jogo / carrega os valores de um save
    private void StartGame() 
    {
        money = 0;
        points = 0;
        UpdateMoney();
        numberOfOrders.text = $"{0}";

        PauseGame(false);

        SanitationController.Instance.ResetValues();

        StartDay();
    }

    //reinicia todos os valores referentes a performance no dia
    private void StartDay() 
    {
        lostClients = 0;

        plates = new Queue<GameObject>();
        orders = new Queue<DishData>();
        SanitationController.Instance.DayChange();
    }

    public void FixedUpdate()
    {
        ServePlate();
    }

    //calcula a pontuacao (TODO: refazer quando tiver um sistema de satisfacao pros clientes)
    public float CalculateScore()
    {
        int finalScore = points + (2 * money) + (2 * TimeController.Instance.timeLeft);
        return finalScore;
    }

    public void SuccessfullDelivery(DishData plate, int numClients){
        Debug.Log("Success!");
        int multiplier = 1;
        if (numClients == 2) multiplier = 3;
        AddMoney(plate.price * multiplier);
        AddPoints (100 * multiplier);
    }

    public void FailledDelivery(int numClients){
        Debug.Log("Failure");
        lostClients += numClients;
        AddPoints(- 125);
    }

    //Adiciona valor inputado ao dinheiro do jogador, e atualiza o contador na tela.
    public void AddMoney (int value)
    {
        money += value;
        UpdateMoney();
    }
    //reduz valor do dinheiro, e atualiza o contador na tela. Retorna false se nao tiver dinheiro suficiente
    public bool RemoveMoney (int value)
    {
        if(money < value)
            return false;

        money -= value;
        UpdateMoney();
        return true;
    }

    public void AddPoints(int points){
        this.points += points;
        pointsText.text = $"score: {this.points}";
    }

    //Adiciona 1 a contagem de clientes perdidos (TODO: trocar isso por um sistema de satisfacao pro cliente) 
    public void LoseClient()
    {
        lostClients--;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        TimeController.Instance.timeMultiplier = 0;
        End.Instance.EndUpdate();
        endScreen.gameObject.SetActive(true);
    }

    //Adiciona pedido à lista e ordena a lista.
    public void GetOrder(DishData order)
    {
        Debug.Log(order);
        orders.Enqueue(order);
        numberOfOrders.text = $"{orders.Count}";
        Debug.Log(orders);
        //AudioManager.instance.PPedido();
    }

    //Toggle de pausar e despausar o jogo. 
    public void PauseToggle()
    {
        _paused = !_paused;
        PauseGame(_paused);
    }

    //Pausa ou despausa só se não tiver.
    public void PauseGame(bool _yesNo)
    {
        _paused = _yesNo;

        if(_paused)
        {
            Time.timeScale = 0.0f;
            UIManager.Instance.EnablePopup(UIManager.ScreenEnum.PauseMenu);
        }
        else
        {
            Time.timeScale = 1.0f;
            UIManager.Instance.DisablePopup(UIManager.ScreenEnum.PauseMenu);
        }
    }

    // Começa a co-rotina de cozinhar o prato.
    public void StartCooking(DishData dish)
    {
        StartCoroutine(Cook(dish));
        numberOfOrders.text = $"{orders.Count}";
    }
    
    // Faz uma contagem e, após, adiciona o objeto do prato cozinhado à fila de pratos a serem servidos.
    public IEnumerator Cook(DishData dish)
    {
        yield return new WaitForSeconds(dish.preparationTime);
        plates.Enqueue(dish.inPlateObj);
        chef.FinishedCooking();
    }

    // Instancia o proximo prato na posição do dispenser.
    public void ServePlate(){
        foreach(Dispenser data in dispensers){   
            if(!data.IsOccupied() && plates.Count > 0){
                Instantiate(plates.Dequeue(), data.transform.position + data.transform.up/2, data.transform.rotation);
            }
        }
    }
    public void UpdateMoney()
    {
        foreach(TMP_Text text in moneyText)
        {
            text.text = $" {money},00 ";
        }
    }
}
