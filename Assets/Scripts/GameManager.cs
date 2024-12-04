using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lives;
    public int maxLives = 3;
    public int bloquesEnPantalla;
    public int puntos = 0;

    public Image[] corazones;
    public Sprite vidaLlena;
    public Sprite vidaVacia;

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

    public void Update()
    {
        PantallaLimpia();

        for (int i = 0; i < corazones.Length; i++)
        {
            if(i < lives)
            {
                corazones[i].sprite = vidaLlena;
            }
            else
            {
                corazones[i].sprite = vidaVacia;
            }
        }
    }

    public void AñadirPuntos()
    {
        puntos += 200;
        
    }


    public void SubstractLive()
    {
        this.lives--;

        if(this.lives < 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("Muerte");
        }
    }

    public void AddLive()
    {
        if (this.lives >= 3)
        {
            Debug.Log("Amigo andas lleno");
        }
        else
        {
            this.lives++;
            Debug.Log("Sumas una vida, ahora tienes " + lives);
        }
    }


    public void PantallaLimpia() //Aqui comprobamos que el numero de bloques en pantalla sea 0 menos para acabar el juego
    {
        //Asignamos a "bloquesEnPantalla" los bloques que hay en la escena
        bloquesEnPantalla = GameObject.FindGameObjectsWithTag("Bloque").Length;
        
        //Comprobamos que escena esta cargada y hacemos que avance a la siguiente
        if (this.bloquesEnPantalla <= 0 && SceneManager.GetSceneByName("Nivel1").isLoaded)
        {
            Debug.Log("Nivel 1 superado");
            SceneManager.LoadScene("Nivel2");
        }

        if (this.bloquesEnPantalla <= 0 && SceneManager.GetSceneByName("Nivel2").isLoaded)
        {
            Debug.Log("Nivel 2 superado");
            SceneManager.LoadScene("Nivel3");
        }

        if (this.bloquesEnPantalla <= 0 && SceneManager.GetSceneByName("Nivel3").isLoaded)
        {
            Debug.Log("Nivel 3 superado");
            SceneManager.LoadScene("Victoria");
        }
    }
}
