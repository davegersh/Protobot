using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MouseButtonEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
    [SerializeField] private PointerEventData.InputButton mouseButton;

    [Space(15)]

    [SerializeField] private UnityEvent OnMouseDown;
    [SerializeField] private UnityEvent OnMouseUp;
    [SerializeField] private UnityEvent OnMouseClick;

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == mouseButton) {
            OnMouseDown?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (eventData.button == mouseButton) {
            OnMouseUp?.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == mouseButton) {
            OnMouseClick?.Invoke();
        }
    }
}
