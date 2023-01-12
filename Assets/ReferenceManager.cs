using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour{

    public GameObject dronePool;
    public GameObject followPlayer;

    private static ReferenceManager m_Instance = null;
    public static ReferenceManager Instance{
        get{
            if (m_Instance == null){
                m_Instance = (ReferenceManager)FindObjectOfType(typeof(ReferenceManager));
            }
            return m_Instance;
        }
    }
}
