using System;
using System.Collections;
using System.Collections.Generic;
using ELMA.SDK.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace Controllers
{
    public class BuildController : MonoBehaviour
    {
        public PurchaseItemDescription selectedItem = null;
        public Dictionary<string, PurchaseItem> ExistingPurchaseItems { get; private set; }
        public Transform[] scanPlanes;
        
        public Transform slotsPivot;
        public int slotSize;
        public GameObject slotVisualPrefab;

        private List<Transform> _slots;
        public bool BuildMode { get; private set; }

        private GameController _gameController;

        private void Awake()
        {
            _gameController = FindObjectOfType<GameController>();
            
            _slots = new List<Transform>();

            ExistingPurchaseItems = new Dictionary<string, PurchaseItem>();
            var items = Resources.LoadAll<GameObject>("PurchaseItems");
            foreach (var item in items)
            {
                var c = item.GetComponent<PurchaseItem>();
                ExistingPurchaseItems.Add(c.description.name, c);
            }
        }

        void Start()
        {
            for (var i = 0; i < scanPlanes.Length; i++)
            {
                var bounds = scanPlanes[i].GetComponent<Collider>().bounds;
                var minBound = bounds.min;
                var maxBound = bounds.max;
                maxBound.y = minBound.y;
                var pos = minBound;
                do
                {
                    do
                    {

                        RaycastHit hit;
                        
                        if (Physics.Raycast(pos - new Vector3(0, 1.0f, 0), Vector3.down, out hit, 100.0f) 
                            && hit.transform.CompareTag("Floor"))
                        {
                            var slot = new GameObject("ItemSlot");
                            var col =slot.AddComponent<BoxCollider>();
                            col.center = Vector3.zero;
                            col.size = new Vector3(slotSize, 0.1f, slotSize);
                            slot.transform.SetParent(slotsPivot);
                            slot.transform.position = hit.point;
                            slot.tag = "Slot";
                            slot.AddComponent<ItemSlot>();
                            var visual = Instantiate(slotVisualPrefab, slot.transform);
                            visual.transform.localScale = new Vector3(slotSize, 0.1f, slotSize);
                            visual.transform.localPosition = Vector3.zero;
                            _slots.Add(slot.transform);
                        }
                        
                        pos.x += slotSize;

                    } while (pos.x <= maxBound.x);

                    pos.x = minBound.x;
                    pos.z += slotSize;
                } while (pos.z <= maxBound.z);
            }
            
            ToggleBuildMode(false);
        }

        void Update()
        {
            
        }

        public void ToggleBuildMode(bool state)
        {
            BuildMode = state;
            foreach (var s in _slots)
            {
                s.gameObject.SetActive(state);
            }
        }

        public void ToggleBuildMode() => ToggleBuildMode(!BuildMode);

        public void TryBuild(Collider collider)
        {
            if (!BuildMode)
                return;
            
            var itemSlot = collider.GetComponent<ItemSlot>();
            if (itemSlot.assignedItem != null)
            {
                // ToDo say cannot build here
                Debug.Log("Cannot build here");
                return;
            }

            if (selectedItem == null || string.IsNullOrEmpty(selectedItem.name))
            {
                return;
            }

            if (_gameController.UserModel.Money >= selectedItem.price)
            {
                _gameController.UserModel.Money -= selectedItem.price;
                var itemObj = Instantiate(ExistingPurchaseItems[selectedItem.name].gameObject, transform);
                itemObj.transform.position = itemSlot.transform.position;
                itemSlot.assignedItem = itemObj;
                _gameController.UserOfficeModel.PurchasedItems.Add(
                    new PurchaseItemModel { Name = selectedItem.name, Location = itemObj.transform.position });
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }

        [CanBeNull]
        private GameObject GetSlotItem(Transform slot)
        {
            return slot.childCount > 0 ? slot.GetChild(0).gameObject : null;
        }
    }
}