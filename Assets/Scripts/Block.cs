using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int hits = 1;

    public void TakeDamage()
    {
        //Se le resta un golpe
        this.hits -= 1;
        //Si los bloques son 0 o menos (Por si hay bugs) destruimos el objeto
        if(this.hits <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
}
