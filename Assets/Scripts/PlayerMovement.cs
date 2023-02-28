using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject[] Obstacles;
    private GameObject[] ObjToPush;
    private bool ReadyToMove;
    private bool directionPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        ObjToPush = GameObject.FindGameObjectsWithTag("ObjToPush");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveinput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveinput.Normalize();

        if (moveinput.sqrMagnitude > 0.5)
        {
             if (ReadyToMove && !directionPressed)
             {
                directionPressed = true;
                Move(moveinput);
             }
            else
            {
                ReadyToMove = true;
            }
        }
        else
        {
            directionPressed = false;
            ReadyToMove = true;
        }
    }

    public bool Move(Vector2 direction)
    {
       if(Mathf.Abs(direction.x) < 0.5) 
       {
            direction.x = 0;
       }
       else 
       {
            direction.y = 0;
       }
       direction.Normalize();

       if(Blocked(transform.position, direction))
       {
           return false;
       }
       else
       {
           transform.Translate(direction);
           return true;
       }
    }

   public bool Blocked(Vector3 position, Vector2 direction)
{
    Vector2 newpos = new Vector2(position.x, position.y) + direction;

    foreach (var obj in Obstacles)
    {
        if (obj.GetComponent<SpriteRenderer>().bounds.Contains(newpos))
        {
            return true;
        }
    }

    foreach (var objToPush in ObjToPush)
    {
        if (objToPush != gameObject && objToPush.GetComponent<SpriteRenderer>().bounds.Contains(newpos))
        {
            Push objpush = objToPush.GetComponent<Push>();
            if (objpush && objpush.Move(direction))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    
    return false;
}


}
