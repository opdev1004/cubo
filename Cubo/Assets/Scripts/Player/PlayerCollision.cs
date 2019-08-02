using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerWin playerWin;
    BoxCollider m_BoxCollider;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerWin = GetComponent<PlayerWin>();
        m_BoxCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision)
    {
        playerMovement.CheckAndRunKnockbackOnCollision(collision.gameObject.GetComponent<PlayerMovement>());

        PlayerCollision otherPlayer = collision.gameObject.GetComponent<PlayerCollision>();

        if (otherPlayer != null)
        {
            if (CollisionAt(Vector3.up) && CollisionAt(Vector3.down))
            {
                Debug.Log("Collision on top of " + gameObject.name);
                playerWin.Disqualify();
            }
        }
    }

    //checks for a collision in the immediate direction (for cube shaped objects) and returns the result.
    public bool CollisionAt(Vector3 direction)
    {
        if (Physics.BoxCast(rigidbody.position, new Vector3((m_BoxCollider.size.x / 2.05f) * gameObject.transform.localScale.x, (m_BoxCollider.size.y / 2.05f) * gameObject.transform.localScale.y, (m_BoxCollider.size.z / 2.05f) * gameObject.transform.localScale.x), direction, Quaternion.LookRotation(Vector3.down), m_BoxCollider.size.y * 0.05f))
        {
            return true;
        }
        return false;
    }
}
