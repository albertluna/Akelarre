using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ButtonRolController : MonoBehaviour
{
    public string rol;
    public Button boto;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        boto = GetComponent<Button>();
        isActive = true;
    }

    [PunRPC]


    // Update is called once per frame
    void Update()
    {
        
    }
}
