using UnityEngine;
using System;

public class SingletonGameObject<T> : MonoBehaviour where T : MonoBehaviour {
	private static WeakReference _container;
	private static WeakReference _instance;
	
	public static T instance
	{
		get {
			GameObject container;
			
			if( _container != null ) {
				container = _container.Target as GameObject;
				if( container != null ) {
					return _instance.Target as T;
				}
			}
			
			container = FindObjectOfType(typeof(T)) as GameObject;
            if(container == null) try {
                container = GameObject.Find(typeof(T).Name);
            } catch (Exception ignored) { }
            if(container == null) try {
                container = GameObject.Find("Canvas").GetComponentsInChildren<T>(true)[0].gameObject;
            } catch (Exception ignored) { }
            if(container == null) {
				container = new GameObject();
				container.name = "_" + typeof(T).Name;
			}

			T instance = container.GetComponent<T>();

            if(instance == null) { 
                instance = container.AddComponent(typeof(T)) as T;
			}
			_container = new WeakReference( container, false );
			_instance = new WeakReference( instance, false );
			
			return instance;
		}
	}
}

public class Singleton<T> where T : new() {
	private static T _instance;
	
	public static T instance {
		get {
			if( _instance == null ) {
				_instance = new T();
			}
			
			return _instance;
		}
	}
}