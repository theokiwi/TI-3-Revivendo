using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : Singleton<Events>
{
    public enum Seasons : int {Standart,Carnival, Easter, Valentines, FestaJulina, Halloween, Christmas}; 
    [SerializeField] private Seasons currentSeason;
    [SerializeField] private GameObject currentScenario;
    
    public List<GameObject> scenarios = new List<GameObject>();
   
   public void Awake(){
    currentSeason = Seasons.Standart;
   }
   public void ChangeSeason(int seasonsPassed){
    switch (currentSeason){
        case Seasons.Standart:
        currentSeason = Seasons.Standart;
        currentScenario.SetActive(false);
        currentScenario = scenarios[0];
        currentScenario.SetActive(true);
        Debug.Log("Changed 0");
            break;
        case Seasons.Carnival:
        currentSeason = Seasons.Carnival;
        currentScenario.SetActive(false);
        currentScenario = scenarios[1];
        currentScenario.SetActive(true);
        Debug.Log("Changed 1");
            break;
        case Seasons.Easter:
        currentSeason = Seasons.Easter;
        currentScenario.SetActive(false);
        currentScenario = scenarios[2];
        currentScenario.SetActive(true);
        Debug.Log("Changed 2");
            break;
        case Seasons.Valentines:
        currentSeason = Seasons.Valentines;
        currentScenario.SetActive(false);
        currentScenario = scenarios[3];
        currentScenario.SetActive(true);
        Debug.Log("Changed 3");
            break;
        case Seasons.FestaJulina:
        currentSeason = Seasons.FestaJulina;
        currentScenario.SetActive(false);
        currentScenario = scenarios[4];
        currentScenario.SetActive(true);
        Debug.Log("Changed 4");
            break;
        case Seasons.Halloween:
        currentSeason = Seasons.Halloween;
        currentScenario.SetActive(false);
        currentScenario = scenarios[4];
        currentScenario.SetActive(true);
        Debug.Log("Changed 5");
        
            break;
        case Seasons.Christmas:
        currentSeason = Seasons.Christmas;
        currentScenario.SetActive(false);
        currentScenario = scenarios[5];
        currentScenario.SetActive(true);
        Debug.Log("Changed 6");
            break;
        
    }

   }
}
