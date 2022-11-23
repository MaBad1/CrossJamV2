using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shapes : MonoBehaviour
{
    private Touch touch;
    public float speedModifier;
    public float speedLaunch;
    public float distanceMin = 100;
    public Rigidbody rb;
    //Vector2 defaultScale;
    Vector2 startTapAngle;

    public float scaleX;
    public float scaleY;
    public float scaleZ;

    void Start()
    {
        //defaultScale = transform.localScale;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (FindObjectOfType<GameManager>().gameState == GameManager.State.InGameEasy)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);


                if (touch.phase == TouchPhase.Began)
                {
                   startTapAngle = new Vector2(
                      touch.position.x,
                      touch.position.y);

                    gameObject.transform.localScale = new Vector3(scaleX - 0.2f, scaleY - 0.2f, scaleZ);

                    //DOTween.Kill(gameObject);
                    //transform.DOScale(defaultScale * 1.1f, 0.2f).SetLink(gameObject).SetEase(Ease.OutExpo);

                } else if (touch.phase == TouchPhase.Moved)
                {
                    
                } else  if (touch.phase == TouchPhase.Ended)
                {
                    Vector2 tapAngle = new Vector2(
                        touch.position.x,
                        touch.position.y);

                    Debug.Log("Distance: " + Vector2.Distance(startTapAngle, tapAngle));
                    if (Vector2.Distance(startTapAngle, tapAngle) < distanceMin) {
                        return;
                    }

                    float angleFinal = Angle((tapAngle - startTapAngle).normalized);
                    Debug.Log(angleFinal);


                    if (angleFinal > 45 && angleFinal < 135)
                    {
                        // Droite
                        rb.velocity = speedLaunch * new Vector3(1, 0, 0);
                        /*DOTween.Kill(gameObject);
                        transform.DOScaleX(defaultScale.x * 1.1f, 0.2f).SetLink(gameObject).SetEase(Ease.OutExpo);
                        transform.DOScaleY(defaultScale.y * 0.9f, 0.2f).SetLink(gameObject).SetEase(Ease.OutExpo);
                        */
                        gameObject.transform.localScale = new Vector3(scaleX + 0.2f, scaleY - 0.3f, scaleZ);
                    }
                    if (angleFinal > 225 && angleFinal < 315)
                    {
                        // Gauche
                        rb.velocity = speedLaunch * new Vector3(-1, 0, 0);
                        gameObject.transform.localScale = new Vector3(scaleX + 0.2f, scaleY - 0.3f, scaleZ);

                    }

                    if (angleFinal >= 315 || angleFinal <= 45)
                    {
                        // Haut
                        rb.velocity = speedLaunch * new Vector3(0, 2, 0);
                        gameObject.transform.localScale = new Vector3(scaleX - 0.3f, scaleY + 0.2f, scaleZ);

                    }
                    if (angleFinal > 135 && angleFinal < 225)
                    {
                        // Bas
                        rb.velocity = speedLaunch * new Vector3(0, -2, 0);
                        gameObject.transform.localScale = new Vector3(scaleX - 0.3f, scaleY + 0.2f, scaleZ);

                    }
                }

            }
        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision: " + collision.gameObject.name);
        rb.velocity = new Vector3(0,0,0);
        enabled = false;
        

        if ( GetComponent<MeshRenderer>().material.name == collision.gameObject.GetComponent<MeshRenderer>().material.name)
        {
            FindObjectOfType<GameManager>().AddScore();
        }
        
        else if (GetComponent<MeshRenderer>().material.name != collision.gameObject.GetComponent<MeshRenderer>().material.name)
        {
            FindObjectOfType<GameManager>().RemoveScore();
        }

        FindObjectOfType<GameManager>().RandomColorEasy();
        Invoke("ResetPos", 0.1f);
    }

    private void ResetPos()
    { 
        transform.position = new Vector3(0, 1, 0);
        gameObject.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        enabled = true;
    }

    public static float Angle(Vector2 vector2)
    {
        if (vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
        }
    }

}
