using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int StartScene = Animator.StringToHash("StartScene");

    public void Awake()
    {
        Time.timeScale = 1;
    }

    public void DoAnimation()
    {
        animator.SetTrigger(StartScene);
    }
    
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("New Scene");
    }
}
