using UnityEngine;

public class MenuPager : MonoBehaviour
{
    // Start is called before the first frame update
    enum State { CTRL, TOGGLE, };
    public GameObject[] pages;
    State state;

    void Start()
    {
        state = State.CTRL;
    }

    public void setPage(int i)
    {
        pages[(int)state].SetActive(false);
        state = (State)i;
        pages[(int)state].SetActive(true);
    }
}
