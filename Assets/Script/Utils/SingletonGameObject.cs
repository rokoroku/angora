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
			
			container = GameObject.FindObjectOfType(typeof(T)) as GameObject;
				if(container == null) {
				container = new GameObject();
				container.name = "_" + typeof(T).Name;
			}
			T instance = container.AddComponent(typeof(T)) as T;
			
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