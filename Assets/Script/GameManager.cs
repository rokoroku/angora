using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : SingletonGameObject<GameManager> {

	[SerializeField]
	private GameObject m_food;
	
	[SerializeField]
	private float m_foodRegenerationDelay;
	private float m_nextFoodGenerationTime;
	
	private GameObject m_playGround;
	private Rect m_playgroundBound;

	private GameObject m_foreGround;
	
	private List<Food> m_foodList = new List<Food>();
	
	// Use this for initialization
	void Start () {
		Application.runInBackground = true;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				Application.Quit();
			}
		}
		
		if(Input.touchCount > 0) {
			Vector2 pos = Input.GetTouch(0).position;
		}
		
		if(m_nextFoodGenerationTime < Time.time) {
			GenerateFood();
		}
		
	}

	void Awake() {
		m_playGround = GameObject.Find("PlayGround") as GameObject;
		m_foreGround = GameObject.Find("ForeGround") as GameObject;
		m_foodRegenerationDelay = 5f;
	}
	
	public void GenerateFood() {
		Vector3 position = new Vector3( Random.Range(m_playgroundBound.x, m_playgroundBound.width), Random.Range(m_playgroundBound.y, m_playgroundBound.height));
		
	    GameObject food = Instantiate(Resources.Load("Carrot")) as GameObject;
		food.transform.SetParent(m_playGround.transform, false);
		food.transform.position = position;
		
		m_nextFoodGenerationTime = Time.time + m_foodRegenerationDelay + Random.Range(-1f, 1f);           
	}
	
	public Food GetFoodInBound(Rect bound) {
		GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
		foreach (GameObject foodObject in foodObjects) {
			if(bound.Contains(foodObject.transform.position)) {
				var food = foodObject.GetComponent ("Food") as Food;
				return food;
			}
		}		
		return null;
	}
		
	public bool IsPositionInPlayground(Vector3 position) {
//		Vector2 positionVector2 = new Vector2(position.x, position.y);		
		return GetPlayGroundBound().Contains(position);		
	}
	
	public Rect GetPlayGroundBound() {
		if(m_playgroundBound.width == 0) {
			Vector3[] corners = new Vector3[4];
			(m_playGround.transform as RectTransform).GetWorldCorners(corners);
			m_playgroundBound = new Rect(corners[0].x, corners[0].y, corners[2].x, corners[2].y);
			Debug.Log ("Boundary:" + m_playgroundBound);
		}
		return m_playgroundBound;
	}
	
	public GameObject getPlayGround() {
		return m_playGround;
	}
	
	public GameObject getForeGround() {
		return m_foreGround;
	}
	
	public float GetDensityFactor() {
		return GetPlayGroundBound().width/10;
	}
	
	
}
