using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5f;
    public float spawnDelay = 0.5f;

    private float xmin,xmax;
    private bool movingRight = true;

	// Use this for initialization
	void Start () {
      	SpawnUntilFull();

        //Initialises the end points on the screen for the enemy formation
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftBoundary.x;
        xmax = rightBoundary.x;
    }

	//Marks the points on the area where enemies spawn
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(width, height));
    }

	// Update is called once per frame
	void Update () {
        MoveFormation();
        //Checks each frame if all members are dead
		if(AllMembersDead()){
        	Debug.Log("Empty Formation");
        	SpawnUntilFull();
        }
	}

    void MoveFormation()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
    }


    Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
    		if(childPositionGameObject.childCount == 0){
    			return childPositionGameObject;
    		}
    	}
    	return null;
    }

    //Checks if all the objects of the Positions are empty or not
    bool AllMembersDead(){
    	foreach(Transform childPositionGameObject in transform){
    		if(childPositionGameObject.childCount > 0){
    			return false;
    		}
    	}
    	return true;
    }

    //Spawns all enemies simultaneously
    void EnemySpawn(){
		//Creates Enemy Prefabs on the positions of the circles
		foreach(Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    //Spawn enemies one by one
    void SpawnUntilFull(){
    	Transform freePosition = NextFreePosition();
    	if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
        	enemy.transform.parent = freePosition;
        }
        if(NextFreePosition()){
        	Invoke("SpawnUntilFull",spawnDelay);
        }
    }
}
