using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text rank;

    private void Start()
    {
        if(PlayerPrefs.GetString("Rank") == null)
        {
            rank.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else
        {
            rank.text = $"{PlayerPrefs.GetString("Rank")}";
        }
    }
}
