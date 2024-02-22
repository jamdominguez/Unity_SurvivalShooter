using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public Transform[] patrolPath = new Transform[4];
    public float turnVelocity = 5f;

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    bool[] pointsFlag = new bool[4];
    bool isAlert, isPatrol;
    Vector3 turnVelocityVector;
    float finalRotation;
    float currentRotation;



    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        turnVelocityVector = new Vector3(0, turnVelocity, 0);
    }

    private void Start() {
        currentRotation = 0f;
        isPatrol = true;
        isAlert = false;
        StartCoroutine(Patrol());
        StartCoroutine(Alert());

    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            finalRotation = transform.rotation.eulerAngles.y;
            Debug.Log("Alert! currentRation[] finalRotation[" + finalRotation + "]");
            isPatrol = false;
            isAlert = true;
            //StartCoroutine(Alert());
            //Alert2();

        }
    }

    void Update() {

        //if (isAlert) {
        //    Alert();
        //}
        //if (isAlert) Alert();

        //if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
        //    nav.SetDestination(player.position);
        //} else {
        //    nav.enabled = false;
        //}
    }

    void PatrolWayPoints() {
        if (!pointsFlag[0]) {
            Move(0);
        } else if (!pointsFlag[1]) {
            Move(1);
        } else if (!pointsFlag[2]) {
            Move(2);
        } else if (!pointsFlag[3]) {
            Move(3);
        } else {
            pointsFlag[0] = false;
            pointsFlag[1] = false;
            pointsFlag[2] = false;
            pointsFlag[3] = false;
        }
    }

    private void Move(int point) {
        nav.SetDestination(patrolPath[point].position);
        if (Mathf.Abs(transform.position.x - patrolPath[point].position.x) < 1 && Mathf.Abs(transform.position.z - patrolPath[point].position.z) < 1) pointsFlag[point] = true;
    }


    IEnumerator Patrol() {
        while (true) {
            if (isPatrol) {
                PatrolWayPoints();
            }
            yield return null;
        }
    }

    IEnumerator Alert() {
        while (true) {
           
            if (isAlert && (currentRotation < 360f)) {
                currentRotation += turnVelocity;
                Vector3 currentAngle = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(currentAngle + turnVelocityVector);
            } else {
                currentRotation = 0f;
                isPatrol = true;
                isAlert = false;
            }
            yield return null;
        }
    }

    void Alert2() {
        Debug.Log("Alert2");
        while (true) {
            Vector3 currentAngle = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currentAngle + turnVelocityVector);
        }
    }

    IEnumerator LogInfo() {
        while (true) {
            Debug.Log("point0: " + pointsFlag[0] + " point1: " + pointsFlag[1] + " point2: " + pointsFlag[2] + " point3: " + pointsFlag[3]);
            yield return new WaitForSeconds(3f);
        }
    }
}
