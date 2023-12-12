using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimatedImage : UIElement
{
    private Image image;
    public Sprite[] sprites;
    private int spriteIndex;
    public float frameRate;
    private float timer;

    private void Start() 
    {
        image = GetComponent<Image>();
        image.sprite = sprites[0];
        spriteIndex = 0;
        timer = 1/frameRate;
    }

    private void Update() 
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer += 1/frameRate;
            spriteIndex++;
            if(spriteIndex >= sprites.Length) spriteIndex = 0;
            image.sprite = sprites[spriteIndex];
        }
    }
}
