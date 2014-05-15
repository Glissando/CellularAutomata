using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Survive{
	[System.Serializable]
	public class Cell{
		public Vector2i size;
		public GameObject go;
		public string id;
		public int spawn_count;//Amount of cells that can be spawn from a cell, if set to 0, infinite blocks are spawned
		public int adjacent_limit;//Amount of connecting cells before it dies
		public int amount;
		public int starting_count;
		public Vector3 offset;
		public bool alive{
			get{
				return active;
			}
			set{
				active = value;
			}
		}
		public Cell(){
			spawn_count = 0;
			adjacent_limit = 0;
			amount = 0;
			starting_count = 0;
			offset = new Vector3();
			alive = false;
		}

		public override bool Equals (Cell obj){
			if(this.id.Equals(obj.id)&&this.go==obj.go)
				return true;
			return false;
		}
	}
	[System.Serializable]
	public class MultiCell : Cell {
		public int layer;

		public MultiCell(){
			spawn_count = 0;
			adjacent_limit = 0;
			amount = 0;
			starting_count = 0;
			offset = new Vector3();
			alive = false;
			layer = 0;
			Mathf.Clamp(this.adjacent_limit,0,8);
		}

		public MultiCell(MultiCell cell){
			this.size = cell.size;
			this.go = cell.go;
			this.spawn_count = cell.spawn_count;
			this.adjacent_limit = cell.adjacent_limit;
			this.amount = cell.amount;
			this.starting_count = cell.starting_count;
			this.active = cell.active;
			this.layer = cell.layer;
			this.precedence = cell.precedence;
			Mathf.Clamp(this.adjacent_limit,0,8);
		}
	}
	[System.Serializable]
	public class SingleCell : Cell {

	}
}
