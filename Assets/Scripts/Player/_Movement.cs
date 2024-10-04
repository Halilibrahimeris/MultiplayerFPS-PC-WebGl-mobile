using UnityEngine;

public class _Movement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float RunSpeed = 7f;
    public float maxVelocityChange = 10f;
    public float JumpPower = 4f;
    [Space]
    public float airController = 4f;

    public Vector2 _input;
    public Rigidbody rb;
    [SerializeField]private Animator _animator;

    private bool isSprinting;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical"));
        _input.Normalize();
    }

    void FixedUpdate()
    {
        isSprinting = Input.GetButton("Sprint");

        if (_input.magnitude > 0.1f)
        {
            rb.AddForce(CalculateMovement(isSprinting ? RunSpeed : walkSpeed), ForceMode.VelocityChange);
            if (isSprinting)
            {
                SetAnimatorWalkRun(false, true);
            }
            else
            {
                SetAnimatorWalkRun(true, false);
            }

        }
        else
        {
            rb.velocity = Vector3.zero;
            SetAnimatorWalkRun(false, false);
        }

    } 

    Vector3 CalculateMovement(float _speed)
    {
        _animator.SetBool("Inspecting", false);
        Vector3 targetVelocity = new Vector3(_input.x, 0f, _input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = rb.velocity;

        if (_input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            velocityChange.y = 0;

            return velocityChange;
        }
        else
            return new Vector3();
    }

    private void SetAnimatorWalkRun(bool Walk, bool Run)
    {
        _animator.SetBool("isRunning", Run);
        _animator.SetBool("isWalking", Walk);
    }
}
