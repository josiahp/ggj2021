using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SeaweedAnimAllPoints : MonoBehaviour
{

    public SpriteShapeController spriteShapeController;
    private float totalMove = 0f;
    public float speed = 0.1f;
    public float maxMovement = 0.2f;
    private int k = 1;

    void Update() {
        SetSpline();
    }

    void SetSpline() {
        Spline spline = spriteShapeController.spline;
        if (totalMove >= maxMovement) {
                totalMove = 0f;
                k = -k;
            }
        for (int i = 1; i < spline.GetPointCount(); i++) {
            Vector3 pos = spline.GetPosition(i);
            //Debug.Log(pos);
            spline.RemovePointAt(i);
            spline.InsertPointAt(i, new Vector3(pos.x + k * speed, pos.y + k * speed/10, pos.z));
            if (i == 1) {
                totalMove += Mathf.Abs(k*speed); 
                //Debug.Log(totalMove);    
            }
            k = -k;
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            /*spline.SetRightTangent(i, rotation * Vector3.down * tangentLength);
            spline.SetLeftTangent(i, rotation * Vector3.up * tangentLength); */
        }
        spriteShapeController.RefreshSpriteShape();
    }
}
