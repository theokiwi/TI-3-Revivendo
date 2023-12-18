using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource click,fogao,pegaPedido,item,music;
    [SerializeField] AudioClip shop, ruins;
    private void Awake()
    {
        Shop();
        if(Instance != this){
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Click()
    {
        click.Play();
    }
    public void Fogao()
    {
        fogao.Play();
    }
    public void PPedido()
    {
        pegaPedido.Play();
    }
    public void Item()
    {
        item.Play();
    }
    public void Shop()
    {
        music.clip = shop;
        music.Play();
    }
    public void Ruins()
    {
        music.clip = ruins;
        music.Play();
    }
}
