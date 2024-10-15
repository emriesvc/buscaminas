using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pieza : MonoBehaviour
{
    public int x, y;
    public bool bomb;

    public Text scoreText;

    // Variable estática para el puntaje compartido
    private static int score;

    // Variable para rastrear si la bomba ya ha sido activada
    private bool bombActivated = false;

    private void Start()
    {                  
        score = Generador.gen.NumeroBombas; // Inicializamos el score al inicio
        scoreText.text = "Puntos: " + score.ToString();
    }

    private void OnMouseDown()
    {   
                         
        if (bomb)
        {
            // Si la bomba no ha sido activada
            if (!bombActivated)
            {
                // Cambia el color a rojo
                GetComponent<SpriteRenderer>().material.color = Color.red;

                // Restar puntos
                score--;
                scoreText.text = "Puntos: " + score.ToString();

                // Marca la bomba como activada
                bombActivated = true;
            }
        }
        else
        {
            // Si no hay bomba obtengo las bombas alrededor
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Generador.gen.GetBombsAround(x, y).ToString();
        }
        
        if (score.ToString() == "0")
            SceneManager.LoadScene("GameOver");
    }
}
