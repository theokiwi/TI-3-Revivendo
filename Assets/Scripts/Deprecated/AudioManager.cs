using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource click,fogao,pegaPedido,item,music;
    [SerializeField] AudioClip shop, ruins;

    private void Awake()
    {
        if(instance == null)
        {
        instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Shop();
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
