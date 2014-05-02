using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Auto{
	
	public enum Neighbour
	{
		VonNeumann,
		Moores
	}
	
	public class CellularAutomata : MonoBehaviour
	{
		public GameObject cell;//Object that will represent a single cell
		[Range(1,100)]
		public float cell_size;
		public Vector3 offset; // Amount to offset the object when spawning
		[Range(1,100)]
		public int starting_count;//Starting count for cells in the arena
		public Vector2i size;//Size of the board
		public int spawn_count;//Amount of cells that can be spawn from a cell, if set to 0, infinite blocks are spawned
		public int adjacent_limit;//Amount of connecting cells before it dies
		public bool isrepeat;
		[Range(0,1000)]
		public float repeat;//Repeat rate
		public bool[,] cells = new bool[10,10];
		[Range(1,100)]
		public int t;//Maximum amount of cycles through the board
		
		public Neighbour neighbour;
		
		//Mesh-Combine
		public MB2_MeshBaker meshbaker;
		[HideInInspector]
		public GameObject [] gos;
		GameObject go;
		
		void Awake(){
			if(isrepeat)
				InvokeRepeating("CellGen",1.0f,repeat);
			else
				CellGen();

		}
		
		protected void CellGen(){
			cells = new bool[size.x,size.y];
			Init ();
			for(int ii=0;ii<t;++ii){
				for(int i=0;i<size.x;++i){
					for(int j=0;j<size.y;++j){
						if(cells[i,j]==true){
							if(AliveNeighbours(new Vector2i(i,j)).Length>adjacent_limit){
								cells[i,j]=false;
							}
							else {
								SetAlive(DeadNeighbours(new Vector2i(i,j),spawn_count));
							}
						}
					}
				}
			}
			SpawnCells();
		}

		protected void SpawnCells(){
			int ii = 0;
			for(int i=0;i<size.x;++i)
				for(int j=0;j<size.y;++j)
					if(cells[i,j]==true){
						go = Instantiate(cell,new Vector3(i,0,j)*cell_size+offset,Quaternion.identity) as GameObject;
						++ii;
					}

			meshbaker.AddDeleteGameObjects(gos,null);
			meshbaker.Apply(true,true,true,true,true,true,false,false,false);
			
		}

		protected void SetAlive(Vector2i[] targets){
			for(int i=0;i<targets.Length;++i){
				cells[targets[i].x,targets[i].y]=true;
			}
		}

		protected void SetDead(Vector2i[] targets){
			for(int i=0;i<targets.Length;++i)
				cells[targets[i].x,targets[i].y]=false;
		}
		
		protected List<Vector2i> VonNeumannNeighbours(Vector2i v){
			List<Vector2i> neighbours=new List<Vector2i>();
			neighbours.Add(new Vector2i(a.x,a.y+1));
			neighbours.Add(new Vector2i(a.x+1,a.y));
			neighbours.Add(new Vector2i(a.x,a.y-1));
			neighbours.Add(new Vector2i(a.x-1,a.y));
			
			//Check if the cells are in bounds
			for(int i=0;i<pos.Length;++i)
				if(!isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours.ToArray();
		}
		
		protected List<Vector2i> VonNeumannNeighbours(Vector2i v, int d){
			List<Vector2i> neighbours=new List<Vector2i>();
			
			for(int i=0;i<d;i++){
				neighbours.Add(new Vector2i(a.x,a.y+1+i));
				neighbours.Add(new Vector2i(a.x+1+i,a.y));
				neighbours.Add(new Vector2i(a.x,a.y-1-i));
				neighbours.Add(new Vector2i(a.x-1-i,a.y));
			}
			
		
			//Check if the cells are in bounds
			for(int i=0;i<pos.Length;++i)
				if(!isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours.ToArray();
		}
		
		protected List<Vector2i> MooresNeighbours(Vector2i v){			
			List<Vector2i> neighbours=new List<Vector2i>();
			neighbours.Add(new Vector2i(a.x,a.y+1));
			neighbours.Add(new Vector2i(a.x+1,a.y+1));
			neighbours.Add(new Vector2i(a.x+1,a.y));
			neighbours.Add(new Vector2i(a.x+1,a.y-1));
			neighbours.Add(new Vector2i(a.x,a.y-1));
			neighbours.Add(new Vector2i(a.x-1,a.y-1));
			neighbours.Add(new Vector2i(a.x-1,a.y));
			neighbours.Add(new Vector2i(a.x-1,a.y+1));
			
			//Check if the cells are in bounds
			for(int i=0;i<pos.Length;++i)
				if(!isOutsideBounds(neighbours[i]))
					neighbours.RemoveAt(i);
			return neighbours.ToArray();
		}

		protected Vector2i[] AliveNeighbours(Vector2i a,int amount){
			List<Vector2i> neighbours;
			switch(neighbour){
				case VonNeumann: 
				neighbours = MooresNeighbours(a);
				break;
				case Moores:
				neighbours = VonNeumannNeighbours(a);
				break;
				default:
				neighbours = VonNeumannNeighbours(a);
				break;
			}
			
			for(int i=0;i<neighbours.Length&&i<amount;i++)
				if(cells[neighbours[i].x,neighbours[i].y]==false)
					neighbours.RemoveAt(neighbours[i]);
			return neighbours.ToArray();
		}
		
		protected Vector2i[] AliveNeighbours(Vector2i a){
			Vector2i neighbours;
			switch(neighbour){
				case VonNeumann: 
				neighbours = MooresNeighbours(a);
				break;
				case Moores:
				neighbours = VonNeumannNeighbours(a);
				break;
				default:
				neighbours = VonNeumannNeighbours(a);
				break;
			}
			
			List<Vector2i> alive=new List<Vector2i>();
				for(int i=0;i<neighbours.Length;i++)
					if(cells[neighbours[i].x,neighbours[i].y]==true){
						alive.Add(neighbours[i]);
					}
			return alive.ToArray();
		}

		protected Vector2i[] DeadNeighbours(Vector2i a){
			Vector2i neighbours;
			switch(neighbour){
				case VonNeumann: 
				neighbours = MooresNeighbours(a);
				break;
				case Moores:
				neighbours = VonNeumannNeighbours(a);
				break;
				default:
				neighbours = VonNeumannNeighbours(a);
				break;
			}
			
			List<Vector2i> dead = new List<Vector2i>();
				for(int i=0;i<neighbours.Length;i++)
					if(cells[neighbours[i].x,neighbours[i].y]==false){
						dead.Add(neighbours[i]);
					}
			return dead.ToArray();
		}

		protected Vector2i[] DeadNeighbours(Vector2i a,int amount){
			List<Vector2i> neighbours;
			switch(neighbour){
				case VonNeumann: 
				neighbours = MooresNeighbours(a);
				break;
				case Moores:
				neighbours = VonNeumannNeighbours(a);
				break;
				default:
				neighbours = VonNeumannNeighbours(a);
				break;
			}
			
			List<Vector2i> alive=new List<Vector2i>();
			for(int i=0;i<neighbours.Length&&i<amount;++i)
				if(cells[neighbours[i].x,neighbours[i].y]==false){
					neighbours.Add(neighbours[i]);
				}
			return alive.ToArray();
		}

		protected bool isOutsideBounds(Vector2i a){
			if(a.x>size.x-1||a.y>size.y-1||a.x<0||a.y<0)
				return true;
			return false;
		}

		protected void Init(){
			while(starting_count!=0){
				int rx = Random.Range(0,size.x);
				int ry = Random.Range(0,size.y);
				cells[rx,ry]=true;
				starting_count--;
			}
		}
	}
}
