using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

namespace Protobot.UI {
    public class AddPartsUI : MonoBehaviour {
        public GameObject lastAddedObj;

        [Header("UI")]
        public Text EmptyListText;
        public string EmptySearchMessage;
        public Text searchText; //the text typed in the searchbar
        private string prevSearch; //the text typed in the searchbar
        public Toggle searchToggle;
        public Dropdown groupDropdown;
        [SerializeField] private float spacing;

        [Space(10)]
        public GameObject partUI; //the UI for individual packets
        public RectTransform partUIsContainer; //used for parenting

        public ToggleGroup partDisplayToggleGroup;
        private int toggleCount => partDisplayToggleGroup.ActiveToggles().Count<Toggle>();
        private int prevToggleCount;

        [Space(10)]

        public UnityEvent OnSelectPartDisplay;
        public UnityEvent OnDeselectPartDisplay;
        

        void Start() {
            PartDisplayUI.OnChangeSelected += _ => {
                OnSelectPartDisplay?.Invoke();
            };

            groupDropdown.onValueChanged.AddListener(index => {
                string group = groupDropdown.options[index].text;

                if (group == "None") {
                    DisplaySearchResults();
                }
                else {
                    DisplayListGroup(group);
                }
            });
            
            DisplaySearchResults();
        }
        
        void Update() {
            if (searchToggle.isOn && searchText.text != prevSearch) {
                DisplaySearchResults();
            }

            prevSearch = searchText.text;
            
            if (toggleCount == 0 && prevToggleCount != 0)
                OnDeselectPartDisplay?.Invoke();

            prevToggleCount = toggleCount;
        }

        public void DeslectSelected() {
            if (toggleCount != 0)
                PartDisplayUI.selected.GetComponent<Toggle>().isOn = false;
        }

        public void SetEmptyListText(string message) {
            EmptyListText.gameObject.SetActive(true);
            EmptyListText.text = message;
        }

        public void DisplayListGroup(string group) {
            List<PartType> groupList = PartsManager.partTypes.Where(p => p.group.ToString() == group).ToList();
            UpdateDisplayedParts(groupList);
        }

        public void DisplaySearchResults() {
            searchToggle.isOn = true;
            string search = searchText.text.ToLower();
            List<PartType> searchList = PartsManager.partTypes.Where(p => 
                CompareSearch(search, p.name)
                && p.group != PartType.PartGroup.None).ToList();
                
            UpdateDisplayedParts(searchList);

            if (searchList.Count == 0)
                SetEmptyListText(EmptySearchMessage);
        }

        public bool CompareSearch(string search, string compare) {
            compare = compare.ToLower();
            return (search.Contains(compare) || compare.Contains(search));
        }

        public void DestroyDisplayedParts() {
            int prevListLength = partUIsContainer.childCount;

            for (int c = 1; c < prevListLength; c++)
                Destroy(partUIsContainer.GetChild(c).gameObject);
        }

        //updates list of objects shown given a list of PartPackets
        public void UpdateDisplayedParts(List<PartType> partsToDisplay) {
            EmptyListText.gameObject.SetActive(false);

            DestroyDisplayedParts();

            for (int i = 0; i < partsToDisplay.Count; i++) {
                GameObject newItem = Instantiate(partUI);
                newItem.transform.SetParent(partUIsContainer);

                RectTransform newRectTransform = newItem.GetComponent<RectTransform>();
                newRectTransform.localScale = Vector3.one;
                newRectTransform.anchoredPosition = new Vector2(0 ,i * (partUI.GetComponent<RectTransform>().sizeDelta.y + spacing));

                PartDisplayUI newPartDisplayUI = newItem.GetComponent<PartDisplayUI>();
                newPartDisplayUI.SetDisplay(partsToDisplay[i]);

                Toggle newToggle = newItem.GetComponent<Toggle>();
                newToggle.group = partDisplayToggleGroup;
            }
            partUIsContainer.sizeDelta = new Vector2(partUIsContainer.sizeDelta.x, (partsToDisplay.Count) * (partUI.GetComponent<RectTransform>().sizeDelta.y + spacing) - spacing);
        }
    }
}