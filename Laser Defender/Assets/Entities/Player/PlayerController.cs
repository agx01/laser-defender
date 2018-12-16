using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public float padding = 1f;
    public float projectileSpeed;
    public float fireRate = 0.2f;
    public GameObject projectile;
    public int health = 250;
    public AudioClip fireSound;
    public LevelManager levelmanager;

	private LivesKeeper lk;


    float xmin ;
    float xmax ;

	// Use this for initialization
	void Start () {
		lk = GameObject.Find("Lives").GetComponent<LivesKeeper>();
		levelmanager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();  
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0 ,0 ,distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("PlayerShoot", 0.00001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("PlayerShoot");
        }
        MoveShip();
	}

    void MoveShip(){
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        //restrict the player to the playspace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void PlayerShoot()
    {	
    	Vector3 offset = new Vector3(0,1,0);
        GameObject beam = Instantiate(projectile, transform.position + offset , Quaternion.identity) as GameObject;
        Rigidbody2D beamrb = beam.GetComponent<Rigidbody2D>();
        beamrb.velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound,transform.position);
    }

    public int ReturnHealth(){
    	return health;
    }

    void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();

        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
			lk.UpdateHealth(missile.GetDamage());
            if (health <= 0)
            {
                Destroy(gameObject);
                levelmanager.LoadLevel("Win Screen");
            }
        }
    }
}
