using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControler : MonoBehaviour
{
    public Camera PlayerCam;
    public float WalkSpeed = 20f;
    public float RunSpeed = 40f;
    public float JumpPower = 15f;
    public float Gravity = 7.5f;

    public float LookSpeed = 4f;
    public float LookXLimit = 90f;

    Vector3 MoveDirection = Vector3.zero;
    float RotationX = 0f;

    public bool CanMove = true;
    CharacterController CharCon;
    void Start()
    {
        CharCon = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        #region Movement
        Vector3 Forward = transform.TransformDirection(Vector3.forward);
        Vector3 Right = transform.TransformDirection(Vector3.right);

        //Press LCtrl to run
        bool IsRunning = Input.GetKey(KeyCode.LeftControl);
        float CurSpeedX = CanMove ? (IsRunning ? RunSpeed : WalkSpeed) * Input.GetAxis("Vertical") : 0;
        float CurSpeedY = CanMove ? (IsRunning ? RunSpeed : WalkSpeed) * Input.GetAxis("Horizontal") : 0;
        float MovementDirection = MoveDirection.y;
        MoveDirection = (Forward * CurSpeedX) + (Right * CurSpeedY);
        #endregion

        #region Jumpin'
        if (Input.GetButton("Jump") && CanMove && CharCon.isGrounded)
        {
            MoveDirection.y = JumpPower;
        }
        else
        {
            MoveDirection.y = MovementDirection;
        }

        if (!CharCon.isGrounded)
        {
            MoveDirection.y -= Gravity * Time.deltaTime;
        }
        #endregion

        #region Rotatin'
        CharCon.Move(MoveDirection * Time.deltaTime);

        if (CanMove)
        {
            RotationX += -Input.GetAxis("Mouse Y") * LookSpeed;
            RotationX = Mathf.Clamp(RotationX, -LookXLimit, LookXLimit);
            PlayerCam.transform.localRotation = Quaternion.Euler(RotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeed, 0);
        }
        #endregion
    }
}
