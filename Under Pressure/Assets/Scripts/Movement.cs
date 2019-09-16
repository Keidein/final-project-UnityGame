using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float directionalSpeed = 6.0f;
    public float diagonalSpeed = 2.0f;
    public Rigidbody2D rb;

    public void MoveUp()
    {
        rb.MovePosition(rb.position + new Vector2(0, directionalSpeed) * Time.fixedDeltaTime);
    }

    public void MoveDown()
    {
        rb.MovePosition(rb.position + new Vector2(0, -directionalSpeed) * Time.fixedDeltaTime);
    }

    public void MoveLeft()
    {
        rb.MovePosition(rb.position + new Vector2(-directionalSpeed, 0) * Time.fixedDeltaTime);
    }

    public void MoveRight()
    {
        rb.MovePosition(rb.position + new Vector2(directionalSpeed, 0) * Time.fixedDeltaTime);
    }

    public void UpRight()
    {
        rb.MovePosition(rb.position + new Vector2(diagonalSpeed, diagonalSpeed) * Time.fixedDeltaTime);
    }
    public void UpLeft()
    {
        rb.MovePosition(rb.position + new Vector2(-diagonalSpeed, diagonalSpeed) * Time.fixedDeltaTime);
    }

    public void DownRight()
    {
        rb.MovePosition(rb.position + new Vector2(diagonalSpeed, -diagonalSpeed) * Time.fixedDeltaTime);
    }

    public void DownLeft()
    {
        rb.MovePosition(rb.position + new Vector2(-diagonalSpeed, -diagonalSpeed) * Time.fixedDeltaTime);
    }
}