using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6f;

    Vector3 movement;
    Animator animator;
    Rigidbody rigid;
    int floorMask;
    int isWalkingId;
    float camRayLength = 100f;

    private void Awake() {
        animator = GetComponent<Animator>();
        isWalkingId = Animator.StringToHash("isWalking");
        rigid = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor").GetHashCode();
    }

    private void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    private void Move(float h, float v) {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime; // vector normalized
        rigid.MovePosition(transform.position + movement);
    }

    private void Turning() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f; // to skyp modify the turn in y axi

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rigid.MoveRotation(newRotation);
        }
    }

    private void Animating(float h, float v) {
        bool isWalking = v != 0f || h != 0f;
        animator.SetBool(isWalkingId, isWalking);
    }
}
