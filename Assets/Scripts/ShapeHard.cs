using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeHard : MonoBehaviour
{
    private Touch touch;
    public float speedModifier;
    public int life = 1;

    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount > 0 && FindObjectOfType<GameManager>().gameState == GameManager.State.InGameHard)
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
            life -= 1;
        }

        FindObjectOfType<GameManager>().RandomColorHard();
        Invoke("ResetPos", 0.2f);
    }

    private void ResetPos()
    {
        enabled = true;
        transform.position = new Vector3(0, 1, 0);
    }
}
