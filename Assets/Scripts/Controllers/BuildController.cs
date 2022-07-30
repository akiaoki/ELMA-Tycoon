using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Controllers
{
    public class BuildController : MonoBehaviour
    {
        public PurchaseItemDescription selectedItem;
        public List<PurchaseItem> ExistingPurchaseItems { get; private set; }
        public Transform[] scanPlanes;
        
        public Transform slotsPivot;
        public int slotSize;
        public GameObject slotVisualPrefab;

        private List<Transform> _slots;
        private bool _buildMode;

        private GameController _gameController;

        private void Awake()
        {
            _gameController = FindObjectOfType<GameController>();
            
            _slots = new List<Transform>();

            ExistingPurchaseItems = new List<PurchaseItem>();
            var items = Resources.LoadAll<GameObject>("PurchaseItems");
            foreach (var item in items)
            {
                ExistingPurchaseItems.Add(item.GetComponent<PurchaseItem>());
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
                            col.size = new Vector3(slotSize, 0, slotSize);
                            slot.transform.SetParent(slotsPivot);
                            slot.transform.position = hit.point;
                            slot.tag = "Slot";
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
        }

        void Update()
        {

        }

        public void ToggleBuildMode(bool state)
        {
            _buildMode = state;
            foreach (var s in _slots)
            {
                s.gameObject.SetActive(state);
            }
        }

        public void TryBuild(Collider collider)
        {
            if (!_buildMode)
                return;

            var item = GetSlotItem(collider.transform);
            if (item != null)
            {
                // ToDo say cannot build here
                Debug.Log("Cannot build here");
                return;
            }

            if (selectedItem == null)
            {
                return;
            }

            if (_gameController.UserModel.Money >= selectedItem.price)
            {
                _gameController.UserModel.Money -= selectedItem.price;
                
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