using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq;

namespace Survive{

	public enum Neighbour{
		VonNeumann,
		Moores,
		Conway
	}

	public class CellularAutomata : MonoBehaviour{
		public Vector3 offset; // Amount to offset objects when spawning
		public Vector2i size;//Size of the board
		public Vector2i scale;

		public Cell[] celltype;//Cell types

		public bool isrepeat;
		[Range(0,1000)]
		public float repeat;//Repeat rate

		[Range(1,50)]
		public int t;//Maximum amount of cycles through the board

		public Neighbour neighbour;

		public virtual IEnumerator CellGen(){
			size -= 1; //Drop size to the amount displayed by inspector
			Init ();

		}

		protected virtual IEnumerator SpawnCells(){

		}

		protected virtual void SetAlive(Vector2i[] targets){
			for(int i=0;i<targets.Length;++i){
				board[targets[i].x,targets[i].y].alive = true;
			}
		}

		protected virtual void SetDead(Vector2i[] targets){
			for(int i=0;i<targets.Length;++i)
				board[targets[i].x,targets[i].y].alive = false;
		}

		protected List<Vector2i> ConwayNeighbours(Vector2i v){
			List<Vector2i> neighbours = new List<Vector2i>();

			neighbours.Add(new Vector2i(v.x-1,v.y+1));
			neighbours.Add(new Vector2i(v.x+1,v.y+1));
			neighbours.Add(new Vector2i(v.x+1,v.y-1));
			neighbours.Add(new Vector2i(v.x-1,v.y-1));

			//Check if the cells are in bounds
			for(int i=0;i<neighbours.Count;++i)
				if(isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours;
		}

		protected List<Vector2i> ConwayNeighbours(int x, int y){
			List<Vector2i> neighbours = new List<Vector2i>();
			
			neighbours.Add(new Vector2i(x-1,y+1));
			neighbours.Add(new Vector2i(x+1,y+1));
			neighbours.Add(new Vector2i(x+1,y-1));
			neighbours.Add(new Vector2i(x-1,y-1));
			
			//Check if the cells are in bounds
			for(int i=0;i<neighbours.Count;++i)
				if(isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours;
		}

		protected List<Vector2i> ConwayNeighbours(Vector2i v, int d){

			for(int i=0;i<d;i++){
				neighbours.Add(new Vector2i(v.x-1-i,v.y+1+i));
				neighbours.Add(new Vector2i(v.x+1+i,v.y+1+i));
				neighbours.Add(new Vector2i(v.x+1+i,v.y-1-i));
				neighbours.Add(new Vector2i(v.x-1-i,v.y-1-i));
			}

			//Check if the cells are in bounds
			for(int i=0;i<neighbours.Count;++i)
				if(isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours;
		}

		protected List<Vector2i> ConwayNeighbours(int x, int y, int d){
			
			for(int i=0;i<d;i++){
				neighbours.Add(new Vector2i(x-1-i,y+1+i));
				neighbours.Add(new Vector2i(x+1+i,y+1+i));
				neighbours.Add(new Vector2i(x+1+i,y-1-i));
				neighbours.Add(new Vector2i(x-1-i,y-1-i));
			}
			
			//Check if the cells are in bounds
			for(int i=0;i<neighbours.Count;++i)
				if(isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours;
		}

		protected List<Vector2i> VonNeumannNeighbours(Vector2i v){
			List<Vector2i> neighbours = new List<Vector2i>();
			
			neighbours.Add(new Vector2i(v.x,v.y+1));
			neighbours.Add(new Vector2i(v.x+1,v.y));
			neighbours.Add(new Vector2i(v.x,v.y-1));
			neighbours.Add(new Vector2i(v.x-1,v.y));
			
			//Check if the cells are in bounds
			for(int i=0;i<neighbours.Count;++i)
				if(isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours;
		}

		protected List<Vector2i> VonNeumannNeighbours(int x, int y){
			List<Vector2i> neighbours = new List<Vector2i>();
			
			neighbours.Add(new Vector2i(x,y+1));
			neighbours.Add(new Vector2i(x+1,y));
			neighbours.Add(new Vector2i(x,y-1));
			neighbours.Add(new Vector2i(x-1,y));
			
			//Check if the cells are in bounds
			for(int i=0;i<neighbours.Count;++i)
				if(isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours;
		}

		protected List<Vector2i> VonNeumannNeighbours(Vector2i v, int d){
			List<Vector2i> neighbours = new List<Vector2i>();

			for(int i=0;i<d;i++){
				neighbours.Add(new Vector2i(v.x,v.y+1+i));
				neighbours.Add(new Vector2i(v.x+1+i,v.y));
				neighbours.Add(new Vector2i(v.x,v.y-1-i));
				neighbours.Add(new Vector2i(v.x-1-i,v.y));
			}

			//Check if the cells are in bounds
			for(int i=0;i<neighbours.Count;++i)
				if(isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours;
		}

		protected List<Vector2i> VonNeumannNeighbours(int x, int y, int d){
			List<Vector2i> neighbours = new List<Vector2i>();
			
			for(int i=0;i<d;i++){
				neighbours.Add(new Vector2i(x,y+1+i));
				neighbours.Add(new Vector2i(x+1+i,y));
				neighbours.Add(new Vector2i(x,y-1-i));
				neighbours.Add(new Vector2i(x-1-i,y));
			}
			
			//Check if the cells are in bounds
			for(int i=0;i<neighbours.Count;++i)
				if(isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours;
		}

		protected List<Vector2i> MooresNeighbours(Vector2i v){
			List<Vector2i> neighbours = new List<Vector2i>();

			neighbours.Add(new Vector2i(v.x,v.y+1));
			neighbours.Add(new Vector2i(v.x+1,v.y+1));
			neighbours.Add(new Vector2i(v.x+1,v.y));
			neighbours.Add(new Vector2i(v.x+1,v.y-1));
			neighbours.Add(new Vector2i(v.x,v.y-1));
			neighbours.Add(new Vector2i(v.x-1,v.y-1));
			neighbours.Add(new Vector2i(v.x-1,v.y));
			neighbours.Add(new Vector2i(v.x-1,v.y+1));

			//Check if the cells are in bounds
			for(int i=0;i<neighbours.Count;++i)
				if(isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours;
		}

		protected Vector2i[] AliveNeighbours(Vector2i v,int amount){
			List<Vector2i> neighbours = new List<Vector2i>();
			switch(neighbour){
				case Neighbour.VonNeumann:
					neighbours = VonNeumannNeighbours(v);
					break;
				case Neighbour.Moores:
					neighbours = MooresNeighbours(v);
					break;
				case Neighbour.Conway:
					neighbours = ConwayNeighbours(v);
					break;
				default:
					neighbours = VonNeumannNeighbours(v);
					break;
			}

			for(int i=0;i<neighbours.Count&&i<amount;i++)
				if(board[neighbours[i].x,neighbours[i].y].alive==true)
					neighbours.RemoveAt(i);
			return neighbours.ToArray();
		}

		protected Vector2i[] AliveNeighbours(Vector2i v){
			List<Vector2i> neighbours = new List<Vector2i>();
			switch(neighbour){
				case Neighbour.VonNeumann:
					neighbours = VonNeumannNeighbours(v);
					break;
				case Neighbour.Moores:
					neighbours = MooresNeighbours(v);
					break;
				case Neighbour.Conway:
					neighbours = ConwayNeighbours(v);
					break;
				default:
					neighbours = VonNeumannNeighbours(v);
					break;
			}

			for(int i=0;i<neighbours.Count;i++)
			if(cells[neighbours[i].x,neighbours[i].y].alive==true){
				neighbours.RemoveAt(i);
			}
			return neighbours.ToArray();
		}

		protected Vector2i[] DeadNeighbours(Vector2i v){
			List<Vector2i> neighbours = new List<Vector2i>();
			switch(neighbour){
				case Neighbour.VonNeumann:
					neighbours = VonNeumannNeighbours(v);
					break;
				case Neighbour.Moores:
					neighbours = MooresNeighbours(v);
					break;
				case Neighbour.Conway:
					neighbours = ConwayNeighbours(v);
					break;
				default:
					neighbours = VonNeumannNeighbours(v);
					break;
			}

			for(int i=0;i<neighbours.Count;i++)
			if(board[neighbours[i].x,neighbours[i].y].alive==false){
				neighbours.RemoveAt(i);
			}
			return neighbours.ToArray();
		}

		protected Vector2i[] DeadNeighbours(Vector2i v,int amount){
			List<Vector2i> neighbours = new List<Vector2i>();
			switch(neighbour){
				case Neighbour.VonNeumann:
					neighbours = VonNeumannNeighbours(v);
					break;
				case Neighbour.Moores:
					neighbours = MooresNeighbours(v);
					break;
				case Neighbour.Conway:
					neighbours = ConwayNeighbours(v);
					break;
				default:
					neighbours = VonNeumannNeighbours(v);
					break;
			}

			for(int i=0;i<neighbours.Count&&i<amount;++i)
			if(board[neighbours[i].x,neighbours[i].y].alive==false){
				neighbours.RemoveAt(i);
			}
			return neighbours.ToArray();
		}

		protected Vector2i[] Neighbours(Vector2i v){
			List<Vector2i> neighbours = new List<Vector2i>();
			switch(neighbour){
				case Neighbour.VonNeumann:
					neighbours = VonNeumannNeighbours(v);
					break;
				case Neighbour.Moores:
					neighbours = MooresNeighbours(v);
					break;
				case Neighbour.Conway:
					neighbours = ConwayNeighbours(v);
					break;
				default:
					neighbours = VonNeumannNeighbours(v);
					break;
			}

			return neighbours.ToArray();
		}

		protected Vector2i[] Neighbours(Vector2i v,int amount){
			List<Vector2i> neighbours = new List<Vector2i>();
			switch(neighbour){
				case Neighbour.VonNeumann:
					neighbours = VonNeumannNeighbours(v);
					break;
				case Neighbour.Moores:
					neighbours = MooresNeighbours(v);
					break;
				case Neighbour.Conway:
					neighbours = ConwayNeighbours(v);
					break;
				default:
					neighbours = VonNeumannNeighbours(v);
					break;
			}
			Mathf.Clamp(amount,0,8);
			return neighbours.RemoveRange(amount,neighbours.Count-amount);
		}

		protected bool isOutsideBounds(Vector2i v){
			if(v.x>size.x-1||v.y>size.y-1||v.x<0||v.y<0)
				return true;
			return false;
		}

		protected bool isOutsideBounds(int x, int y){
			if(x>size.x-1||y>size.y-1||x<0||y<0)
				return true;
			return false;
		}

		protected Cell GetType(Cell cell){
			for(int i=0;i<celltype.Length;i++)
				if(celltype.Equals(cell))
					return celltype[i];
		}

		protected MultiCell GetType(MultiCell cell){
			for(int i=0;i<celltype.Length;i++)
				if(celltype.Equals(cell))
					return celltype[i];
		}

		protected SingleCell GetType(SingleCell cell){
			for(int i=0;i<celltype.Length;i++)
				if(celltype.Equals(cell))
					return celltype[i];
		}

		protected virtual void Init(){
			int j = 0;
			for(int i=0;i<celltype.Length;i++)
				while(cell[j].starting_count!=0){
					int rx = Random.Range(0,size.x);
					int ry = Random.Range(0,size.y);
					j++;
					board[rx,ry] = new Cell(celltype[i]);
					cell[i].starting_count--;
				}
		}
	}
}
