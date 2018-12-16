using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 150f;
    public float projectileSpeed;
    public GameObject enemyFire;
    public GameObject enemylaserspawn;
    private float fireRate;
	private float i = 0f;
    private int[] isFiring= {1,2,3,4};


    	void Start(){
			fireRate = Random.Range(0.5f,0.75f);
			enemylaserspawn=GameObject.Find("LaserSpawn");
			if(enemylaserspawn!=null){

				Debug.Log("Object found");

			}

    	}

    void Update(){
    	int fire= Random.Range(0,3);
    	if(i % 60==0&&isFiring[fire]==3){
    		EnemyFire();
    	}
		i++;
    }

    void EnemyFire(){
    	GameObject beam=Instantiate(enemyFire,transform.position,Quaternion.identity) as GameObject;
    	Rigidbody2D beamrb= beam.GetComponent<Rigidbody2D>();
    	beamrb.velocity = new Vector3(0,-projectileSpeed*fireRate,0);
    	beam.transform.parent=enemylaserspawn.transform;
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
                Destroy(gameObject);
            }
            Debug.Log("Hit by a Projectile");
        }
    }
}
