using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemGFX : MonoBehaviour
{
    public float damage;
    public float stunTime;
    public int uses;
    public bool showDurability;
    public float itemHealth;
    public KeyCode key;
    public useAbleTypes Type;
    public float heal;
    public float boostSpeed;
    public bool temperarySpeed;
    public float temporaryTime;
    private float speedFromBefore;
    private bool first;
    private IEnumerator lastTemporary;
    public float timeStop;
    private bool Dood;
    public enum useAbleTypes
    {
        Nothing,
        Health,
        SpeedIncrease
        //StopingTime
    }
    private void Start()
    {
        speedFromBefore = manager.instance.player.GetComponent<PlayerMoveTopDown>().speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Zombo>())
        {
            collision.collider.GetComponent<Zombo>().Damage(damage);
            if (stunTime != 0)
            {
                collision.collider.GetComponent<Zombo>().Stun(stunTime);
            }
            uses--;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(uses <= 0)
        {
            manager.instance.player.GetComponent<PlayerInventory>().destroyItem(manager.instance.player.GetComponent<PlayerInventory>().itemSlot);
        }

        if(Type == useAbleTypes.Health && Input.GetKeyDown(key))
        {
            manager.instance.player.GetComponent<PlayerMoveTopDown>().damage(-heal);
            uses--;
        }
        else if(Type == useAbleTypes.SpeedIncrease && Input.GetKeyDown(key) && !temperarySpeed)
        {
             manager.instance.player.GetComponent<PlayerMoveTopDown>().permamentSpeed += boostSpeed;
            uses--;
        }
        else if(Type == useAbleTypes.SpeedIncrease && Input.GetKeyDown(key) && temperarySpeed)
        {
            Temporary(temporaryTime);
            uses--;
        }
        /*else if(Type == useAbleTypes.StopingTime && Input.GetKeyDown(key))
        {
            StopTime(timeStop);
            uses--;
        }*/
        if (!Dood)
        {
            manager.instance.player.GetComponent<PlayerMoveTopDown>().speed = manager.instance.player.GetComponent<PlayerMoveTopDown>().permamentSpeed;
        }
    }
    public void StopTime(float Time)
    {
        if (first)
        {
            first = false;
        }
        else if (lastTemporary != null)
        {
            StopCoroutine(lastTemporary);
        }
        lastTemporary = Temporarycode(Time);
        StartCoroutine(StopTimecode(Time));
    }
    public IEnumerator StopTimecode(float Time)
    {

        Zombo[] zombos = FindObjectsOfType<Zombo>();
        for (int i = 0; i < zombos.Length; i++)
        {
            if (zombos[i].isStunned)
            {
                zombos[i].savedStun = zombos[i].currentStun;
            }
            else
            {
                zombos[i].savedStun = 0;
            }
            zombos[i].enabled = false;
            zombos[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        //manager.instance.player.GetComponent<PlayerMoveTopDown>().setVelocity = false;
        yield return new WaitForSeconds(Time);
        for (int i = 0; i < zombos.Length; i++)
        {
            zombos[i].enabled = true;
            zombos[i].stuncode(zombos[i].savedStun);
        }
        //manager.instance.player.GetComponent<PlayerMoveTopDown>().setVelocity = true;
        lastTemporary = null;
    }
    public void Temporary(float TemporaryTime)
    {
        /*if (first)
        {
            first = false;
        }
        else if (lastTemporary != null)
        {
            StopCoroutine(lastTemporary);
            Dood = false;
        }*/
        if (!Dood)
        {
            lastTemporary = Temporarycode(TemporaryTime);
            StartCoroutine(lastTemporary);
        }
    }
    public IEnumerator Temporarycode(float TemporaryTime)
    {
        Dood = true;
        manager.instance.player.GetComponent<PlayerMoveTopDown>().speed += boostSpeed;
        //manager.instance.player.GetComponent<PlayerMoveTopDown>().setVelocity = false;
        yield return new WaitForSeconds(TemporaryTime);
        Dood = false;
        manager.instance.player.GetComponent<PlayerMoveTopDown>().speed = manager.instance.player.GetComponent<PlayerMoveTopDown>().permamentSpeed;
        //manager.instance.player.GetComponent<PlayerMoveTopDown>().setVelocity = true;
        lastTemporary = null;
    }
}
