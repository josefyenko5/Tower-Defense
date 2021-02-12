using UnityEngine;

[CreateAssetMenu]
public class TurretSettingsSO : ScriptableObject {

    public float fireRate;
    public float damage;
    public float radius = 7;
    public float angularSmoothing = 2;
    public LayerMask enemyMask = 3;

}
