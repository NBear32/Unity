using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // NPC�� �̵� �ӵ�
    public float walkSpeed = 1.0f; // �ȱ� �ӵ�
    public float runSpeed = 3.0f; // �޸��� �ӵ�
    public float changeDirectionInterval = 2.0f; // ���� ���� ����
    public float moveRadius = 10.0f; // NPC�� �̵��� �ݰ�
    public float animationEndMultiplier = 1.3f; // �ִϸ��̼� ���� �� �̵� �Ÿ� ���

    private Vector3 targetPosition;
    private Vector3 lastDirection; // ������ �̵� ����
    private float timeSinceLastChange;
    private Animator animator;
    private bool isMoving;

    // �ִϸ��̼� ���� �̸�
    private string[] animationStates = { "Agreeing", "ArmStretching", "Bored", "CWalk", "DIdle", "HeadGesture", "Idle", "Idle2", "LAround", "LAround2",
                                         "Listening", "NeckStretching", "OffensiveIdle", "SadIdle", "ShoulderRubbing", "Talk", "WeightShift", "Yawn" };

    private string animationState = "Idle";
    private Rigidbody rb;
    private float timer;
    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
        SetRandomTargetPosition();

        rb = GetComponent<Rigidbody>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing from this game object.");
        }
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from this game object.");
        }
        timer = changeDirectionInterval;

        ApplyRandomAnimation();

        if (animationState == "Idle") {
            SetRandomDirection();
        }
    }

     void SetRandomDirection()
     {
        // ������ ���� ����
        int randomDirection = Random.Range(0, 4);

        switch (randomDirection)
        {
            case 0: // ������ �̵�
                movementDirection = Vector3.forward;
                transform.rotation = Quaternion.Euler(0, 0, 0); // Forward
                break;
            case 1: // �ڷ� �̵�
                movementDirection = Vector3.back;
                transform.rotation = Quaternion.Euler(0, 180, 0); // Backward
                break;
            case 2: // �������� �̵�
                movementDirection = Vector3.left;
                transform.rotation = Quaternion.Euler(0, -90, 0); // Left
                break;
            case 3: // ���������� �̵�
                movementDirection = Vector3.right;
                transform.rotation = Quaternion.Euler(0, 90, 0); // Right
                break;
        }

        //// ���� �ӵ� ���� (0f, 2f, 4f �� �ϳ�)
        //moveSpeed = Random.Range(0, 3) * 2f;

        //// �ִϸ��̼� ���� ����
        //if (moveSpeed == 0f)
        //{
        //    animator.Play("Idle");
        //}
        //else if (moveSpeed == 2f)
        //{
        //    animator.Play("Walk");
        //}
        //else if (moveSpeed == 4f)
        //{
        //    animator.Play("Run");
        //}

        //// Update the target position based on the new direction and speed
        //targetPosition = transform.position + movementDirection * moveSpeed * changeInterval;
    }

    void ApplyRandomAnimation()
    {
        if (animator != null)
        {
            // AnimationStates �������� ����
            int randomIndex = Random.Range(0, animationStates.Length);

            // �������� ���õ� �ִϸ��̼� ���� �̸� ��������
            animationState = animationStates[randomIndex];

            // ���õ� �ִϸ��̼� ���¸� �÷���
            animator.Play(animationState);
        }
    }

    // Update�� �� �����Ӹ��� ȣ��, FixedUpdate�� ������ �ð� �������� ȣ��(ȣ�� �ֱ� ����, ���� ������ ����)
    void Update()
    {
        if (animator != null)
        {
            // NPC�� �̵� ���¿� ���� �ִϸ��̼� ��ȯ
            isMoving = Vector3.Distance(transform.position, targetPosition) > 0.1f;

            if (isMoving)
            {
                // float distance = Vector3.Distance(transform.position, targetPosition);
                // float speed = moveSpeed; // �⺻ �ӵ� ����

                // string animationState = "CWalk"; // �⺻ �ִϸ��̼� ����

                // string animationState = "Idle"; // �⺻ �ִϸ��̼� ����

                //if (distance > 6.5f)
                //{
                //    speed = runSpeed; // ������ �̵��� ��
                //    animationState = "Run";
                //}
                //else
                //{
                //    speed = walkSpeed; // õõ�� �̵��� �� // 
                //    animationState = "Walk";
                //}

                // animator.Play(animationState); // �ִϸ��̼� ���� ��ȯ
                // animator.SetFloat("Speed", speed); // �ִϸ��̼� �ӵ� ����
                // MoveTowardsTarget(speed); // �ӵ��� ���ڷ� ����

                // �ִϸ��̼��� �������� üũ
                if (IsAnimationFinished())
                {
                    ApplyRandomAnimation();
                    // MoveInLastDirection();
                    // SetRandomTargetPosition(); // �ִϸ��̼��� ������ �� ���ο� Ÿ�� ����
                    timeSinceLastChange = 0.0f; // ���� ���� ���� �ʱ�ȭ

                    // ���� �ִϸ��̼� �������� Idle �̸� ���� �ٲٰ� ����
                    if (animationState == "Idle")
                    {
                        SetRandomDirection();
                    }
                }
            }
            else
            {
                animationState = "Idle";
                animator.Play(animationState); // ���� ���¿��� 'Idle' �ִϸ��̼�
                animator.SetFloat("Speed", 0); // ���� ���¿��� �ִϸ��̼� �ӵ� 0
            }
        }

        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= changeDirectionInterval)
        {
            SetRandomTargetPosition();
            timeSinceLastChange = 0.0f;
        }
    }
    private void SetRandomTargetPosition()
    {
        // moveRadius ���� ���� ���� ���� ���͸� ����
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;

        // y���� 0���� �����Ͽ� ���� �̵��� ���
        randomDirection.y = 0;

        // ���� ������Ʈ�� ��ġ�� �������� Ÿ�� ��ġ ���
        targetPosition = transform.position + randomDirection;
        lastDirection = (targetPosition - transform.position).normalized; // ������ �̵� ���� ����
    }

    private void MoveTowardsTarget(float speed)
    {
        // NPC�� targetPosition���� �ε巴�� �̵��ϵ���
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ���� ��ġ�� Ÿ�� ��ġ ������ ������ ���
        Vector3 directionToTarget = targetPosition - transform.position;

        // Ÿ�� ������ �ٶ󺸴� ȸ�� ����
        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // �ε巯�� ȸ���� ���� Slerp
        }

        // ��ǥ�� ��������� ���ο� ��ǥ�� ����
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private bool IsAnimationFinished()
    {
        // ���� �ִϸ��̼� ���¸� ������
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // �ִϸ��̼��� �������� üũ
        return stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0);
    }

    private void MoveInLastDirection()
    {
        // ������ �̵� �������� ��ġ�� �̵���ŵ�ϴ�.
        // �̵� �Ÿ� ��� ����
        Vector3 moveVector = lastDirection * animationEndMultiplier;
        transform.position += moveVector;
    }
}