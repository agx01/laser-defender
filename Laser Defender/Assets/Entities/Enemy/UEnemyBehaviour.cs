using UnityEngine;
using System.Collections;

public class UEnemyBehaviour : MonoBehaviour {

	public float health;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;

	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Fire();
		}
	}

	void Fire(){
		Vector3 startPositon = transform.position + new Vector3(0, -1, 0);
		GameObject missile = Instantiate(projectile,startPositon,Quaternion.identity) as GameObject;
		Rigidbody2D missileRB = missile.GetComponent<Rigidbody2D>();
		missileRB.velocity = new Vector2(0,-projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
            	Die();
            }
        }
    }

    void Die(){
		AudioSource.PlayClipAtPoint(deathSound,transform.position);
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
    }
}
