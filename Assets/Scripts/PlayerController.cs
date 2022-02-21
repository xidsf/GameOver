using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Move")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float airSpeed;
    private float applySpeed;
    [SerializeField]
    private float invincibleTime;
    private float currentInvincibleTime;

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
    [SerializeField]
    TextMeshProUGUI SQUIDCountText;

    //상태 변수
    [Header("Test")]
    public bool isGround = true;
    public bool isShooting;
    public bool isDead;
    public bool isInvincible;


    [Header("Camera")]
    //카메라 민감도
    [SerializeField]
    private float lookSensitivity;
    [SerializeField]
    private Vector3 KnockDownPosition;
    [SerializeField]
    private Vector3 originPosition;

    //카메라 회전 한계
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0f;


    //필요한 컴포넌트
    [Header("Components")]
    [SerializeField]
    Camera theCamera;
    [SerializeField]
    GameObject SquidPrefab;
    [SerializeField]
    GameObject Arms;
    [SerializeField]
    PlayerStatus thePlayerStatus;
    [SerializeField]
    LayerMask GroundLayerMask;
    [SerializeField]
    GameObject BottomCollider;
    [SerializeField]
    PlayerDamaged DamagedUI;
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;


    private void Awake()
    {
        Cursor.visible = false;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        applySpeed = walkSpeed;
        maxSquid = GameManager.instance.SquidCount[GameManager.instance.currentStage];
        currentSquid = maxSquid;
        
    }

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
        //ObstacleCheck();
        TryShoot();
        CalcInvisibleTime();
        DeadCheck();
        KnockDown();
        KnockDownCancel();
        ShowSquidCount();
    }


    /*private void ShowVelocity()
    {
        Debug.Log(new Vector3(myRigid.velocity.x, 0, myRigid.velocity.z).magnitude);
    }*/

    private void CheckGround()
    {
        if(BottomCollider.GetComponent<BottomCollider>().isGround)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }//지면 체크

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Vertical") == 1)
        {
            Run();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunCancel();
        }
    }//달리기 시도

    private void TryMove()
    {
        if(!thePlayerStatus.isNnockDown && !isDead)
        {
            if (isGround)
            {
                Move();
            }
            else
            {
                AirMove();
            }
        }
    }//움직이기 시도

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        velocity.y = myRigid.velocity.y;
        myRigid.velocity = velocity;
    }//움직임

    private void AirMove()//공중에서의 속력(일반 속력보다는 느림)
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 currentVelocity = myRigid.velocity;

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 velocity;

        if ((_moveVertical + new Vector3(myRigid.velocity.x, 0, myRigid.velocity.z)).magnitude >= runSpeed)
        {
            velocity = _moveHorizontal.normalized * airSpeed;
        }
        else
        {
            velocity = (_moveHorizontal + _moveVertical).normalized * airSpeed;
        }

        myRigid.velocity += velocity * Time.deltaTime;
    }

    private void Run()
    {
        applySpeed = runSpeed;
    }//달리기

    private void RunCancel()
    {
        applySpeed = walkSpeed;
    }//달리기 취소

    private void CameraRotation()
    {
        //상하 카메라 회전

        if(!thePlayerStatus.isNnockDown)
        {
            float _xRotation = Input.GetAxisRaw("Mouse Y");

            float _cameraRotationX = _xRotation * lookSensitivity;
            currentCameraRotationX -= _cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
        }
    }//y축 카메라 회전

    private void CharacterRotation()
    {
        //좌우 케릭터 회전

        if(!thePlayerStatus.isNnockDown)
        {
            float _yRotation = Input.GetAxisRaw("Mouse X");
            Vector3 _characterRotationY = new Vector3(0, _yRotation, 0) * lookSensitivity;
            myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        }
    }//x축 카메라 회전

    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround && !thePlayerStatus.isNnockDown && !isDead)
        {
            Jump();
        }
    }//점프 시도

    private void Jump()
    {
        myRigid.velocity = jumpForce * transform.up;
    }//점프

    private void CalcShootDelay()
    {
        if(currentShootDelay > 0)
        {
            currentShootDelay -= Time.deltaTime;
        }
    }//발사 딜레이 계산

    private void TryShoot()
    {
        if (currentSquid > 0 &&Input.GetButton("Fire1") && currentShootDelay <= 0 && !isShooting && !isDead)
        {
            currentSquid -= 1;
            StartCoroutine("Shoot");
        }
        else if(GameManager.instance.currentStage == 0 && currentShootDelay <= 0 && Input.GetButton("Fire1") && !isShooting)
        {
            StartCoroutine("Shoot");
        }
    }//발사시도

    private IEnumerator Shoot()
    {
        isShooting = true;
        Arms.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Instantiate(SquidPrefab, theCamera.transform.position + transform.rotation.normalized * Vector3.forward * 1.5f, theCamera.transform.rotation);
        currentShootDelay = ShootDelay;
        yield return new WaitForSeconds(0.7f);
        isShooting = false;
    }//발사 코루틴

    public void PlayerDamaged()
    {
        if(!isInvincible)
        {
            isInvincible = true;
            currentInvincibleTime = invincibleTime; 
            thePlayerStatus.PlayerHP -= 1;
        }
    }//플레이어 데미지public함수

    private void CalcInvisibleTime()
    {
        if(currentInvincibleTime > 0)
        {
            currentInvincibleTime -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }
    }

    private void KnockDown()
    {
        if(thePlayerStatus.isNnockDown)
        {
            theCamera.transform.localPosition = Vector3.Lerp(theCamera.transform.localPosition, KnockDownPosition, 0.2f);
        }
    }//넉다운

    private void KnockDownCancel()
    {
        if(!thePlayerStatus.isNnockDown)
        {
            theCamera.transform.localPosition = Vector3.Lerp(theCamera.transform.localPosition, originPosition, 0.3f);
        }
    }//넉다운 캔슬

    private void ShowSquidCount()
    {
        if(GameManager.instance.currentStage == 0)
        {
            SQUIDCountText.text = "SQUID  " + "--/--";
        }
        else
        {
            SQUIDCountText.text = "SQUID  " + currentSquid.ToString() + "/" + maxSquid.ToString();
        }
    }//징징이 UI표시

    private void DeadCheck()
    {
        if(thePlayerStatus.PlayerHP <= 0 && !isDead)
        {
            isDead = true;
            GameManager.instance.DeathCount[GameManager.instance.currentStage]++;
            DeadEvent();
        }
    }//사망 체크

    private void DeadEvent()
    {
        SceneManager.LoadScene("GameOver");
    }

    /*private void ObstacleCheck()
    {
        Vector3 _velocityXZ = new Vector3(myRigid.velocity.x, 0, myRigid.velocity.z);
        RaycastHit hit;
        Vector3 raycastPosition = transform.position + new Vector3(0, -0.4f, 0);
        float rayDistance = 0.7f;

        if(_velocityXZ != Vector3.zero)
        {
            for (int i = 0; i < 5; i++) // ray를 5번 쏨
            {
                Physics.Raycast(raycastPosition + new Vector3(0, 0.2f * i, 0), _velocityXZ.normalized, out hit, rayDistance, GroundLayerMask);
                if(hit.distance >= 0.5 && hit.distance <= 0.6)
                {
                    myRigid.velocity = _velocityXZ * (hit.distance - 0.5f) + new Vector3(0, myRigid.velocity.y, 0);
                }
                
            }
        }
    }*/

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ContactPoint _point = collision.GetContact(0);
            if(_point.separation <= 0.01f)
            {
                myRigid.velocity = new Vector3(_point.point.x - transform.position.x, myRigid.velocity.y, _point.point.z - 0.5f).normalized;
            }
            Debug.Log(_point.point.ToString() + " / " +_point.separation.ToString());
        }
    }*/

    public void AquireSquid()
    {
        currentSquid++;
    }//징징이 획득 public함수

    private void CheckQuitMenu()//ESC키 누를 시 화면에 메뉴 출력(게임 일시정지X)
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            
        }
    }


}
