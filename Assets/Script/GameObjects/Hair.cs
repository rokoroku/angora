using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hair : BaseItem {
	
    private GameObject m_hairball;
    private GameObject m_ground;
    
    private float m_timeToDisappear;
	private float m_timeToStopBouncing;
	
	private bool isVisible = false;
	private bool isCollidable = true;
	
	private Rect Boundary {
		get {
			Vector3[] corners = new Vector3[4];
			(transform as RectTransform).GetWorldCorners(corners);
			float width = Mathf.Abs(corners[0].x - corners[2].x); 	
			float height = Mathf.Abs (corners[0].y - corners[2].y);
			return new Rect(corners[0].x, corners[0].y, width, height);
		}
	}
	
	// Use this for initialization
	void Start () {
        m_hairball = transform.Find("Hairball").gameObject;
        m_ground = transform.Find("Ground").gameObject;
        
        Rect boundary = Boundary;
        m_hairball.rigidbody2D.AddForce(new Vector2(boundary.width * Random.Range (-20f, 20f), boundary.height * Random.Range (30f, 50f)), ForceMode2D.Force);
		m_hairball.rigidbody2D.gravityScale = GameManager.instance.GetDensityFactor();
		
		Vector3 groundPosition = m_ground.transform.localPosition;
		groundPosition.y -= Random.Range(boundary.height/5, boundary.height);
		m_ground.transform.localPosition = groundPosition;

		m_timeToDisappear = Time.time + 5f;
		m_timeToStopBouncing = Time.time + 2.5f;

        ImageUtil.ChangeAlpha(m_hairball, 0);
        ImageUtil.ChangeSprite(m_hairball, Sprite);    
	}
    
	// Update is called once per frame
	void Update () {
        if(Time.time > m_timeToDisappear) {
            Collect();
        } else {
            if(!isVisible) {
                InterpolateAlpha();
            } 
            if(isCollidable && Time.time > m_timeToStopBouncing) {
    			RemoveRigidBody();
    		}
        }
	}
	
	void InterpolateAlpha() {
		Image image = m_hairball.GetComponent (typeof(Image)) as Image;
		Color color = image.color;
		
		color.a += (1 - color.a) * Time.deltaTime * 10;
		image.color = color;
		if(color.a == 1) isVisible = true;
	}
	
	void RemoveRigidBody() {
		GameObject.Destroy (m_hairball.rigidbody2D);
		GameObject.Destroy (m_ground.rigidbody2D);
		GameObject.Destroy (m_hairball.collider2D);
		GameObject.Destroy (m_ground.collider2D);
	}
	
	private void Collect() {

        Vector3 currentPosition = transform.position;
		GameObject button3 = GameObject.Find("Button2") as GameObject;
		Vector3 nextPosition = button3.transform.position;

        currentPosition += (nextPosition - currentPosition) * (Time.deltaTime * 2);
        transform.position = currentPosition;	

		Image image = m_hairball.GetComponent (typeof(Image)) as Image;
        if(image != null) {
    		Color color = image.color;
    		color.a += (0 - color.a) * Time.deltaTime * 2;
    		image.color = color;		
        } 
        
        if(image.color.a < 0.1f) {
            Inventory.instance.AddItem(this);
            Destroy(gameObject);
		}
	}
	
}
