using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : PowerUp
{
    public static PowerUp GeneradorPowerUp()
    {
        int random = Random.Range(1,6);
        switch (random)
        {
            case 1:
                GameManager.Instance.AddLive();
                Debug.Log("Has ganado una vida");
                Debug.Log("Ahora tienes " + GameManager.Instance.lives);
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;

        }
        return null;
    }
    

    public override void Ejecutar()
    {
        
    }
}
