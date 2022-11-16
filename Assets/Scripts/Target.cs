using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float scaleX;
    public float scaleY;
    public float scaleZ;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.transform.localScale = new Vector3(scaleX + 0.2f, scaleY + 0.2f, scaleZ);
        Invoke("ScaleDown", 0.2f);
    }

    private void ScaleDown()
    {
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
