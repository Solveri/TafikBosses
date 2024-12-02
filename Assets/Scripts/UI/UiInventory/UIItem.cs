using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour,IPointerDownHandler
{
   public static event Action<UIItem> OnItemClick;
    [SerializeField] Image itemImage = null;
    [SerializeField] string text = null;
    ItemType type;
    

    ItemScriptable itemScriptable = null;

    private bool empty = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Has Been Donwed");
        OnItemClick?.Invoke(this);
    }

    public void SetData(ItemScriptable item)
    {
        itemScriptable = item;
        text = item.Description;
        itemImage.sprite = item.Sprite;
        type = item.Type;
        
    }
    
    public Sprite GetImage()
    {
        return itemImage.sprite;
    }
    public string GetDes()
    {
        return text;
    }
    public ItemType GetitemType()
    {
       
        return type;
    }
   
}
