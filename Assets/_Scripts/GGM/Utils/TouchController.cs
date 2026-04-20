using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public Vector2 postPosition;
    public float velocity = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // ESSE SERIA O INPUT PARA O CELULAR, MAS COMO ESTOU TESTANDO NO PC, VOU USAR O INPUT DO MOUSE
        // if(Input.GetTouch(0).phase == TouchPhase.Moved)
        // {
        //     Move(Input.GetTouch(0).deltaPosition.x);
        // }
        if(Input.GetMouseButton(0))
        {
           Move(Input.mousePosition.x - postPosition.x);
        }
        postPosition = Input.mousePosition;
    }

    public void Move (float speed) 
    {
        transform.position += Vector3.right * speed * Time.deltaTime * velocity;
    }
}
