using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragImage : MonoBehaviour, IEndDragHandler ,IDragHandler
{
    bool drag = true;
    [SerializeField] Vector3 drop;

    public void OnEndDrag(PointerEventData eventData)
    {
        if((transform.position - drop).normalized.magnitude < 4f)
        {
            drag = false;
            transform.localPosition = drop;
            CounterMinigame.instance.Task();
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
       if(drag)
       {
            transform.position = Input.mousePosition;

       }
    }
}
