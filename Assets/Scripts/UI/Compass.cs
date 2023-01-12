using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour{
    // Start is called before the first frame update

    // Update is called once per frame
    public GameObject pointTo;
    private Vector3 targetPosition;
    private RectTransform pointerRect;

    public void Awake(){
        targetPosition = pointTo.transform.position;

        pointerRect = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    public void Update() {
        if (pointTo != null) {
            targetPosition = pointTo.transform.position;
            Vector3 toPos = targetPosition;
            Vector3 fromPos = Camera.main.transform.position;

            fromPos.z = 0.0f;
            Vector3 dir = (toPos - fromPos).normalized;
            float angle = GetAngleFromVectorFloat(dir) - 90;

            pointerRect.localEulerAngles = new Vector3(0, 0, angle);
        }

        
    }


    public void changeTarget(GameObject go) {
        pointTo = go;
    }
    public float GetAngleFromVectorFloat(Vector3 dir){
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
