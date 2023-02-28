using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    private List<GameObject> Obstacles;
    private List<GameObject> ObjToPush;
    // Start is called before the first frame update
    void Start()
    {
        Obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacles"));
        ObjToPush = new List<GameObject>(GameObject.FindGameObjectsWithTag("ObjToPush"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool Move(Vector2 direction)
    {
        if(ObjToBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

     public bool ObjToBlocked(Vector3 position, Vector2 direction)
{
    Vector2 newpos = new Vector2(position.x, position.y) + direction;

    foreach (var obj in Obstacles)
    {
        if (obj != null && obj.GetComponent<SpriteRenderer>().bounds.Contains(newpos))
        {
            foreach (var objToPush in ObjToPush)
            {
                if (objToPush.transform.position.x == newpos.x && objToPush.transform.position.y == newpos.y)
                {
                    GameObject objToPushObj = objToPush.gameObject;
                    Destroy(objToPushObj);
                    ObjToPush.Remove(objToPush); // remove o objeto destruído da lista
                    return true;
                }
            }
        }
    }

    foreach (var objToPush in ObjToPush)
    {
        if(objToPush.transform.position.x == newpos.x && objToPush.transform.position.y == newpos.y)
        {
            return true;
        }
    }
    return false;
}

}
