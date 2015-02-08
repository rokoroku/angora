using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Food : BaseFood, IEatable
{   
    private float m_timeToDisappear;
    private bool isEatable;

    #region MonoBehaviour Methods
    void Start()
    {
        float randomTimeFactor = DisappearTime / 5;
        m_timeToDisappear = Time.time + DisappearTime + UnityEngine.Random.Range(-randomTimeFactor, randomTimeFactor);
        isEatable = true;
    }
    
    void Update()
    {
        if (Time.time > m_timeToDisappear)
        {
            GetComponent<Animator>().SetTrigger("Disappear");
        }
    }
    #endregion  
    
    // Destroy this object
    void Destroy()
    {
        Destroy(gameObject);
    }
    
    // Override from IEatable
    public void Eat()
    {
        if (isEatable)
        {
            GetComponent<Animator>().SetTrigger("Disappear");
            audio.PlayOneShot(BiteSound);
            isEatable = false;
        }
    }
    
}
