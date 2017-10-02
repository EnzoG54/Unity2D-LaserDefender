using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public GameObject projectile;
	public float health = 150;
	public float projectileSpeed = 10f;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;

	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			Fire ();
		}
	}

	void Fire(){
		GameObject missile = Instantiate (projectile, transform.position, Quaternion.identity)as GameObject;
		missile.rigidbody2D.velocity = new Vector2 (0, -projectileSpeed);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0){
				Die();
			}
		}
	}

	void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		scoreKeeper.Score(scoreValue);
		Destroy (gameObject);
	}
}
