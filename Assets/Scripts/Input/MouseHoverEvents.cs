using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MouseHoverEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private UnityEvent OnMouseEnter;
    [SerializeField] private UnityEvent OnMouseExit;

    public void OnPointerEnter(PointerEventData eventData) {
        OnMouseEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData) {
        OnMouseExit?.Invoke();
    }
}
