using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Python : LinearJointStructure
{
    private void Update()
    {
        joints[0].rb2d.MovePosition(joints[0].rb2d.position + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))/10);
    }
}
