using System.Collections;
using UnityEngine;

public class Shower : MonoBehaviour
{
    public GameObject material, ray; //낙하물
    public Vector2 lu, rd;
    public float period;    //낙하물 생성 주기(s)
    public int count;       //낙하물 동시 생성량

    Vector2 pos;
    // Start i called before the first frame update
    void Start()
    {
        StartCoroutine(Fall());
        pos = new Vector2();
    }

    IEnumerator Fall()
    {
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                if (lu.x != rd.x) pos.x = Random.Range(lu.x, rd.x);
                if (lu.y != rd.y) pos.y = Random.Range(lu.y, rd.y);
                Instantiate(material, pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                Instantiate(ray, pos, Quaternion.Euler(0, 0, 270));
            }
            yield return new WaitForSeconds(period);
        }
    }
}
