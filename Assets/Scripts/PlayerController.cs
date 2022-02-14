using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float airSpeed;
    private float applySpeed;

    [Header("Player Jump & Gravity")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float maxGravity;

    [Header("Shooting")]
    [SerializeField]
    private float ShootDelay;
    private float currentShootDelay = 0;

    [Header("SQUID")]
    [SerializeField]
    private int maxSquid;
    private int currentSquid;

    //상태 변수
    private bool isGround = true;
    private bool isNormal = true;
    private bool isShooting;
    private bool isRun;
    private bool isLongJump;


    [Header("Camera")]
    //카메라 민감도
    [SerializeField]
    private float lookSensitivity;

    //카메라 회전 한계
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0f;


    //필요한 컴포넌트
    [SerializeField]
    Camera theCamera;
    [SerializeField]
    GameObject SquidPrefab;
    [SerializeField]
    GameObject Arms;
    [SerializeField]
    PlayerStatus thePlayerStatus;
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        applySpeed = walkSpeed;
    }

    /*private void FixedUpdate()
    {
        Gravity();
    }

    private void Gravity()
    {
        if(!isGround)
        {
            myRigid.velocity -= new Vector3(0, gravity, 0);
        }
    }*/


    void Update()
    {
        CameraRotation();
        CharacterRotation();
        CheckGround();
        CalcShootDelay();
        //ShowVelocity();
        TryRun();
        TryMove();
        TryJump();
        TryShoot();
    }



    private void ShowVelocity()
    {
        Debug.Log(new Vector3(myRigid.velocity.x, 0, myRigid.velocity.z).magnitude);
    }

    private void CheckGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.2f);
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunCancel();
        }
    }

    private void TryMove()
    {
        if(isGround)
        {
            Move();
        }
        else
        {
            AirMove();
        }
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        velocity.y = myRigid.velocity.y;
        myRigid.velocity = velocity;

    }

    private void Run()
    {
        applySpeed = runSpeed;
    }

    private void RunCancel()
    {
        applySpeed = walkSpeed;
    }

    private void AirMove()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 currentVelocity = myRigid.velocity;

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 velocity = (_moveHorizontal + _moveVertical).normalized * airSpeed;

        if(isNormal && (velocity + new Vector3(myRigid.velocity.x, 0, myRigid.velocity.z)).magnitude > runSpeed)
        {
            return;
        }
        myRigid.velocity += velocity * Time.deltaTime;
    }

    private void CameraRotation()
    {
        //상하 카메라 회전

        float _xRotation = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }

    private void CharacterRotation()
    {
        //좌우 케릭터 회전

        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0, _yRotation, 0) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        myRigid.velocity = jumpForce * transform.up;
    }

    private void CalcShootDelay()
    {
        if(currentShootDelay > 0)
        {
            currentShootDelay -= Time.deltaTime;
        }
    }

    private void TryShoot()
    {
        if (Input.GetButton("Fire1") && currentShootDelay <= 0 && !isShooting)
        {
            StartCoroutine("Shoot");
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        Arms.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Instantiate(SquidPrefab, theCamera.transform.position + transform.rotation.normalized * Vector3.forward * 1.5f, theCamera.transform.rotation);
        currentShootDelay = ShootDelay;
        isShooting = false;
        yield break;
    }
}
