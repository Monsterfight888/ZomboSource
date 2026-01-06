using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public static manager instance;
    public GameObject player;
    public GameObject uiSlot1;
    public GameObject uiSlot2;
    public patterHolder patternHolder;
    public item[] items;
    //List<float> allNumbers;
    public modes mode;
    public float[] waveszombos;
    public float[] wavestime;
    private int waveNumber;
    private float wavesTimer;
    public GameObject zomboPrefab;
    public Text wavesNumber;
    public Text timer;
    public float howManyZombiesEverySecond;
    public float timeBtwZombies;
    public materialModes materialMode;
    public GameObject mat;
    public float timeBtwMats;
    public float matsSpawnedAtOnce;
    public Vector2 whereMatsSpawnMax;
    public Vector2 whereMatsSpawnMin;
    public KeyCode activationKey;
    public float zombiesGatheredPerSecond;
    private float currentZombosGathered;
    public bool doesReturnToMenu;
    public TransitionToNewScene transition;
    public bool waitForSet;
    private bool done;
    private bool done2;
    public Vector2 whereZombosSpawnMax = new Vector2(-56.6f, 23.7f);
    public Vector2 whereZombosSpawnMin = new Vector2(-31.1f, 6.2f);
    public enum modes
    {
        test,
        waves,
        zombieEverySecond,
        activationBased
        
    }
    public enum materialModes
    {
        setMats,
        Spawnrandomly
    }
    public Slider healthBar;
    private void Start()
    {
        
        if (mode == modes.waves)
        {
            waveNumber = 0;
            ActivateWaves();
        }
        else if (mode == modes.zombieEverySecond && !waitForSet)
        {
            StartCoroutine(startZombieEverySecond());
        }
        if (materialMode == materialModes.Spawnrandomly)
        {
            StartCoroutine(matsLoop());
        }
        if (mode == modes.activationBased && !waitForSet)
        {
            StartCoroutine(ActivationLoop());
        }

    }
    private void Update()
    {

        if(mode == modes.waves)
        {
            if (wavesTimer >= 0)
            {
                wavesTimer -= Time.deltaTime;
            }
            timer.text = wavesTimer.ToString();
            wavesNumber.text = waveNumber.ToString();
        }
        if (mode == modes.zombieEverySecond)
        {
            if (waitForSet && howManyZombiesEverySecond != 0 && timeBtwZombies != 0 && done == false)
            {
                done = true;
                StartCoroutine(startZombieEverySecond());
            }
            if (wavesTimer >= 0)
            {
                wavesTimer -= Time.deltaTime;
            }
            timer.text = wavesTimer.ToString();
            wavesNumber.text = waveNumber.ToString();
        }
        if(mode == modes.activationBased)
        {
            if (waitForSet && done2 == false && zombiesGatheredPerSecond != 0)
            {
                done2 = true;
                StartCoroutine(ActivationLoop());
            }
            timer.text = currentZombosGathered.ToString();
            if (Input.GetKeyDown(activationKey))
            {
                for (int i = 0; i < currentZombosGathered; i++)
                {
                    GameObject t_zombo = Instantiate(zomboPrefab);
                    t_zombo.transform.position = new Vector2(Random.Range(whereZombosSpawnMax.x, whereZombosSpawnMin.x), Random.Range(whereZombosSpawnMax.y, whereZombosSpawnMin.y));
                }
                waveNumber++;
                wavesNumber.text = waveNumber.ToString();
                currentZombosGathered = 0;
            }
        }
    }
    IEnumerator matsLoop()
    {
        yield return new WaitForSeconds(timeBtwMats);
        for (int i = 0; i < matsSpawnedAtOnce; i++)
        {
            GameObject t_mat = Instantiate(mat);
            t_mat.transform.position = new Vector2(Random.Range(whereMatsSpawnMax.x, whereMatsSpawnMin.x), Random.Range(whereMatsSpawnMax.y, whereMatsSpawnMin.y));
        }
        StartCoroutine(matsLoop());
    }
    IEnumerator ActivationLoop()
    {
        yield return new WaitForSeconds(1f);
        currentZombosGathered += zombiesGatheredPerSecond;
        StartCoroutine(ActivationLoop());
    }
    IEnumerator startZombieEverySecond()
    {
        wavesTimer = timeBtwZombies;
        waveNumber++;
        yield return new WaitForSeconds(timeBtwZombies);
        for (int i = 0; i < howManyZombiesEverySecond; i++)
        {
            GameObject t_zombo = Instantiate(zomboPrefab);
            t_zombo.transform.position = new Vector2(Random.Range(-20, -40), Random.Range(30, -30));
        }
        StartCoroutine(startZombieEverySecond());
    }
    public void ActivateWaves()
    {
         StartCoroutine(waveCount(wavestime[0], 0));
    }
    private IEnumerator waveCount(float time, int t_waveNumber)
    {
        wavesTimer = time;
        yield return new WaitForSeconds(time);
        waveNumber = t_waveNumber;
        for (int i = 0; i < waveszombos[waveNumber]; i++)
        {
            GameObject t_zombo = Instantiate(zomboPrefab);
            t_zombo.transform.position = new Vector2(Random.Range(-20, -40), Random.Range(30,-30));
        }
        t_waveNumber++;
        if(wavestime.Length > t_waveNumber)
        {
            StartCoroutine(waveCount(wavestime[t_waveNumber], t_waveNumber));
        }
        else if (doesReturnToMenu)
        {
            transition.Transition();
        }
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
        items = FindObjectsOfType<item>();
        instance = this;
    }
    public void quitApplication()
    {
        Application.Quit();
    }
    
}
