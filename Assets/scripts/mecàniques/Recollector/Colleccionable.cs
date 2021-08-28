using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleccionable : MonoBehaviour
{
    #region variables
    public int percentatge;
    public string color;
    public ColleccionableCreators parent;
    public float tempsVida;
    /*
    [SerializeField]
    [Range(5, 30)]
    private float minTimer;
    [SerializeField]
    [Range(20, 120)]
    private float maxTimer;*/
    [SerializeField]
    private GameObject llum;
    [SerializeField]
    private MeshRenderer bola;
    #endregion

    private void Update()
    {
        if (parent.GetRecollector().photonView.IsMine)
        {
            if (tempsVida >= 0)
            {
                tempsVida -= Time.deltaTime;
            }
            else
            {
                parent.GetRecollector().EliminarColleccionable(this);
            }
        }
    }

    /// <summary>
    /// S'elimina la llum i el render de la bola quan ha de ser invisible
    /// </summary>
    public void SetInivisible()
    {
        Destroy(llum);
        bola.enabled = false;
    }
}
