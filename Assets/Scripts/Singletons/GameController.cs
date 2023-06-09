using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameController : Singleton<GameController>
{
    public List<DishData> orders;
    public Queue<GameObject> plates;
    [SerializeField] TMP_Dropdown[] slots;
    [SerializeField] GameObject kitchenMenu;
    [SerializeField] TMP_Text  moneyText;
    [SerializeField] TMP_Text numberOfOrders;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject endScreen;
    public bool paused = true;
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
        lostClients = 0;
        money = 0f;
        moneyText.text = $" {money},00 ";
        numberOfOrders.text = $"{orders.Count}";

        Time.timeScale = 1f;
        
        plates = new Queue<GameObject>();
    }

    public void FixedUpdate()
    {
        ServePlate();
    }

    // Adiciona valor inputado ao dinheiro do jogador, e atualiza o contador na tela.
    public void AddMoney (int value)
    {
        money += value;
        moneyText.text = $" {money} ";
    }

    public void LoseClient()
    {
        lostClients--;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        endScreen.SetActive(true);
    }

    // Adiciona pedido à lista e ordena a lista.
    public void GetOrder(DishData order)
    {
        orders.Add(order);
        orders.Sort();
        numberOfOrders.text = $"{orders.Count}";
        AudioManager.instance.PPedido();
    }

    // Toggle de pausar e despausar o jogo. 
    public bool PauseGame()
    {
        paused = !paused;

        if(paused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        return paused;
    }

    // Impede conflitos entre o menu de pausa e o da cozinha.
    public void PauseMenu()
    {
        if(!kitchenMenu.activeInHierarchy)
        {
            pauseScreen.SetActive(PauseGame());
        }
    }

    // Impede conflitos entre o menu de pausa e o da cozinha.
    public void OpenKitchen()
    {
        if(!pauseScreen.activeInHierarchy)
        {
            kitchenMenu.SetActive(PauseGame());
            numberOfOrders.text = $"{orders.Count}";
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
