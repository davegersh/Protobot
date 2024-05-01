using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidebarUI : MonoBehaviour {

    [SerializeField] private GameObject[] menus;

    public void SetMenu(int index) { 
        for (int i = 0; i < menus.Length; i++)
            menus[i].SetActive(i == index);
    }
}
