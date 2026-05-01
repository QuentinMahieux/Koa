using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveDistance;
    public Rigidbody2D rb;
    
    public List<Vector3> positionsHistoirques =  new List<Vector3>();
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var vector3 = transform.position;
            vector3.y += moveDistance;
            rb.MovePosition(vector3);
            AddPosition(vector3);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var vector3 = transform.position;
            vector3.y -= moveDistance;
            rb.MovePosition(vector3);
            AddPosition(vector3);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var vector3 = transform.position;
            vector3.x += moveDistance;
            rb.MovePosition(vector3);
            AddPosition(vector3);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var vector3 = transform.position;
            vector3.x -= moveDistance;
            rb.MovePosition(vector3);
            AddPosition(vector3);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemovePosition(rb.position);
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.gameObject.GetComponent<ElementGame>().element.id == "V")
        {
            RemovePosition(other.gameObject.transform.position);
        }
        else
        {
            PlayerPalindrome.instance.AddPattern(other.gameObject.GetComponent<ElementGame>().element);
        }
    }

    void AddPosition(Vector3 position)
    {
        positionsHistoirques.Add(position);
    }

    void RemovePosition(Vector3 position)
    {
        if(positionsHistoirques.Count == 0) return;
        positionsHistoirques.Remove(position);
        transform.position = positionsHistoirques[^1];
    }
}
