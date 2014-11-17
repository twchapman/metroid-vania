using UnityEngine;
using System.Collections;

public class BossBullet : MonoBehaviour {
    private float moveSpeed = 5f;

    void Update() {
        Vector3 position = transform.position;
        float movement = moveSpeed * Time.deltaTime;
        float angle = transform.eulerAngles.magnitude * Mathf.Deg2Rad;
        position.x -= Mathf.Cos(angle) * movement;
        position.y -= Mathf.Sin(angle) * movement;
        transform.position = position;
    }
}
