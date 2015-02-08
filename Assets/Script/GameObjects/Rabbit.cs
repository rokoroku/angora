using UnityEngine;
using System.Collections;

public class Rabbit : CacheableObject
{
	[SerializeField] private float m_speed;
	[SerializeField] private float m_weight;
	[SerializeField] private AudioClip m_jumpSound;
	[SerializeField] private AudioClip m_biteSound;
	
    private bool isMoving = false;
    private bool hasTarget = false;
    
    private float nextMoveTime;
    private Rect targetBound;
    private Vector2 defaultSize;
    private Vector3 currentPosition;
    private Vector3 nextMovePosition;
    private Vector3 targetPosition;
    
	private Facing facing;
    private Animator animator;
    
	private Rect rabbitRect
	{
		get
		{
			Vector3[] corners = new Vector3[4];
			(transform as RectTransform).GetWorldCorners(corners);
			float width = Mathf.Abs(corners [0].x - corners [2].x); 	
			float height = Mathf.Abs(corners [0].y - corners [2].y);
			float x = (facing == Facing.RIGHT) ? corners [0].x - width : corners [0].x;
			return new Rect(x, corners [0].y, width, height);
		}
	}
	
	private float boxSize
	{
		get
		{
			return GameManager.instance.GetDensityFactor();

		}
	}
	
	public enum Facing
	{
		RIGHT,
		LEFT
	}
	
	void Start()
	{
		decideNextMoveTime();
		currentPosition = transform.position;
		nextMovePosition = transform.position;
		defaultSize = (GameObject.Find("rabbit_body_normal").transform as RectTransform).sizeDelta;
	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			// Set a targetPosition as clicked position, if the point is in the playground boundary.
			if (!isMoving && rabbitRect.Contains(Input.mousePosition))
			{
				OnClick();

			} else if (GameManager.instance.IsPositionInPlayground(Input.mousePosition))
			{
				targetPosition = Input.mousePosition;				
				targetBound = new Rect(targetPosition.x - boxSize / 2, targetPosition.y - boxSize / 2, boxSize, boxSize);
								                  
				hasTarget = true;
			}
		}
		
		if (!isMoving)
		{	
			nextMovePosition = currentPosition;
			if (hasTarget || Time.time > nextMoveTime)
			{
				isMoving = true;
				CancelInvoke();
				Invoke("Move", 0.2f);
			} 	
		} else
		{
			interpolateTransform();
		}
	}
	
	void Awake()
	{
		animator = GetComponent<Animator>();		
	}
						
	private Vector3 decideNextMovePosition(Vector3 targetPosition)
	{

		Vector3 nextMovePosition;
		Vector3 deltaPosition = targetPosition - currentPosition;
		
		float distanceToJump = boxSize * m_speed;
		if (deltaPosition.sqrMagnitude > Mathf.Pow(distanceToJump, 2))
		{
			float divider = distanceToJump / deltaPosition.magnitude;
			deltaPosition.x *= divider;
			deltaPosition.y *= divider;
			nextMovePosition = currentPosition + deltaPosition;
			
		} else
		{
			nextMovePosition = targetPosition;
		}
	
		return nextMovePosition;
	}
	
	private Vector3 decideRandomNextMovePosition()
	{
		// Randomly decide next move position
		float distanceToJump = boxSize * m_speed;
		Vector3 targetPosition = currentPosition + new Vector3(Random.Range(-distanceToJump, distanceToJump), Random.Range(-distanceToJump, distanceToJump));
		Vector3 nextMovePosition = adjustMovablePosition(targetPosition);	
		return nextMovePosition;
	}
	
	private void decideNextMoveTime()
	{
		nextMoveTime = Time.time + UnityEngine.Random.Range(10f, 15f);
	}
	
	private void OnMoveAnimationEnd()
	{
		Invoke("Stop", 0.2f);

		// Reset target position when reached.
		if (hasTarget && targetBound.Contains(currentPosition))
		{
			Debug.Log("reached! " + rabbitRect + "/" + facing);
			hasTarget = false;
			
			Food food = GameManager.instance.GetFoodInBound(targetBound);
			if (food != null)
			{
				Debug.Log("food occured! " + food.transform.position);
				EatFood(food);
			}
		}
	}
	
	public void OnClick()
	{		
		animator.SetTrigger("Bounce");
		audio.PlayOneShot(m_jumpSound);
		if (m_weight > 0 && Random.Range(0f, 3f) < 1)
		{
			m_weight -= 3;
			GenerateHair();
			interpolateTransform();
		}
	}
	
	public void GenerateHair()
	{
		GameObject hair = Instantiate(Resources.Load("Hair")) as GameObject;
		hair.transform.SetParent(GameManager.instance.getForeGround().transform, false);
		Vector3 position = transform.position;
		hair.transform.position = position;
		
	}
		
	private void EatFood(Food food)
	{
		if (food != null)
		{ 
			animator.SetTrigger("Bounce");
			audio.PlayOneShot(m_biteSound);
	
			m_weight += food.Growth;
			interpolateTransform();
			food.Eat();
		}
	}
	
	private void Stop()
	{
		isMoving = false;
		animator.ResetTrigger("Move");
	}
	
	public void Move()
	{
		CancelInvoke();
		animator.ResetTrigger("Bounce");
		animator.SetTrigger("Move");
		animator.ForceStateNormalizedTime(0.0f);
		audio.PlayOneShot(m_jumpSound);
		decideNextMoveTime();
	
		if (hasTarget)
		{
			// If target position presented, head to target position.
			nextMovePosition = decideNextMovePosition(targetPosition);		
			
		} else
		{
			// Randomly move to a new position
			nextMovePosition = decideRandomNextMovePosition();
		}
	
		if (currentPosition.x > nextMovePosition.x)
		{
			if (facing == Facing.RIGHT)
			{
				transform.Rotate(0f, 180f, 0f);
				facing = Facing.LEFT;
			}
		} else
		{
			if (facing == Facing.LEFT)
			{
				transform.Rotate(0f, 180f, 0f);
				facing = Facing.RIGHT;
			}
		}			
	}
	
	private Vector3 adjustMovablePosition(Vector3 targetPosition)
	{
		if (!GameManager.instance.IsPositionInPlayground(targetPosition))
		{
			Rect playgroundBound = GameManager.instance.GetPlayGroundBound();
			if (targetPosition.x < playgroundBound.x)
				targetPosition.x = playgroundBound.x;
			else if (targetPosition.x > playgroundBound.width)
				targetPosition.x = playgroundBound.width;
			
			if (targetPosition.y > playgroundBound.height)
				targetPosition.y = playgroundBound.height;
			else if (targetPosition.y < playgroundBound.y)
				targetPosition.y = playgroundBound.y;
		}
		return targetPosition;	
	}
	
	private void interpolateTransform()
	{
		currentPosition += (nextMovePosition - currentPosition) * (isMoving ? Time.deltaTime * 3 : Time.deltaTime * 5);
		transform.position = currentPosition;	
		
		float sizeDelta = m_weight / 3;
		if (sizeDelta > 5)
			sizeDelta = 5;
		(GameObject.Find("rabbit_body_normal").transform as RectTransform).sizeDelta = defaultSize + new Vector2(sizeDelta, sizeDelta);
	}
	
}
