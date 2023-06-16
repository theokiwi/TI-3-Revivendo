using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource click,fogao,pegaPedido,item;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
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
}
