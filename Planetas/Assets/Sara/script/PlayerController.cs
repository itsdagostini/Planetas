using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float vida = 100f;
    public float vidaMaxima = 100f;

    public void RecibirDanio(float cantidad)
    {
        vida -= cantidad;

        if (vida < 0)
        {
            vida = 0;
        }
    }

    public void Curar(float cantidad)
    {
        vida += cantidad;

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }
    }
}

