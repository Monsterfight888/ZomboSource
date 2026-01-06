using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class item : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject prefabGFX;
    [System.NonSerialized]
    public GameObject uiPrefab;
    [System.NonSerialized]
    public GameObject Prefabitem;
    private PlayerInventory inventory;
    public ItemDataTemp itemData;
    public float savePosX;
    public int ItemID;
    //Save Variables
    //private float[] Saveposition;
    public int gfxUses;
    public float buildingHealthMax;
    [System.NonSerialized]
    public float buildingHealthCurrent;
    public TextMesh textMesh;
    public bool pickUpAble = true;

    void Awake()
    {
        buildingHealthCurrent = buildingHealthMax;
        prefabGFX = itemData.prefabGFX;
        uiPrefab = itemData.uiPrefab;
        Prefabitem = itemData.Prefabitem;
    }
    void Start()
    {
        inventory = manager.instance.player.GetComponent<PlayerInventory>();
        //transform.position = new Vector3(PlayerPrefs.GetFloat("xSave"), PlayerPrefs.GetFloat("ySave"), 0);
    }
    public void Damage(float damage)
    {
        buildingHealthCurrent -= damage;
        if(buildingHealthCurrent <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnApplicationQuit()
    {
        SaveData.SaveFloat(transform.position.x, "Position X", gameObject);
        SaveData.SaveFloat(transform.position.y, "Position Y", gameObject);
        SaveData.SaveFloat(transform.rotation.eulerAngles.z, "EuelerRotation", gameObject);
    }
    void Update()
    {
        if(buildingHealthCurrent == buildingHealthMax)
        {
            if(textMesh != null)
            {
                textMesh.gameObject.SetActive(false);
            }
        }
        else
        {
            textMesh.gameObject.SetActive(true);
            textMesh.text = buildingHealthCurrent.ToString();
        }
        //Saving

        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveData.SaveFloat(transform.position.x, "Position X", gameObject);
            SaveData.SaveFloat(transform.position.y, "Position Y", gameObject);
            SaveData.SaveFloat(transform.rotation.eulerAngles.z, "EuelerRotation", gameObject);
            PlayerPrefs.Save();
            /*PlayerPrefs.SetFloat("xSave" + transform.position.x.ToString(), transform.position.x);
            PlayerPrefs.SetFloat("ySave" + transform.position.y.ToString(), transform.position.y);
            PlayerPrefs.Save();
            savePosX = PlayerPrefs.GetFloat("ySave" + gameObject.name);
            Debug.Log("SAVED, " + PlayerPrefs.GetFloat("xSave" + gameObject.name) + ", " + PlayerPrefs.GetFloat("ySave" + gameObject.name));*
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            float t_x = SaveData.GetFloat("Position X", gameObject);
            float t_y = SaveData.GetFloat("Position Y", gameObject);
            float t_Eulerz = SaveData.GetFloat("EuelerRotation", gameObject);
            transform.position = new Vector3(t_x, t_y, 0);
            transform.rotation = Quaternion.Euler(0, 0, t_Eulerz);
            /*if(PlayerPrefs.HasKey("xSave" + transform.position.x.ToString()))
            {

            }
            transform.position = new Vector3(PlayerPrefs.GetFloat("xSave" + transform.position.x.ToString()), PlayerPrefs.GetFloat("ySave" + transform.position.y.ToString()), 0);
            Debug.Log("LOADED, " + PlayerPrefs.GetFloat("xSave" + transform.position.x) + ", " + PlayerPrefs.GetFloat("ySave" + gameObject.name));*//*
        }*/
        /*if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.DeleteAll();
        }*/
    }
}
