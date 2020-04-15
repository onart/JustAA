using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMe : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera vc;
    void Start()
    {
        vc = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        vc.Follow = FindObjectOfType<Player>().transform;
    }
}
