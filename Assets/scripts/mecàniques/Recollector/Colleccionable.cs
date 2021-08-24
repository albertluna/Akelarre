using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleccionable : MonoBehaviour
{
    #region variables
    public int percentatge;
    public string color;
    public ColleccionableCreators parent;
    private float timer;
    [SerializeField]
    [Range(5, 30)]
    private float minTimer;
    [SerializeField]
    [Range(20, 120)]
    private float maxTimer;
    [SerializeField]
    private GameObject llum;
    [SerializeField]
    private MeshRenderer bola;
    #endregion

    private void Awake()
    {
        timer = Random.Range(minTimer, maxTimer);
    }

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            parent.EliminarColleccionable(this);
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
