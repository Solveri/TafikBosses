using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIPageScript : MonoBehaviour
{
    [SerializeField]
    RectTransform contentPanel;

    public UIItem itemPrefab;
    List<UIItem> items = new List<UIItem>();
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Image ChoosedItemDisplay;
    [SerializeField]
    ItemScriptable item;
    [SerializeField]UIItem ChosenBall;
    [SerializeField]UIItem ChosenBoard;
    [SerializeField]
    List<ItemScriptable> itemScriptables = new List<ItemScriptable>();
    // Start is called before the first frame update
    void Start()
    {
       
        InitInventory(3);
       
    }
    private void OnEnable()
    {
        UIItem.OnItemClick += OnItemClicked;
    }

    public void InitInventory(int size)
    {
      

        for (int i = 0; i < size; i++)
        {
            // Instantiate the new UIItem at the default position and rotation
            UIItem newItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentPanel);

            // Add the new item to the list
            items.Add(newItem);

            // Check if the corresponding itemScriptable is null or not and set data accordingly
            if (i < itemScriptables.Count)
            {
                newItem.SetData(itemScriptables[i]);
            }
            else
            {
                newItem.SetData(item);
            }
        }
    }

private void OnItemClicked(UIItem item)
    {
        ChoosedItemDisplay.sprite = item.GetImage();
        ChosenBall = item;
        text.text = item.GetDes();
      
        
        
        
    }

    private void OnDisable()
    {
        UIItem.OnItemClick -= OnItemClicked;
    }
    
}
