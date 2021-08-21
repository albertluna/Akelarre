using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColleccionableCreators : MonoBehaviour
{
    #region variables
    public bool estaOcupat;
    private int index;
    #endregion

    /// <summary>
    /// Funció que controla la instanciació dels col·leccionables
    /// </summary>
    /// <param name="collecionable">Referència al col·leccionable a instanciar</param>
    /// <param name="enable">true per la pantalla del recol·lector, false per la resta</param>
    /// <param name="isInvisible">true si els col·leccionables són invisibles</param>
    public void Instantiate(Colleccionable collecionable, bool enable, bool isInvisible)
    {
        Colleccionable nouColleccionable = Instantiate(collecionable, transform.position, Quaternion.identity, transform);
        nouColleccionable.parent = this;
        nouColleccionable.enabled = enable;
        estaOcupat = true;
        if (isInvisible && enable) nouColleccionable.setInivisible();
    }

    /// <summary>
    /// Funció per gestionar quan el recol·lector ha agafat el col·leccionable i s'ha d'eliminar
    /// </summary>
    /// <param name="collecionable">Referència al col·leccionable a eliminar</param>
    public void destruirColleccionable(Colleccionable collecionable)
    {
        RecollectorController rc = FindObjectOfType<RecollectorController>();
        int index = rc.indexColleccionable(collecionable.gameObject);
        rc.deleteColleccionable(index);
    }

    public void SetIndex(int index) { this.index = index; }
}
