using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveTopDown : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Transform parent;
    public float maxHealth;
    private float health;
    public bool setVelocity = true;
    public bool wayFlipped;
    public GameObject GFX;
    private Vector2 startScale;
    private Animator animator;
    public GameObject arm;
    private Vector2 startScaleArm;
    public TransitionToNewScene transition;
    public float permamentSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;
        startScale = GFX.transform.localScale;
        startScaleArm = arm.transform.localScale;
        setHealthBar();
        permamentSpeed = speed;
    }
    public void setHealthBar()
    {
        manager.instance.healthBar.maxValue = maxHealth;
        manager.instance.healthBar.value = health;
    }
    // Update is called once per frame
    void Update()
    {
        //Movement
        if (setVelocity)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        }
        if(Input.GetAxisRaw("Horizontal") <= 0.5 && Input.GetAxisRaw("Horizontal") != 0)
        {
            GFX.transform.localScale = new Vector2(-startScale.x, startScale.y);
            arm.transform.localScale = new Vector2(-startScaleArm.x, startScaleArm.y);
        }
        else if (Input.GetAxisRaw("Horizontal") >= -0.5 && Input.GetAxisRaw("Horizontal") != 0)
        {
            GFX.transform.localScale = startScale;
            arm.transform.localScale = startScaleArm;
        }
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
        //Rotation
        RotateTwordsMouse();
        //updateFlip();
    }
    public void damage(float damage)
    {
        health -= damage;
        animator.SetTrigger("PlayerHurt");
        if(health <= 0)
        {
            setVelocity = false;
            GetComponent<PlayerInventory>().enabled = false;
            animator.SetTrigger("Death");
            rb.bodyType = RigidbodyType2D.Static;
            transition.Transition();
            Destroy(parent.gameObject);
            enabled = false;
        }
        setHealthBar();
    }
    /*public void updateFlip()
    {
        if ()
        {
            wayFlipped = true;
        }
        else
        {
            wayFlipped = false;
        }
    }*/
    public void RotateTwordsMouse()
    {
        if(parent != null)
        {
            Vector3 t_mousePos = Input.mousePosition;
            Vector3 t_screenSpaceTransform = Camera.main.WorldToScreenPoint(parent.position);
            t_mousePos.x -= t_screenSpaceTransform.x;
            t_mousePos.y -= t_screenSpaceTransform.y;
            float t_angle = Mathf.Atan2(t_mousePos.y, t_mousePos.x) * Mathf.Rad2Deg;
            parent.rotation = Quaternion.Euler(0, 0, t_angle);
        }
    }
}
