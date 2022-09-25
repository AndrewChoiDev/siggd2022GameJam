using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Soldier : MonoBehaviour, IAttackable
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float inputWeight;
    [SerializeField] private float repelWeight;
    [SerializeField] private float spinWeight;
    private Collider2D coll;


    private void Awake() {
        SoldierManager.soldierList.Add(this.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var body = GetComponent<Rigidbody2D>();
        var inX = Input.GetAxis("Horizontal");
        var inY = Input.GetAxis("Vertical");

        var targetDir = (Vector2.right * inX + Vector2.up * inY).normalized;

        var inputAccel = targetDir * inputWeight;
        if (targetDir == Vector2.zero) {
            inputAccel = -(body.velocity.normalized) * inputWeight;
        }

        var totalAccel = (inputAccel);

        body.velocity += totalAccel * Time.deltaTime;
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
    }

    private void OnDestroy() {
        SoldierManager.soldierList.Remove(this.transform);
        if (SoldierManager.soldierList.Count == 0) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void Attack(int amount)
    {
        Destroy(gameObject);
    }
}
