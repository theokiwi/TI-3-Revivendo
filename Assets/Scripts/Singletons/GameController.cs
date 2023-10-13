using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : Singleton<GameController>
{
    public Queue<DishData> orders;
    public Queue<GameObject> plates;
    [SerializeField] TMP_Text  moneyText;
    [SerializeField] TMP_Text numberOfOrders;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject endScreen;

    private bool _paused = true;
    public bool _IsPaused { get => _paused; }

    public float money;

    public int lostClients;

    [SerializeField] Transform[] dispensers;
    [SerializeField] Vector3 boxCastSize;
    [SerializeField] float maxDistance = 300.0f;
    [SerializeField] LayerMask plateLayer;

    private void Awake()
    {
        //singleton pattern transferido pra Singleton<T>
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //inicialização movida para o método StartGame()
        StartGame();
    }

    //reinicia todos os valores para o início de um novo jogo / carrega os valores de um save
    private void StartGame() 
    {
        money = 0f;
        moneyText.text = $" {money},00 ";
        numberOfOrders.text = $"{0}";

        Time.timeScale = 1f;

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
        return money - (lostClients * 10);
    }

    //Adiciona valor inputado ao dinheiro do jogador, e atualiza o contador na tela.
    public void AddMoney (int value)
    {
        money += value;
        moneyText.text = $" {money} ";
    }
    //Adiciona 1 a contagem de clientes perdidos (TODO: trocar isso por um sistema de satisfacao pro cliente) 
    public void LoseClient()
    {
        lostClients--;
    }

    public void GameOver()
    {
        PauseGame(true);
        UIManager.Instance.EnablePopup(UIManager.ScreenEnum.EndGameScreen);
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
    }
    
    // Faz uma contagem e, após, adiciona o objeto do prato cozinhado à fila de pratos a serem servidos.
    public IEnumerator Cook(DishData dish)
    {
        yield return new WaitForSeconds(dish.preparationTime);
        plates.Enqueue(dish.inPlateObj);
    }

    // Instancia o proximo prato na posição do dispenser.
    public void ServePlate()
    {
        foreach(Transform data in dispensers)
        {
            bool hitDetect;
            RaycastHit hit;

            hitDetect = Physics.BoxCast(data.transform.position, boxCastSize/2, Vector3.up, out hit, data.transform.rotation, maxDistance, plateLayer);
            if(hitDetect == false)
            {
                //Debug.Log("Dispenser vazio.");
                try{
                    Instantiate(plates.Dequeue(), data.transform.position + data.transform.up/2, data.transform.rotation);
                    AudioManager.instance.Fogao();
                }catch{}
                break;
            }
            else if( hitDetect == true)
            {
                Debug.Log("Ocupado");
            }
        }        
    }
}
