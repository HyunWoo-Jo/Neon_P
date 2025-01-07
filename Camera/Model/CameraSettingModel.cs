using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettingModel
{
    private int distance;
    private int height;
    private float rotationDamping;
    private float heightDamping;

    public CameraSettingModel(int distance,int height, float rotationDamping, float heightDamping)
    {
        this.distance = distance;
        this.height = height;
        this.rotationDamping = rotationDamping;
        this.heightDamping = heightDamping;
    }

    public int getDistance() { return distance; }
    public int getHeight() { return height; }
    public float getRotDamping() { return rotationDamping; }
    public float getHeiDamping() { return heightDamping; }
}
