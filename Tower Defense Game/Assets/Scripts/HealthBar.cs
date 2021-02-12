using UnityEngine;

public class HealthBar : MonoBehaviour {
    	
	[SerializeField] private HealthBarSettingsSO healthBarSettings;
	[SerializeField] private Transform healthPivotTransform;
	[SerializeField] private Transform damagePivotTransform;
	[SerializeField] private Animator anim;
	private float timeToResetDamagePivot;
	private bool damaged;
	private bool reset;
	
	private void Start () {
		timeToResetDamagePivot = healthBarSettings.timeToResetDamagePivotTimer;
	}
	
	private void Update () {
		transform.LookAt(Camera.main.transform);
		if (damaged) {
			timeToResetDamagePivot -= Time.deltaTime;
			if (timeToResetDamagePivot <= 0) {
				timeToResetDamagePivot += healthBarSettings.timeToResetDamagePivotTimer;
				damaged = false;
				reset = true;
			}
		}
		
		if (reset) {
			const float speed = 5;
			damagePivotTransform.localScale = Vector3.Lerp (damagePivotTransform.localScale, healthPivotTransform.localScale, Time.deltaTime*speed);
		}
	}
    
	public void UpdateHealthBar (float currentHealth, float maxHealth, bool decrease) {
		var healthNormalized = (float)currentHealth / maxHealth;
		if (decrease) anim.SetTrigger ("Shake");
		healthPivotTransform.localScale = new Vector3 (healthNormalized, 1, 1);
		reset = false;
		damaged = true;
	}
	
	public void ShowHealthBar (bool show) {
		gameObject.SetActive (show);
	}
}
