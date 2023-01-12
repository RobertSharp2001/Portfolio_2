using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour{
    // Start is called before the first frame update
    private static ManagerScene m_Instance = null;
    public static ManagerScene Instance {
        get {
            if (m_Instance == null) {
                m_Instance = (ManagerScene)FindObjectOfType(typeof(ManagerScene));
            }
            return m_Instance;
        }
    }

    public void restartGame()   {
        SceneManager.LoadScene(1);
    }

    public void endGame(){
        SceneManager.LoadScene(2);
    }
}
