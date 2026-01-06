using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{
    public Text numberOfZombosConstant;
    public Text numberOfSecondsConstant;
    public Text zombosAcumulationActivation;
    public TransitionToNewScene constantTransition;
    //public TransitionToNewScene activationTransition;
    public Text matsSeconds;
    public Text matsAmount;
    private float numberOfZombosConstantSave;
    private float numberOfSecondsConstantSave;
    public Dropdown typeDropDown;
    public Dropdown zomboDropDown;
    private float zombosAcumulationActivationSave;
    private float numberOfMatSaved;
    private float numberOfSecondsMatSaved;
    private int savedMat;
    public GameObject[] mats;
    public GameObject[] zomboTypes;
    private int savedZomboType;
    private float minY;
    private float minX;
    private float maxY;
    private float maxX;
    private float minYZombo;
    private float minXZombo;
    private float maxYZombo;
    private float maxXZombo;
    public Text minYText;
    public Text minXText;
    public Text maxYText;
    public Text maxXText;
    public Text minYTextZombo;
    public Text minXTextZombo;
    public Text maxYTextZombo;
    public Text maxXTextZombo;
    private bool mode;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if(manager.instance != null)
        {
            if(mode == true)
            {
                manager.instance.mode = manager.modes.activationBased;

                manager.instance.zombiesGatheredPerSecond = zombosAcumulationActivationSave;
            }
            else
            {
                manager.instance.mode = manager.modes.zombieEverySecond;
                
                manager.instance.howManyZombiesEverySecond = numberOfZombosConstantSave;
                manager.instance.timeBtwZombies = numberOfSecondsConstantSave;
            }
            manager.instance.zomboPrefab = zomboTypes[savedZomboType];
            manager.instance.matsSpawnedAtOnce = numberOfMatSaved;
            manager.instance.timeBtwMats = numberOfSecondsMatSaved;
            manager.instance.mat = mats[savedMat];
            if(minX == 0 && minY == 0 && maxX == 0 && maxY == 0)
            {

            }
            else
            {
                manager.instance.whereMatsSpawnMin = new Vector2(minX, minY);
                manager.instance.whereMatsSpawnMax = new Vector2(maxX, maxY);
            }
            if (minXZombo == 0 && minYZombo == 0 && maxXZombo == 0 && maxYZombo == 0)
            {

            }
            else
            {
                manager.instance.whereZombosSpawnMin = new Vector2(minXZombo, minYZombo);
                manager.instance.whereZombosSpawnMax = new Vector2(maxXZombo, maxYZombo);
            }
        }
        //Self Destruct Protocoll
        if(manager.instance == null && matsSeconds == null)
        {
            Destroy(gameObject);
        }
    }
    public void Constant()
    {
        mode = false;
        setUpMats();
        if (numberOfZombosConstant.text != "" || numberOfSecondsConstant.text != "" || matsAmount.text != "" || matsSeconds.text != "")
        {
            numberOfZombosConstantSave = float.Parse(numberOfZombosConstant.text);
            numberOfSecondsConstantSave = float.Parse(numberOfSecondsConstant.text);
        }
        else
        {
            numberOfZombosConstantSave = 5;
            numberOfSecondsConstantSave = 15;
        }
        if (minXTextZombo.text == "" || minYTextZombo.text == "" || maxXTextZombo.text == "" || maxYTextZombo.text == "")
        {
            minXZombo = 0;
            minYZombo = 0;
            maxXZombo = 0;
            maxYZombo = 0;
        }
        else
        {
            minXZombo = float.Parse(minXTextZombo.text);
            minYZombo = float.Parse(minYTextZombo.text);
            maxXZombo = float.Parse(maxXTextZombo.text);
            maxYZombo = float.Parse(maxYTextZombo.text);
        }
        savedZomboType = zomboDropDown.value;
        savedMat = typeDropDown.value;
        constantTransition.Transition();
    }
    public void setUpMats()
    {
        if (matsAmount.text == "" || matsSeconds.text == "")
        {
            numberOfMatSaved = 5;
            numberOfSecondsMatSaved = 5;
        }
        else
        {
            numberOfMatSaved = float.Parse(matsAmount.text);
            numberOfSecondsMatSaved = float.Parse(matsSeconds.text);
        }
        if (minXText.text == "" || minYText.text == "" || maxXText.text == "" || maxYText.text == "")
        {
            minX = 0;
            minY = 0;
            maxX = 0;
            maxY = 0;
        }
        else
        {
            minX = float.Parse(minXText.text);
            minY = float.Parse(minYText.text);
            maxX = float.Parse(maxXText.text);
            maxY = float.Parse(maxYText.text);
        }
    }
    public void Activation()
    {
        mode = true;
        setUpMats();
        if (zombosAcumulationActivation.text != "" || matsAmount.text != "" || matsSeconds.text != "")
        {
            zombosAcumulationActivationSave = float.Parse(zombosAcumulationActivation.text);
        }
        else
        {
            zombosAcumulationActivationSave = 0.25f;
            numberOfMatSaved = 5;
            numberOfSecondsMatSaved = 5;
        }
        if (minXTextZombo.text == "" || minYTextZombo.text == "" || maxXTextZombo.text == "" || maxYTextZombo.text == "")
        {
            minXZombo = 0;
            minYZombo = 0;
            maxXZombo = 0;
            maxYZombo = 0;
        }
        else
        {
            minXZombo = float.Parse(minXTextZombo.text);
            minYZombo = float.Parse(minYTextZombo.text);
            maxXZombo = float.Parse(maxXTextZombo.text);
            maxYZombo = float.Parse(maxYTextZombo.text);
        }
        savedMat = typeDropDown.value;
        savedZomboType = zomboDropDown.value;
        constantTransition.Transition();
    }
    /*public void Activation()
    {
        if(zombosAcumulationActivation.text != "")
        {
            zombosAcumulationActivationSave = float.Parse(zombosAcumulationActivation.text);
            constantTransition.Transition();
        }
    }*/
}
