using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YikonUtility {
    public static class Utility {

        public static float GetAngleFromVector (Vector3 vector3) {
            return Mathf.Atan2(vector3.x, vector3.z) * Mathf.Rad2Deg;
        }

        public static float GetAngleFromVector (Vector2 vector2) {
            return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
        }

        public static Vector3 GetDirNormalized (Vector3 a, Vector3 b) {
            return (b - a).normalized;
        }

        public static Vector3 GetDir (Vector3 a, Vector3 b) {
            return b - a;
        }

        public static int GetRandomIndex (int min, int max) {
            return Random.Range(min, max);
        }

        public static int ClampNegativeTo1 (int i) {
            return Mathf.Clamp(i, -1, 2);
        }

        public static float ClampNegativeTo1 (float i) {
            return Mathf.Clamp(i, -1, 2);
        }

        public static Vector3 ClampVectorNegativeTo1 (Vector3 v) {
            return new Vector3(ClampNegativeTo1(v.x), ClampNegativeTo1(v.y), ClampNegativeTo1(v.z));
        }

        public static Vector2 ClampVectorNegativeTo1 (Vector2 v) {
            return new Vector2(ClampNegativeTo1(v.x), ClampNegativeTo1(v.y));
        }

        public static Vector3 RoundToIntVector (Vector3 v) {
            return new Vector3(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        }

        public static Vector2 RoundToIntVector (Vector2 v) {
            return new Vector2(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }

        public static Vector3 RoundVector (Vector3 v, int decimalCount) {
            return new Vector3((float)decimal.Round((decimal)v.x, decimalCount, MidpointRounding.AwayFromZero), (float)decimal.Round((decimal)v.y, decimalCount, MidpointRounding.AwayFromZero), (float)decimal.Round((decimal)v.z, decimalCount, MidpointRounding.AwayFromZero));
        }

        public static Vector2 RoundVector (Vector2 v, int decimalCount) {
            return new Vector2((float)decimal.Round((decimal)v.x, decimalCount, MidpointRounding.AwayFromZero), (float)decimal.Round((decimal)v.y, decimalCount));
        }

        public static Vector3 FloorToIntVector (Vector3 v) {
            return new Vector3(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
        }

        public static Vector2 FloorToIntVector (Vector2 v) {
            return new Vector2(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        }

        public static Vector3 FloorVector (Vector3 v) {
            return new Vector3(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z));
        }

        public static Vector2 FloorVector (Vector2 v) {
            return new Vector2(Mathf.Floor(v.x), Mathf.Floor(v.y));
        }

        public static Vector3 CeilToIntVector (Vector3 v) {
            return new Vector3(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
        }

        public static Vector2 CeilToIntVector (Vector2 v) {
            return new Vector2(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
        }

        public static Vector3 CeilVector (Vector3 v) {
            return new Vector3(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z));
        }

        public static Vector2 CeilVector (Vector2 v) {
            return new Vector2(Mathf.Ceil(v.x), Mathf.Ceil(v.y));
        }

        public static void DistanceNear (Vector3 a, Vector3 b, float range, Action onDistanceNear) {
            if (Vector3.Distance(a, b) < range) {
                onDistanceNear?.Invoke();
            }
        }
        public static void DistanceFar (Vector3 a, Vector3 b, float range, Action onDistanceFar) {
            if (Vector3.Distance(a, b) > range) {
                onDistanceFar?.Invoke();
            }
        }

        public static void Distance (Vector3 a, Vector3 b, float range, Action onDistanceNear, Action onDistanceFar) {
            if (Vector3.Distance(a, b) > range) {
                onDistanceFar?.Invoke();
            } else if (Vector3.Distance(a, b) < range) {
                onDistanceNear?.Invoke();
            }
        }

        public static Transform FindClosestTarget (Transform currentTransform, Transform[] transforms) {
            var nearestTransform = transforms[0];
            foreach (var transform in transforms) {
                Utility.DistanceNear(currentTransform.position, transform.position, Vector3.Distance(currentTransform.position, nearestTransform.position), () => {
                    nearestTransform = transform;
                });
            }

            return nearestTransform;
        }
        
        public static Transform FindClosestTarget (Transform currentTransform, Collider[] colliders) {
            var nearestCollider = colliders[0].transform;
            foreach (var collider in colliders) {
                Utility.DistanceNear(currentTransform.position, collider.transform.position, Vector3.Distance(currentTransform.position, nearestCollider.transform.position), () => {
                    nearestCollider = collider.transform;
                });
            }

            return nearestCollider.transform;
        }
    }
}

