using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombo : MonoBehaviour
{
    public float raduis;
    public LayerMask player;
    public LayerMask zomboCanBreak;
    public float damage;
    public float attackStun;
    public bool isStunned;
    public TextMesh StunText;
    public float offsetlook;
    public float stunTimer;
    private Rigidbody2D rb;
    public float speed;
    public float health;
    public float buildingDamage;
    public float buildingAttackStun;
    public Transform placeInWichZombosAttack;
    private IEnumerator lastStun;
    private bool first = true;
    public TransitionToNewScene transition;
    public bool WhenKillTransition;
    [System.NonSerialized]
    public float speedSave;
    public bool stopped = false;
    public float savedStun;
    public float currentStun;
    private BoxCollider2D boxCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        speedSave = speed;
    }
    private void FixedUpdate()
    {
        if (!isStunned)
        {
            transform.right = manager.instance.player.transform.position - transform.position;
            rb.velocity = new Vector2(manager.instance.player.transform.position.x * speed - transform.position.x * speed,
                manager.instance.player.transform.position.y * speed - transform.position.y * speed);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    public void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            GetComponent<Animator>().SetTrigger("Death");
            Destroy(gameObject, 2f);
            GetComponent<BoxCollider2D>().enabled = false;
            rb.Sleep();
            if (WhenKillTransition)
            {
                transition.Transition();
            }
            enabled = false;
        }
        GetComponent<Animator>().SetTrigger("Hurt");
    }
    void Update()
    {
        if (!isStunned)
        {
            Collider2D hitColliderPlayer = Physics2D.OverlapCircle(placeInWichZombosAttack.position, raduis, player);
            Collider2D hitCollidersBreakAbles = Physics2D.OverlapCircle(placeInWichZombosAttack.position, raduis, zomboCanBreak);
            StunText.text = "";
            if (hitColliderPlayer != null)
            {
                GetComponent<Animator>().SetTrigger("Attack");
                PlayerMoveTopDown t_player = hitColliderPlayer.GetComponent<PlayerMoveTopDown>();
                if (t_player.isActiveAndEnabled)
                {
                    hitColliderPlayer.GetComponent<PlayerMoveTopDown>().damage(damage);
                }
                //hitColliderPlayer.GetComponent<Rigidbody2D>().AddForce(Vector3.forward * 10);
                Stun(attackStun);
            }
            else if(hitCollidersBreakAbles != null)
            {
                if (hitCollidersBreakAbles.GetComponent<item>())
                {
                    GetComponent<Animator>().SetTrigger("Attack");
                    hitCollidersBreakAbles.GetComponent<item>().Damage(buildingDamage);
                    Stun(buildingAttackStun);
                }
            }
        }
        else
        {
            if(stunTimer >= 0)
            {
                stunTimer -= Time.deltaTime;
            }
            else if(stunTimer * 1 == stunTimer)
            {
                stunTimer = 0;
            }
            int floorTimer = Mathf.FloorToInt(stunTimer);
            StunText.text = floorTimer.ToString();
        }
    }
    public void Stun(float stunTime)
    {
        if (first)
        {
            first = false;
        }
        else if (lastStun != null)
        {
            StopCoroutine(lastStun);
        }
        lastStun = stuncode(stunTime);
        StartCoroutine(lastStun);
    }
    public IEnumerator stuncode(float stunTime)
    {
        currentStun = stunTime;
        isStunned = true;
        stunTimer = stunTime;
        //boxCollider.isTrigger = false;
        //manager.instance.player.GetComponent<PlayerMoveTopDown>().setVelocity = false;
        yield return new WaitForSeconds(stunTime);
        //boxCollider.isTrigger = true;
        //manager.instance.player.GetComponent<PlayerMoveTopDown>().setVelocity = true;
        lastStun = null;
        isStunned = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(placeInWichZombosAttack.position, raduis);
    }
}
