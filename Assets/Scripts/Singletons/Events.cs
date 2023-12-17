using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : Singleton<Events>
{
    public enum Seasons : int {Standart,Carnival, Easter, Valentines, FestaJulina, Halloween, Christmas}; 
    [SerializeField] private MenuSO[] menus;
    [SerializeField] private Seasons currentSeason;
    [SerializeField] private GameObject currentScenario;
    [SerializeField] private GameObject currentParticle;
    
    public List<GameObject> scenarios = new List<GameObject>();
    public List<GameObject> eventParticles = new List<GameObject>();
   
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            ChangeSeason();
        }
    }
    public Seasons ReturnSeason()
    {
        return currentSeason;
    }

   public void Awake(){
    currentSeason = Seasons.Standart;
   }
   public void ChangeSeason(){
        Debug.Log(gameObject);
    switch (currentSeason){
        case Seasons.Christmas:
        currentSeason = Seasons.Standart;
        currentScenario.SetActive(false);
        currentParticle.SetActive(false);
        currentScenario = scenarios[0];
        GameController.Instance.menuData = menus[0];
        currentScenario.SetActive(true);
        currentParticle = eventParticles[0];
        currentParticle.SetActive(true);
        Debug.Log("Changed 0");
            break;
        case Seasons.Standart:
        currentSeason = Seasons.Carnival;
        currentScenario.SetActive(false);
        currentParticle.SetActive(false);
        currentScenario = scenarios[1];
        GameController.Instance.menuData = menus[1];
        currentScenario.SetActive(true);
        currentParticle = eventParticles[1];
        currentParticle.SetActive(true);
        Debug.Log("Changed 1");
            break;
        case Seasons.Carnival:
        currentSeason = Seasons.Easter;
        currentScenario.SetActive(false);
        currentParticle.SetActive(false);
        currentScenario = scenarios[2];
        GameController.Instance.menuData = menus[2];
        currentScenario.SetActive(true);
        currentParticle = eventParticles[2];
        currentParticle.SetActive(true);
        Debug.Log("Changed 2");
            break;
        case Seasons.Easter:
        currentSeason = Seasons.Valentines;
        currentScenario.SetActive(false);
        currentParticle.SetActive(false);
        currentScenario = scenarios[3];
        GameController.Instance.menuData = menus[3];
        currentScenario.SetActive(true);
        currentParticle = eventParticles[3];
        currentParticle.SetActive(true);
        Debug.Log("Changed 3");
            break;
        case Seasons.Valentines:
        currentSeason = Seasons.FestaJulina;
        currentScenario.SetActive(false);
        currentParticle.SetActive(false);
        currentScenario = scenarios[4];
        GameController.Instance.menuData = menus[4];
        currentScenario.SetActive(true);
        currentParticle = eventParticles[4];
        currentParticle.SetActive(true);
        Debug.Log("Changed 4");
            break;
        case Seasons.FestaJulina:
        currentSeason = Seasons.Halloween;
        currentScenario.SetActive(false);
        currentParticle.SetActive(false);
        currentScenario = scenarios[5];
        GameController.Instance.menuData = menus[5];
        currentScenario.SetActive(true);
        currentParticle = eventParticles[5];
        currentParticle.SetActive(true);
        Debug.Log("Changed 5");
        
            break;
        case Seasons.Halloween:
        currentSeason = Seasons.Christmas;
        currentScenario.SetActive(false);
        currentParticle.SetActive(false);
        currentScenario = scenarios[6];
        GameController.Instance.menuData = menus[6];
        currentScenario.SetActive(true);
        currentParticle = eventParticles[6];
        currentParticle.SetActive(true);
        Debug.Log("Changed 6");
            break;
        
    }
   }
}
