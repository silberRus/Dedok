
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float currenDistance = 0f;

    private float currRoad = 0;

    [SerializeField] private float distance = 2f;
    [SerializeField] private ParticleSystem Particle_Jump;

    private bool isMovement = false;

    private float TimeMoving;

    private bool alive = true;

    private CharacterController cc;

    private Vector3 moveDirection = Vector3.zero;

    [SerializeField] private float gravity = 25f;
    [SerializeField] private float jumpSpeed = 25f;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
     }
        

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            float dir = (Input.GetAxisRaw("Horizontal"));

            if (!isMovement && dir != 0 && dir != currRoad && currRoad < 2 && currRoad > -2)
            {
                isMovement = true;
                currenDistance = distance;
            
                if (TimeMoving == 0)
                    TimeMoving = animator.GetCurrentAnimatorStateInfo(0).length;

                StartCoroutine(Move(dir));
            }

            if (!isMovement && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Jump());
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
    }

    IEnumerator Jump()
    {
        isMovement = true;

        animator.SetTrigger("Jump");
        Particle_Jump.Play();
        moveDirection.y = jumpSpeed;
        
        while (!cc.isGrounded)
            yield return new WaitForSeconds(0.01f);

        isMovement = false;
    }

    IEnumerator Move(float currenDir)
    {
        //isMovement = true;
        animator.SetTrigger(currenDir > 0 ? "turnLeft" : "turnRight");
        
        while (currenDistance > 0)
        {
            yield return new WaitForSeconds(0.01f);
            
            float speed = distance / TimeMoving;
            float tmpDist = Time.deltaTime * speed;
            cc.Move(Vector3.right * currenDir * tmpDist);
            currenDistance -= tmpDist;
        }
        currRoad += currenDir;
        isMovement = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger"))
        { 
            animator.SetTrigger("Fall");
            WorldController.instance.worldSpeed = 0;
            alive = false;
        }            
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 120, 120, 220));
        if (GUILayout.Button("Road: " + currRoad))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GUILayout.Label(moveDirection.ToString());
        GUILayout.EndArea();
    }
}
