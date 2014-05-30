using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
/*
namespace Survive{
	public class MultiCellularAutomata : CellularAutomata{
		private Board<MultiCell> board = new Board<MultiCell>(size.x,size.y);//Area where cells are stored
	
		public override IEnumerator CellGen (){
			base.CellGen ();
			 
			for(int ii=0;ii<t;ii++)
				for(int i=0;i<size.x;i++)
					for(int j=0;j<size.y;j++)
						if(board[i,j].alive){
							if(board[i,j].adjacent_limit<
						}
		}
		
		private override IEnumerator SpawnCells (){
			return base.SpawnCells ();
		}
		
		private override void SetAlive(Vector2i v){
			Vector2i[] targets = Neighbours(v);
			for(int i=0;i<v.Length;++i){
				if(board[targets[i].x,targets[i].y].layer<board[v.x,v.y].layer||
				!board[targets[i].x,targets[i].y].alive){
					board[targets[i].x,targets[i].y] = new MultiCell(GetType(board[v.x,v.y]));
				
				}
			}
		}
		
		private override void SetAlive (Vector2i[] targets,Vector2i v){
			for(int i=0;i<v.Length;++i){
				board[targets[i].x,targets[i].y] = new MultiCell(GetType(board[v.x,v.y]));

			}
		}
		
		private override void SetAlive (Vector2i[] targets,int x,int y){
			for(int i=0;i<v.Length;++i){
				board[targets[i].x,targets[i].y] = new MultiCell(GetType(board[x,y]));
				
			}
		}
		
		private override void SetDead (Vector2i[] targets,Vector2i v){
			for(int i=0;i<targets.Length;++i){
				if(board[targets.x,targets.y].layer<board[targets.x,targets.y].layer)
				board[targets[i].x,targets[i].y].alive = true;
			}

		}
		
		private override void SetDead (Vector2i[] targets,int x,int y){

		}
		
		private Vector2i[] LowerNeighbours(Vector2i v){
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
			}
			
			for(int i=0;i<neighbours.Length;i++)
				if(board[neighbours[i].x,neighbours[i].y].layer<board[v.x,v.y].layer&&board[n.x)
					
		}

		private override void Init (){
			base.Init ();
		}
	}
}*/