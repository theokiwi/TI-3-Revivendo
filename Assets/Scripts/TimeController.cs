using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class DayNightCycle
    // ta sem comentario pq eu fiz isso cinco horas da manha saindo de casas, eu coloco dps calma
{
    [SerializeField] public float timeMultiplier;
    [SerializeField] public float startHour;
    [SerializeField] public TextMeshProUGUI timeText;
}
