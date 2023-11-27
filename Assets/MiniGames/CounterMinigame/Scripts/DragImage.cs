using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragImage : MonoBehaviour, IEndDragHandler ,IDragHandler
{
    bool drag = true;
    [SerializeField] Transform drop;
    public void OnEndDrag(PointerEventData eventData)
    {
        if((transform.position - drop.transform.position).normalized.magnitude < 4f)
        {
            drag = false;
            transform.position = drop.position;
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
