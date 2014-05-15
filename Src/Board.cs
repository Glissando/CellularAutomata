using UnityEngine;
using System.Collections;
using System.IO;

namespace Survive{
	[System.Serializable]
	public class Board<T>{
		Vector2i size;

		T[] Array = new T[100];

		public T this[int x, int y]{
			get{
				if(y>size.y||x>size.x||y<0||x<0){
					//throw new IOException("Out of range exception");
					Debug.LogError("Out of range get operation");
					return null;
				}
				return Array[size.x*y+x];
			}
			set{
				if(y>size.y||x>size.x||y<0||x<0){
					//throw new IOException("Out of range exception");
					Debug.LogError("Out of range set operation");
				}
				else
					Array[size.x*y+x] = value;
			}
		}

		public int Count{
			get{
				int i = 0;
				foreach(T obj in Array)
					i++;
				return i;
			}
			private set;
		}

		public Vector2i Capacity{
			get{
				return new Vector2i(size.x,size.y);
			}
			set{
				Array = new T[value.x*value.y];
				size = value;
			}
		}

		public Board(){

		}

		public Board(int x, int y){
			Array = new T[x*y];
			size.x = x;
			size.y = y;
		}

		public System.Type GetType(){
			return Array.GetType();
		}
	}
}

