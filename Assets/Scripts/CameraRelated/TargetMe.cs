using UnityEngine;

public class TargetMe : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera vc;
    void Start()
    {
        vc = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        Find();
    }
    void Find()
    {
        if (!vc.Follow) { 
            vc.Follow = FindObjectOfType<Player>().transform;
            Invoke("Find", 0.02f);
        }
        else return;
    }
}
