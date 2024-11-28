using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int hits;
    public int contadorRotos;
    private PowerUp powerUp;

    public void Start()
    {
        RevisarColor();
    }

    private void RevisarColor() 
    {
        switch (hits) {
            case 1:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.5618103f, 0.9528302f, 0.6449021f, 0.33f);
                break;
            case 2:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.83f, 0.69f, 0.33f, 0.66f);
                break;
            case 3:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.84f, 0.33f, 0.36f, 1f);
                break;

        }
    }

    public void TakeDamage()
    {
        //Se le resta un golpe
        this.hits -= 1;
        RevisarColor();
        //Si los bloques son 0 o menos (por si hay bugs) destruimos el objeto
        if(this.hits <= 0)
        {
            contadorRotos += 1;
            Destroy(this.gameObject);
            this.powerUp = Generador.GeneradorPowerUp();
        }
    }
    
}
