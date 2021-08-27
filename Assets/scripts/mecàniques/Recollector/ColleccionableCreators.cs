using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColleccionableCreators : MonoBehaviour
{
    #region variables
    public bool estaOcupat;
    private RecollectorController recollector;
    private Colleccionable fill;
    //private int index;
    #endregion

    /// <summary>
    /// Funció que controla la instanciació dels col·leccionables
    /// </summary>
    /// <param name="collecionable">Referència al col·leccionable a instanciar</param>
    /// <param name="enable">true per la pantalla del recol·lector, false per la resta</param>
    /// <param name="isInvisible">true si els col·leccionables són invisibles</param>
    public void Instantiate(Colleccionable collecionable, bool enable, bool isInvisible)
    {
        fill = Instantiate(collecionable.gameObject, transform.position, Quaternion.identity, transform).GetComponent<Colleccionable>();
        fill.parent = this;
        fill.enabled = enable;
        estaOcupat = true;
        if (isInvisible && enable) fill.SetInivisible();
    }

    public void SetRecollector(RecollectorController recollector) { this.recollector = recollector; }

    public RecollectorController GetRecollector() { return recollector; }

    public Colleccionable GetFill() { return fill; }
}
