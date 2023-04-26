using UnityEngine;
using System.Collections;

//Padrão singleton de tipagem genérica pra outros scripts herdarem dele
public class Singleton<T> : MonoBehaviour
    where T : Component //não deixa você fazer singleton de tipo que n pode tar num objeto na unity
{
    private static T _instance; //variável privada
    public static T Instance {  //propriedade pública
        get {   
            //se não tiver uma instância, arranhja uma
            if (_instance == null) {
                //primeiro tenta achar na cena
                var objs = FindObjectsOfType (typeof(T)) as T[];
                if (objs.Length > 0)
                    _instance = objs[0];
                if (objs.Length > 1) {
                    Debug.LogError ("There is more than one " + typeof(T).Name + " in the scene.");
                }
                //se não achou, cria um objeto e bota
                if (_instance == null) {
                    GameObject obj = new GameObject ();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<T> ();
                }
            }
            //se tiver só retorna ela
            return _instance;
        }
    }
}