using UnityEngine;
using System.Collections;

namespace Survive{
	public class SingleCellularAutomata : CellularAutomata{
		Board<SingleCell> board = new Board<SingleCell>();//Area where cells are stored
		SingleCell[] celltype;

		public override bool CellGen (){
			size -= 1; //Drop size to the amount displayed by inspector
			Init ();
			
			for(int ii=0;ii<t;++ii){
				for(int i=0;i<size.x;++i){
					for(int j=0;j<size.y;++j){
						if(board[i,j].alive==true){
							if(AliveNeighbours(new Vector2i(i,j)).Length>board[i,j].adjacent_limit){
								board[i,j].alive=false;
							}
							else {
								if(board[i,j].spawn_count!=0){
									Vector2i[] dead = DeadNeighbours(new Vector2i(i,j));
									SetAlive(dead);
									board[i,j].spawn_count -= dead.Length;

								}
							}
						}
					}
				}
			}
			return SpawnCells();
		}

		protected override bool SpawnCells (){
			base.SpawnCells ();
			int i = 0;
			int j = 0;
			int ii = 0;

			for(i=0;i<size.x;i+=board[i,j].size.x)
				for(j=0;j<size.y;j+=board[i,j].size.y)
				if(board[i,j].alive==true){
					go = Instantiate(board[i,j].go,new Vector3(i*scale.x,0,j*scale.y)
					                 +offset+board[i,j].offset,
					                 Quaternion.identity) as GameObject;
					++ii;
					//gos[ii] = go;
				}
			
			//meshbaker.AddDeleteGameObjects(gos,null);
			meshbaker.Apply(true,true,true,true,true,true,false,false,false);
			
			return true;
		}

		protected override void Init (){
			base.Init ();
		}
	}
}
