using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInput : MonoBehaviour
{

    public static Action <int> Muve;

    public static Action <int> Steer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void muve(int value)
    {
        Muve?.Invoke(value);
    }

    public void steer(int value)
    {
        Steer?.Invoke(value);
    }

    public void load(int level)
    {
        SceneManager.LoadScene(level);
        
    }
}
