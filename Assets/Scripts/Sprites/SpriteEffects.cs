using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffects : MonoBehaviour{
    // Start is called before the first frame update

    public IEnumerator FadeTo(GameObject go, float aValue, float aTime)  {
        SpriteRenderer sr = go.transform.GetComponent<SpriteRenderer>();
        float alpha = sr.material.color.a;

        bool done = false;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            sr.material.color = newColor;
            yield return null;
        }
        if(alpha == 0) {
            Color oldColor = new Color(1, 1, 1, 1);
            sr.material.color = oldColor;
        }
        
    }

    public IEnumerator InvisFlash(GameObject go) {
        //Debug.Log("Running");
        
        SpriteRenderer sr = go.transform.GetComponent<SpriteRenderer>();
        Color whateverColor = new Color(1, 1, 1 , 0); //edit r,g,b and the alpha values to what you want
        for (var n = 0; n < 5; n++) {
            sr.material.color = Color.white;
            yield return new WaitForSeconds(.1f);
            sr.material.color = whateverColor;
            yield return new WaitForSeconds(.1f);
        }
        sr.material.color = Color.white;
 
    }

}
