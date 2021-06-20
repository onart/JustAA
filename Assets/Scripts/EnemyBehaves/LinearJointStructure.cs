using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LinearJointStructure : MonoBehaviour
{
    JointBody[] joints;
    public (float, float) hilo; // 공통 각도제한(max, min 순서)
    
    void Start()
    {
        joints = GetComponentsInChildren<JointBody>();
        for(int i = 0; i+1 < joints.Length; i++)
        {
            joints[i].setTail(joints[i + 1]);
            joints[i].setAngleLim(hilo.Item2, hilo.Item1);
        }
    }
}
