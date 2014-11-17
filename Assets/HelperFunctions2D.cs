using UnityEngine;
using System;

namespace Assets {
    public static class HelperFunctions2D {
        public static Quaternion Look2D(Transform looker, Transform lookee) {
            float AngleRad = Mathf.Atan2(looker.position.y - lookee.position.y, looker.position.x - lookee.position.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            return Quaternion.Euler(0, 0, AngleDeg);
        }
    }
}
