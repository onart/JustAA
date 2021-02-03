using UnityEngine;

public class AudPlayer : MonoBehaviour  //애니메이터와 연계됨. 변수를 변경하는 즉시 오디오 소스를 1회 플레이
{
    AudioSource[] as_s;

    void Start()
    {
        as_s = GetComponents<AudioSource>();
    }

    public void PlayOne(int idx)
    {
        as_s[idx].Play();
    }

}
