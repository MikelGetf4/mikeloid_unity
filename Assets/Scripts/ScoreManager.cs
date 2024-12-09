using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI score;
    public GameManager gameManager;

    void Update() //Comprueba constamente los puntos de la variable "puntos" del Game Manager y los enseña en pantalla
    {
        score.text = "Puntos: " + gameManager.puntos;
    }
}
