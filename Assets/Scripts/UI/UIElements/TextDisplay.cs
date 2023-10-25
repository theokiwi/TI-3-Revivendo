using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//texto que se atualiza sempre que atribui a DisplayText
[RequireComponent(typeof(TMP_Text))]
public class TextDisplay : UIElement
{
    private TMP_Text textComponent;
    private string _displayText;
    public string DisplayText
    {
        get => _displayText;
        set
        {
            _displayText = value;
            UpdateUI();
        }
    }
    private void OnEnable()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        textComponent.text = DisplayText;
    }
}
