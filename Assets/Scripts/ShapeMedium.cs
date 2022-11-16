using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMedium : MonoBehaviour
{
    private Touch touch;
    public float speedModifier;
    public float speedLaunch;
    private Vector3 target1 = new Vector3(1, 7, 0);
    private Vector3 target2 = new Vector3(1, -6, 0);
    private Vector3 target3 = new Vector3(-2, 1, 0);
    private Vector3 target4 = new Vector3(2, 1, 0);
    public int lives = 3;

    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount > 0 && FindObjectOfType<GameManager>().gameState == GameManager.State.InGameMedium)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedModifier,
                    transform.position.y + touch.deltaPosition.y * speedModifier,
                    transform.position.z);
            }

        }

        if (transform.position.x <= -0.8)
        {
            Left();
        }

        if (transform.position.x >= 0.8)
        {
            Right();
        }

        if (transform.position.y <= 0)
        {
            Down();
        }

        if (transform.position.y >= 2)
        {
            Up();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision: " + collision.gameObject.name);
        enabled = false;

        if (GetComponent<MeshRenderer>().material.name == collision.gameObject.GetComponent<MeshRenderer>().material.name)
        {
            FindObjectOfType<GameManager>().AddScore();
        }

        else if (GetComponent<MeshRenderer>().material.name != collision.gameObject.GetComponent<MeshRenderer>().material.name)
        {
            FindObjectOfType<GameManager>().RemoveScore();
            lives -= 1;
        }

        FindObjectOfType<GameManager>().RandomColorMedium();
        Invoke("ResetPos", 0.2f);
    }

    private void ResetPos()
    {
        enabled = true;
        transform.position = new Vector3(0, 1, 0);
    }

    private void Left()
    {
        transform.position = Vector3.MoveTowards(transform.position, target3, Time.deltaTime * speedLaunch);
    }

    private void Right()
    {
        transform.position = Vector3.MoveTowards(transform.position, target4, Time.deltaTime * speedLaunch);
    }

    private void Down()
    {
        transform.position = Vector3.MoveTowards(transform.position, target2, Time.deltaTime * speedLaunch);
    }

    private void Up()
    {
        transform.position = Vector3.MoveTowards(transform.position, target1, Time.deltaTime * speedLaunch);
    }
}
