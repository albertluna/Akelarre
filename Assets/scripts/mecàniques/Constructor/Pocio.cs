﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocio : MonoBehaviour
{

    [SerializeField]
    private Colleccionable[] llista;

    [SerializeField]
    private int index;

    /// <summary>
    /// Funció per començar la llista del principi
    /// </summary>
    public void Comencar()
    {
        index = 0;
    }

    /// <summary>
    /// Funció per extreure la quantitat de col·leccionables de la llista
    /// </summary>
    /// <returns>Llargada de la llista</returns>
    public int getMaxValue()
    {
        return llista.Length;
    }

    /// <summary>
    /// Obtenir el valors de l'index
    /// </summary>
    /// <returns>valor de l'index</returns>
    public int getIndex()
    {
        return index;
    }

    /// <summary>
    /// Funició per determinar si s'ha posat un col·leccionable correcte a la llista
    /// </summary>
    /// <param name="colleccionable">Valor del col·leccionable d'entrada</param>
    /// <returns>true si és correcte, false si és fals</returns>
    public bool esColleccionableCorrecte(Colleccionable colleccionable)
    {
        return colleccionable.color.Equals(llista[index].color);
    }

    /// <summary>
    /// Funció per avançar en la llista
    /// </summary>
    public void Seguent()
    {
        index++;
    }

    /// <summary>
    /// Funció per determinar si ja s'ha acabat la llista, és a dir, la partida
    /// </summary>
    /// <returns>true si s'ha acabat, false si no</returns>
    public bool EsUltim()
    {
        return index == llista.Length;
    }
}
