using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerMove playerMove;
    private Transform model;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerMove = GetComponent<PlayerMove>();
        model = transform.Find(playerManager.avatarPrefabs[playerManager.AvatarNum()].name + "(Clone)");
    }
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Chair"))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (playerMove.isSit == false)
                {
                    //other.GetComponent<MeshCollider>().enabled = false;

                    Vector3 dir = other.transform.localRotation.eulerAngles;
                    dir = new Vector3(dir.x, -dir.z, dir.y);
                    model.transform.rotation = Quaternion.Euler(dir);
                    transform.position = other.transform.position + other.transform.forward * 0.5f;

                    StartCoroutine(SitDelay());
                }
                else if (playerMove.isSit == true && !playerMove.isWrite)
                {
                    StartCoroutine(StandDelay());
                }
            }
        }
    }

    private IEnumerator SitDelay()
    {
        playerMove.Sit();
        playerMove.isSit = true;
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator StandDelay()
    {
        playerMove.Sit();
        yield return new WaitForSeconds(2f);
        playerMove.isSit = false;
    }
}
