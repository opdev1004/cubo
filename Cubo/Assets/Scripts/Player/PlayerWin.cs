using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Checks if the player meets the requirements of winning the game.
public class PlayerWin : MonoBehaviour
{
    Rigidbody rigidbody;
    List<PlayerWin> winList;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        winList = new List<PlayerWin>();
        winList.AddRange(FindObjectsOfType<PlayerWin>());
    }

    // Update is called once per frame
    void Update()
    {
        //Player wins if the player is the last on that meets the win condition.
        if (winList.Contains(this) && winList.Count == 1)
        {
            Debug.Log("win condition met for " + gameObject.name);
        }
    }

    //If the player loses this method is called which removes itself from all instances of PlayerWin.
    public void Disqualify()
    {
        if (winList.Contains(this)) 
        {
            winList.Remove(this);
            foreach (PlayerWin candidate in winList)
            {
                candidate.winList.Remove(this);
            }
        }
    }
}
