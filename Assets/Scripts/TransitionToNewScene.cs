using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToNewScene : MonoBehaviour
{
    public float timeToTransition;
    public float timeBeforeDarkTransiton;
    public string scene;
    public Animator animator;
    public bool usesCollider;
    public bool usesDarkTransition;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (usesCollider)
        {
            Transition();
        }
    }

    public void Quit()
    {
        StartCoroutine(quitCode());
    }
    private IEnumerator quitCode()
    {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
    void Update()
    {
        
    }
    public string returnSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void Transition()
    {
        StartCoroutine(TransitionCode());
    }
    private IEnumerator TransitionCode()
    {
        yield return new WaitForSeconds(timeBeforeDarkTransiton);
        StartCoroutine(transition2());
    }
    private IEnumerator transition2()
    {
        if (usesDarkTransition)
        {
            animator.SetTrigger("Trigger");
        }
        yield return new WaitForSeconds(timeToTransition);
        SceneManager.LoadScene(scene);
    }
}
