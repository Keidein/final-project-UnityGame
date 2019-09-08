using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Movement movement;
    void FixedUpdate()
    {
        // Fixed directional input.
        if (Input.GetKey("w"))
        {
            movement.MoveUp();
        }

        if (Input.GetKey("s"))
        {
            movement.MoveDown();
        }

        if (Input.GetKey("d"))
        {
            movement.MoveRight();
        }

        if (Input.GetKey("a"))
        {
            movement.MoveLeft();
        }

        // Diagonal movement input.
        if (Input.GetKey("w") && Input.GetKey("d"))
        {
            movement.UpRight();
        }

        if (Input.GetKey("w") && Input.GetKey("a"))
        {
            movement.UpLeft();
        }

        if (Input.GetKey("s") && Input.GetKey("d"))
        {
            movement.DownRight();
        }

        if (Input.GetKey("s") && Input.GetKey("a"))
        {
            movement.DownLeft();
        }
    }
}
