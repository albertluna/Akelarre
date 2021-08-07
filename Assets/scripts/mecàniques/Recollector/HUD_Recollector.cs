using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Recollector : MonoBehaviour
{
    public GameObject[] vides;

    public void ActualitzarVides(int nVides)
    {
        for(int i = 0; i < vides.Length; i++)
        {
            if (i < nVides) vides[i].SetActive(true);
            else vides[i].SetActive(false);

        } 
    }

    public int NombreVides() { return vides.Length; }
    
}
