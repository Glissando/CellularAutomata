using UnityEngine;
using System.Collections;

namespace Survive{
	[System.Serializable]
	public class Vector2i{
		public int x;
		public int y;
		
		public Vector2i(){
			x=0;
			y=0;
		}

		public Vector2i(Vector2i a){
			x = a.x;
			y = a.y;
		}

		public Vector2i(int x,int y){
			this.x = x;
			this.y = y;
		}

		public override string ToString (){
			return string.Format ("x: " + x +", y: " + y);
		}

		public override bool Equals (object obj){
			if(obj==this)
				return true;
			return false;
		}

		public override int GetHashCode (){
			return this.x^this.y;
		}

		public static Vector2i operator +(Vector2i a, int b){
			return new Vector2i(a.x+b,a.y+b);
		}

		public static Vector2i operator -(Vector2i a, int b){
			return new Vector2i(a.x+b,a.y+b);
		}

		public static Vector2i operator *(Vector2i a, int b){
			return new Vector2i(a.x*b,a.y*b);
		}

		public static Vector2i operator /(Vector2i a, int b){
			return new Vector2i(Mathf.FloorToInt(a.x/b),Mathf.FloorToInt(a.y/b));
		}

		public static bool operator ==(Vector2i a, Vector2i b){
			if(a.x==b.x&&a.y==b.y)
				return true;
			return false;
		}

		public static bool operator ==(Vector2i a, int b){
			if(a.x==b&&a.y==b)
				return true;
			return false;
		}

		public static bool operator !=(Vector2i a, Vector2i b){
			if(a.x==b.x&&a.y==b.y)
				return true;
			return false;
		}

		public static bool operator !=(Vector2i a, int b){
			if(a.x==b&&a.y==b)
				return true;
			return false;
		}
	}
}
