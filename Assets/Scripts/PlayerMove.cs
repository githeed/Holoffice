using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController cc;
    private Transform model;
    private PlayerManager playerManager;
    private Animator animator;

    public Transform cam;
    public float moveSpeed = 5;     // 사용자 이동속도 
    public bool isSit = false;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();      // PlayerManager를 가진 컴포넌트 찾기 (하나만 존재할경우 사용)
        cc = GetComponent<CharacterController>();
        model = transform.Find(playerManager.avatarPrefabs[playerManager.AvatarNum()].name + "(Clone)");    // Instantiate로 생성하여 뒤에 (Clone) 추가

        Cursor.lockState = CursorLockMode.Confined;

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isSit)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Sit();
        }
    }

    void Move()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        if (dir.magnitude > 0.1f)
        {
            animator.SetBool("IsWalk", true);

            Vector3 forward = cam.forward;
            Vector3 right = cam.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 movedir = (forward * dir.z + right * dir.x).normalized;

            model.transform.forward = movedir;

            cc.Move(movedir * moveSpeed * Time.deltaTime);

        }
        else
        {
            animator.SetBool("IsWalk", false);
        }
    }

    void Sit()
    {
        isSit = !isSit;
        animator.SetBool("IsSit", isSit);
    }
}
