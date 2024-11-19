using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lives;

    public int Lives {  get { return lives; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        this.lives = 3;
    }

    public void SubstractLive()
    {
        this.lives--;

        if(this.lives < 0)
        {
            Debug.Log("Game Over");
        }
    }
}
