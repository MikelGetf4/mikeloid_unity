using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Codigo para instanciar el GameManager
    public int lives;                   //Vidas restantes del jugador
    public int maxLives = 3;            //El maximo de vidas a las que puede aspirar el jugador
    public int bloquesEnPantalla;       //El numero de bloques que hay en pantalla.
    public int puntos = 0;              //Los puntos que tiene el jugador

    public Image[] corazones;           //Un array que almacena los spirtes de los "corazones" par el recuento de vidas
    public Sprite vidaLlena;            //El sprite de la vida
    public Sprite vidaVacia;            //El spirte de una vida perdida

    public int Lives {  get { return lives; } }

    private void Awake()    //Metodo para que el GameManager no se destruya
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


        this.lives = 3; //Mantener las vidas a 3
    }

    public void Update()
    {

        PantallaLimpia();

        ManejarCorazonesPantalla();
    }

    //Metodo para añadir puntos globales
    public void AñadirPuntos()
    {
        puntos += 200;
        
    }


    //Metodo para quitar vidas a la pelota
    public void SubstractLive()
    {
        this.lives--;

        if(this.lives <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("Muerte");
            lives = 3;
        }
    }

    //Metodo para añadir vidas a la pelota
    public void AddLive()
    {
        if (this.lives >= maxLives) //Solo permite hasta un numero maximo de vidas
        {
            Debug.Log("Vidas maximas alcanzadas");
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
            SceneManager.LoadScene("Victoria");
        }
    }

    public void ManejarCorazonesPantalla() //Con este for comprobamos cuantas vidas tiene el jugador y cuantos sprites deben mostrarse en pantalla
    {
            for (int i = 0; i < corazones.Length; i++)  
            {
                if (i < lives)
                {
                    corazones[i].sprite = vidaLlena;
                }
                else
                {
                    corazones[i].sprite = vidaVacia;
                }
            }
    }


}
