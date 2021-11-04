using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothing;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    private GameObject player;


    private void LateUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dummy");
        foreach (GameObject player in players)
        {
            if (PhotonView.Get(player).IsMine)
            {
                target = player.transform;
                if (transform.position != target.position)
                {
                    Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
                    targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
                    targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
                    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
                } 
            }
        }
    }
}
