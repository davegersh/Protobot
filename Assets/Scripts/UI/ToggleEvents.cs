using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Protobot;

[RequireComponent(typeof(Toggle))]
public class ToggleEvents : MonoBehaviour {
    [CacheComponent] private Toggle toggle;

    public UnityEvent OnToggleOn;
    public UnityEvent OnToggleOff;

    public void Start() {
        toggle.onValueChanged.AddListener(toggleValue => {
            if (toggleValue)
                OnToggleOn?.Invoke();
            else
                OnToggleOff?.Invoke();
        });
    }
}
