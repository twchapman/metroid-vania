using UnityEngine;

public class MonoBehaviour2D : MonoBehaviour {
    void LookAt2D(Transform target) {
        float AngleRad = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}