using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public float testStupid;
    public KeyCode pickUpKey;
    public KeyCode slot1Key;
    public KeyCode slot2Key;
    public KeyCode dropKey;
    public KeyCode craftKey;
    public GameObject ItemHolder;
    public float raduis;
    public int itemSlot = 1;
    private GameObject uiPrefabSlot1Refrence;
    private GameObject uiPrefabSlot2Refrence;
    private GameObject IteminSlot1PrefabGFXRefrence;
    private GameObject IteminSlot2PrefabGFXRefrence;
    private bool isFull1;
    private bool isFull2;
    private ItemDataTemp itemData1;
    private ItemDataTemp itemData2;
    private GameObject objectInSlot1;
    private GameObject objectInSlot2;
    public ItemCraftingTemp[] recipes;
    public GameObject arm;

    void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void destroyItem(int item)
    {
        if(item == 1)
        {
            Destroy(objectInSlot1);
            Destroy(IteminSlot1PrefabGFXRefrence);
            IteminSlot1PrefabGFXRefrence = null;
            Destroy(uiPrefabSlot1Refrence);
            isFull1 = false;
            itemData1 = null;
        }
        else if(item == 2)
        {
            Destroy(objectInSlot2);
            Destroy(IteminSlot2PrefabGFXRefrence);
            IteminSlot2PrefabGFXRefrence = null;
            Destroy(uiPrefabSlot2Refrence);
            isFull2 = false;
            itemData2 = null;
        }
    }
     
    void Update()
    {
        if (IteminSlot1PrefabGFXRefrence != null && IteminSlot1PrefabGFXRefrence.GetComponent<itemGFX>().showDurability && itemSlot == 1)
        {
            GameObject.Find("UI Slot 1" + uiPrefabSlot1Refrence.name + "/Durability Text").GetComponent<Text>().text = 
                IteminSlot1PrefabGFXRefrence.GetComponent<itemGFX>().uses.ToString();
        }
        else if (IteminSlot2PrefabGFXRefrence != null && IteminSlot2PrefabGFXRefrence.GetComponent<itemGFX>().showDurability && itemSlot == 2)
        {
            GameObject.Find("UI Slot 2" + uiPrefabSlot2Refrence.name + "/Durability Text").GetComponent<Text>().text = 
                IteminSlot2PrefabGFXRefrence.GetComponent<itemGFX>().uses.ToString();
        }
        if(IteminSlot2PrefabGFXRefrence != null && itemSlot == 2)
        {
            arm.SetActive(true);
            GetComponent<Animator>().SetBool("hasArm", false);
        }
        else if(IteminSlot1PrefabGFXRefrence != null && itemSlot == 1)
        {
            arm.SetActive(true);
            GetComponent<Animator>().SetBool("hasArm", false);
        }
        else
        {
            arm.SetActive(false);
            GetComponent<Animator>().SetBool("hasArm", true);
        }
        if (Input.GetKeyDown(craftKey))
        {
            bool hasGottenPrefab = false;
            for (int i = 0; i < recipes.Length; i++)
            {
                if(recipes[i].item1 == itemData1 && recipes[i].item2 == itemData2)
                {
                    destroyItem(1);
                    destroyItem(2);
                    itemData1 = recipes[i].itemproduced;
                    objectInSlot1 = Instantiate(recipes[i].itemproduced.Prefabitem);
                    objectInSlot1.SetActive(false);
                    item t_itemInSlot = objectInSlot1.GetComponent<item>();
                    IteminSlot1PrefabGFXRefrence = Instantiate(t_itemInSlot.prefabGFX, ItemHolder.transform);
                    uiPrefabSlot1Refrence = Instantiate(t_itemInSlot.uiPrefab, manager.instance.uiSlot1.transform);
                    manager.instance.uiSlot1.transform.Find("Backround").GetComponent<RawImage>().color = Color.green;
                    manager.instance.uiSlot2.transform.Find("Backround").GetComponent<RawImage>().color = Color.black;
                    itemSlot = 1;
                    isFull1 = true;
                    GetComponent<Animator>().SetTrigger("Craft");
                    hasGottenPrefab = true;
                }
                if (hasGottenPrefab)
                {
                    break;
                }
            }
        }
        if (Input.GetKeyDown(pickUpKey))
        {
            PickUpObjects();
        }
        testStupid = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKeyDown(slot1Key)||Input.GetAxis("Mouse ScrollWheel") >= -0.1 && Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            itemSlot = 1;
            manager.instance.uiSlot1.transform.Find("Backround").GetComponent<RawImage>().color = Color.green;
            manager.instance.uiSlot2.transform.Find("Backround").GetComponent<RawImage>().color = Color.black;
            if(/*IteminSlot1PrefabGFXRefrence.activeSelf == false && itemData1 != null*/ IteminSlot1PrefabGFXRefrence != null)
            {
                IteminSlot1PrefabGFXRefrence.SetActive(true);
            }
            if(IteminSlot2PrefabGFXRefrence != null && IteminSlot2PrefabGFXRefrence.activeSelf == true)
            {
                IteminSlot2PrefabGFXRefrence.SetActive(false);
                itemData1 = null;
            }
        }
        else if (Input.GetKeyDown(slot2Key) || Input.GetAxis("Mouse ScrollWheel") <= 0.1 && Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            itemSlot = 2;
            manager.instance.uiSlot2.transform.Find("Backround").GetComponent<RawImage>().color = Color.green;
            manager.instance.uiSlot1.transform.Find("Backround").GetComponent<RawImage>().color = Color.black;
            if (/*IteminSlot2PrefabGFXRefrence.activeSelf == false && itemData2 != null || */IteminSlot2PrefabGFXRefrence != null/* && itemData2 != null*/)
            {
                IteminSlot2PrefabGFXRefrence.SetActive(true);
            }
            if (IteminSlot1PrefabGFXRefrence != null && IteminSlot1PrefabGFXRefrence.activeSelf == true)
            {
                IteminSlot1PrefabGFXRefrence.SetActive(false);
                itemData2 = null;
            }
        }
        if (Input.GetKeyDown(dropKey))
        {
            if(itemSlot == 1 && isFull1)
            {
                //Instantiate(itemData1.Prefabitem, ItemHolder.transform.position, ItemHolder.transform.rotation);
                objectInSlot1.SetActive(true);
                objectInSlot1.GetComponent<item>().gfxUses = IteminSlot1PrefabGFXRefrence.GetComponent<itemGFX>().uses;
                objectInSlot1.transform.position = ItemHolder.transform.position;
                objectInSlot1.transform.rotation = ItemHolder.transform.rotation;
                Destroy(IteminSlot1PrefabGFXRefrence);
                IteminSlot1PrefabGFXRefrence = null;
                Destroy(uiPrefabSlot1Refrence);
                isFull1 = false;
                itemData1 = null;
            }
            else if(itemSlot == 2 && isFull2)
            {
                //Instantiate(itemData2.Prefabitem, ItemHolder.transform.position, ItemHolder.transform.rotation);
                objectInSlot2.SetActive(true);
                objectInSlot2.GetComponent<item>().gfxUses = IteminSlot2PrefabGFXRefrence.GetComponent<itemGFX>().uses;
                objectInSlot2.transform.position = ItemHolder.transform.position;
                objectInSlot2.transform.rotation = ItemHolder.transform.rotation;
                Destroy(IteminSlot2PrefabGFXRefrence);
                IteminSlot2PrefabGFXRefrence = null;
                Destroy(uiPrefabSlot2Refrence);
                isFull2 = false;
                itemData2 = null;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ItemHolder.transform.position, raduis);
    }
    public void PickUpObjects()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(ItemHolder.transform.position, raduis);
        List<float> distances = new List<float>();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].transform.GetComponent<item>() && hitColliders[i].transform.GetComponent<item>().pickUpAble)
            {
                distances.Add(Vector2.Distance(hitColliders[i].transform.position, ItemHolder.transform.position));
            }
        }
        distances.Sort();
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].transform.GetComponent<item>() &&
                Vector3.Distance(hitColliders[i].transform.position, ItemHolder.transform.position) == distances[0])
                {
                    if (itemSlot == 1 && isFull1 == false && Vector2.Distance(hitColliders[i].transform.position,
                    ItemHolder.transform.position) == distances[0])
                    {
                        itemData1 = hitColliders[i].transform.GetComponent<item>().itemData;
                        item t_itemInSlot = hitColliders[i].transform.GetComponent<item>();
                        IteminSlot1PrefabGFXRefrence = Instantiate(t_itemInSlot.prefabGFX, ItemHolder.transform);
                        IteminSlot1PrefabGFXRefrence.GetComponent<itemGFX>().uses = t_itemInSlot.gfxUses;
                        //IteminSlot1PrefabGFXRefrence.GetComponent<itemGFX>().itemHealth = t_itemInSlot.buildingHealthCurrent;
                        uiPrefabSlot1Refrence = Instantiate(t_itemInSlot.uiPrefab, manager.instance.uiSlot1.transform);
                        objectInSlot1 = hitColliders[i].gameObject;
                        
                        hitColliders[i].gameObject.SetActive(false);
                        isFull1 = true;
                    }
                    else if (itemSlot == 2 && isFull2 == false && Vector2.Distance(hitColliders[i].transform.position,
                    ItemHolder.transform.position) == distances[0])
                    {
                        itemData2 = hitColliders[i].transform.GetComponent<item>().itemData;
                        item t_itemInSlot = hitColliders[i].transform.GetComponent<item>();
                        IteminSlot2PrefabGFXRefrence = Instantiate(t_itemInSlot.prefabGFX, ItemHolder.transform);
                        IteminSlot2PrefabGFXRefrence.GetComponent<itemGFX>().uses = t_itemInSlot.gfxUses;
                        //t_itemInSlot.buildingHealthCurrent = IteminSlot2PrefabGFXRefrence.GetComponent<itemGFX>().itemHealth;
                        uiPrefabSlot2Refrence = Instantiate(t_itemInSlot.uiPrefab, manager.instance.uiSlot2.transform);
                        objectInSlot2 = hitColliders[i].gameObject;
                        hitColliders[i].gameObject.SetActive(false);
                        isFull2 = true;
                    }
                }
            }
        }
    }