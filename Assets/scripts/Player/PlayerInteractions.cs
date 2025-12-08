using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteractions : MonoBehaviour
{
    public Transform startPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathFloor"))
        {

            //perder vida, respawn a nuestro player

            GameManager.Instance.LoseHealth(50);
            GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = startPosition.position;
            GetComponent<CharacterController>().enabled = true;

        }


    }





}
