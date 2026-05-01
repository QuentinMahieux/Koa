using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public List<Historique> historiques = new List<Historique>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemovePosition();
            PlayerPalindrome.instance.RemovePattern();

            GameManager.instance.GameOver(!PlayerPalindrome.instance.DetectPalindrome(PlayerPalindrome.instance.patterns));
        }
        
        if(GameManager.instance.isGameOver) return;
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var vector3 = transform.position;
            vector3.y += LevelGenerator.instance.generatorData.marge;
            rb.MovePosition(vector3);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var vector3 = transform.position;
            vector3.y -= LevelGenerator.instance.generatorData.marge;
            rb.MovePosition(vector3);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var vector3 = transform.position;
            vector3.x += LevelGenerator.instance.generatorData.marge;
            rb.MovePosition(vector3);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var vector3 = transform.position;
            vector3.x -= LevelGenerator.instance.generatorData.marge;
            rb.MovePosition(vector3);
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        ElementGame elementGame = other.gameObject.GetComponent<ElementGame>();
        
        if(!elementGame) return;
        
        if(!elementGame.actualElement.isObstacle)
        {
            PlayerPalindrome.instance.AddPattern(elementGame.actualElement);
            AddPosition(rb.position, elementGame);
        }
        else
        {
            AddPosition(rb.position, elementGame);
            RemovePosition();
        }
        
        if(!elementGame.actualElement.isIndestructible) elementGame.Remove();
    }

    void AddPosition(Vector3 position, ElementGame element)
    {
        historiques.Add(new Historique());
        historiques[^1].lastElement = element;
        historiques[^1].lastPosition = position;
    }

    void RemovePosition()
    {
        if(historiques.Count <= 0) return;
        
        historiques[^1].lastElement.Back();
        
        historiques.RemoveAt(historiques.Count - 1);
        transform.position = historiques[^1].lastPosition;
    }
}

[System.Serializable]
public class Historique
{
    public ElementGame lastElement;
    public Vector3 lastPosition;

}