using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int hits;                   //Golpes necesarios para romper el bloque
    public int contadorRotos;           //Bloques que se han roto
    private PowerUp powerUp;            //PowerUp
    private GameManager gameManager;    //GameManager
     

    public void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        RevisarColor();
    }

    private void RevisarColor() // Logica para elegir el color de los bloques dependiendo de los golpes que les queden
    {
        switch (hits) {
            case 1:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.5618103f, 0.9528302f, 0.6449021f, 0.33f); //Verde
                break;
            case 2:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.83f, 0.69f, 0.33f, 0.66f); //Amarillo
                break;
            case 3:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.84f, 0.33f, 0.36f, 1f); //Rojo
                break;

        }
    }

    public void TakeDamage() //Logica para quitarle golpes a los bloques
    {
        //Se le resta un golpe
        this.hits -= 1;
        RevisarColor();
        //Si los bloques son 0 o menos (por si hay bugs) destruimos el objeto
        if(this.hits <= 0)
        {
            contadorRotos += 1; //Suma un bloque mas roto
            this.powerUp = GeneradorPowerUp(); //Crea la posibilidad de generar un PowerUP
            Destroy(this.gameObject); //Destruye el objeto

            if (powerUp != null) //Si existe un PowerUp, ejecutalo
            {
                powerUp.Ejecutar();
                
            }
        }
    }

    private PowerUp GeneradorPowerUp() //Logica para generar los PowerUps cuando se rompe un bloque
    {
        PowerUp powerUpADevolver = null;

        int random = Random.Range(1, 7); //Crea un numero aleatorio
        switch (random)
        {
            case 1:
                powerUpADevolver = new PaddleGrande();
                break;
            case 2:
                powerUpADevolver = new PaddleGrande();
                break;

            case 3:
                GameManager.Instance.AddLive();
                break;

            case 4:
                GameManager.Instance.AddLive();
                break;

            case 6:
                powerUpADevolver = new PaddleRapido();
                break;
            case 7:
                powerUpADevolver = new PaddleRapido();
                break;
        }

        return powerUpADevolver;
    }
}
