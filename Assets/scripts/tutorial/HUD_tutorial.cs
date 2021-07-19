using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HUD_tutorial : MonoBehaviour
{
    private Tutorial tutorial;

    public void setTutorial(Tutorial tutorial) {
        this.tutorial = tutorial;
    }

    public void OnComencar()
    {
        tutorial.Comencar(); 
    }
}
