using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    public static Generador gen;
    public GameObject pieza;
    public GameObject[][] map;   
    public int ancho, alto, numeroBombas; //ancho, alto y nBombas

    // Variable pública para acceder al número de bombas
    public int NumeroBombas => numeroBombas;

    private void Awake()
    {
        gen = this; // Inicializa Generador antes que Pieza para recoger NumeroBombas en el Start de Pieza
    }

    // Start is called before the first frame update
    private void Start()
    {        
        gen = this;
        map = new GameObject[ancho][];
        for ( int k = 0; k < ancho; k++)
        {
            map[k] = new GameObject[alto];
        }        

        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                map[i][j] = Instantiate(pieza,new Vector2(i, j), Quaternion.identity);  
                map[i][j].GetComponent<Pieza>().x = i;
                map[i][j].GetComponent<Pieza>().y = j;   
            }
        }        

        Camera.main.transform.position = new Vector3((float)ancho / 2 - 0.5f, (float)alto / 2 - 0.5f, -10);
        //TODO posicion Puntos
        //transform.GetChild(0).position = ;
        SetBombs();
    }

    public void SetBombs()
    {
        for (int i = 0; i < numeroBombas; i++)
        {
            //map[Random.Range(0, ancho)][Random.Range(0, alto)].GetComponent<SpriteRenderer>().material.color = Color.red;
            int x = Random.Range(0, ancho); 
            int y = Random.Range(0, alto);
            if (!map[x][y].GetComponent<Pieza>().bomb)
                map[x][y].GetComponent<Pieza>().bomb = true;            
            else            
                i--;            
        }
    }
    
    public int GetBombsAround(int x, int y)
    {        
        int cont = 0;     
        for (int i = x-1; i < x+2; i++)
        {
            for (int j = y-1; j < y+2; j++)
            {
                if ((i>-1) && (j>-1) && (j<alto) && (i<ancho))
                {
                    if (!((i==x) && (j==y)) && map[i][j].GetComponent<Pieza>().bomb) 
                    {
                        cont++;
                    }
                }
            }
        } 

        //!! Forma solo con if
        /*     
        if (x > 0 && y < alto - 1 && map[x - 1][y + 1].GetComponent<Pieza>().bomb)        
            cont++; 
        if (y < alto - 1 && map[x][y + 1].GetComponent<Pieza>().bomb)        
            cont++; 
        if (x < ancho - 1 && y < alto - 1 && map[x + 1][y + 1].GetComponent<Pieza>().bomb)        
            cont++;
        if (x > 0 && map[x - 1][y].GetComponent<Pieza>().bomb)        
            cont++;
        if (x < ancho - 1 && map[x + 1][y].GetComponent<Pieza>().bomb)        
            cont++;
        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Pieza>().bomb)        
            cont++;
        if (y > 0 && map[x][y - 1].GetComponent<Pieza>().bomb)        
            cont++;
        if (x < ancho - 1 && y > 0 && map[x + 1][y - 1].GetComponent<Pieza>().bomb)        
            cont++;
        */ 
        return cont;      
    }
}
