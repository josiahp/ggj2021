using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.U2D;

public class SeaweedAnim : MonoBehaviour
{
    public SpriteShapeController spriteShapeController;
    private float totalMove = 0f;
    public float speed = 0.1f;
    public float maxMovement = 0.2f;
    public int numSplines = 2;
    private int k = 1;
    public float tangentLength = 1.0f;

    private Vector3[] startPos;
    private Vector3[] endPos;

    // Update is called once per frame
    void Start() {
        Spline spline = spriteShapeController.spline;
        Vector3 posStart = spline.GetPosition(0);
        Vector3 posStop = spline.GetPosition(1);
        float yTop = posStop.y;
        float yBottom = posStart.y;
        float x = (posStart.x + posStop.x) / 2;
        float z = (posStart.z + posStop.z) / 2;
        float step = (yTop - yBottom) / numSplines;

        spline.RemovePointAt(1);
        try {
        for (int i = 1; i < numSplines; i++) {
            spline.InsertPointAt(i, new Vector3(x, yBottom + i * step, z));
        }
        spline.InsertPointAt(numSplines, posStop);
        /*for (int i = 0; i <= numSplines; i++) {
            Vector3 pos = spline.GetPosition(i);
            startPos[i] = new Vector3(pos.x - maxMovement, pos.y, pos.z);
            endPos[i] = new Vector3(pos.x + maxMovement, pos.y, pos.z);
            Debug.Log(startPos[i]);
            Debug.Log(endPos[i]);
        } */
        } catch (ArgumentException) {
            Debug.LogWarning("Attempted to insert too many points");
        } finally {
            spriteShapeController.RefreshSpriteShape();
        }
    }
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
            spline.RemovePointAt(i);
            Vector3 newPos = new Vector3(pos.x + k * speed, pos.y + k * speed/10, pos.z);
            spline.InsertPointAt(i, newPos);
            if (i == 1) {
                totalMove += Mathf.Abs(k*speed); 
                //Debug.Log(totalMove);    
            }
            //float rotation = (k > 0) ? 30f : -30f;
            k = -k;
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            /*spline.SetRightTangent(i, Vector3.down * tangentLength);
            spline.SetLeftTangent(i, Vector3.up * tangentLength); */
        }
        spriteShapeController.RefreshSpriteShape();
    }
}