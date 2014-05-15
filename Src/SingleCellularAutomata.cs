using UnityEngine;
using System.Collections;

namespace Survive{
	public class SingleCellularAutomata : CellularAutomata{
		public Board<SingleCell> board = Board<SingleCell>(size.x,size.y);//Area where cells are stored

		public override IEnumerator CellGen (){
			base.CellGen ();
			
			for(int ii=0;ii<t;++ii){
				for(int i=0;i<size.x;++i){
					for(int j=0;j<size.y;++j){
						if(board[i,j].alive==true){
							if(AliveNeighbours(new Vector2i(i,j)).Length>cells[i,j].adjacent_limit){
								board[i,j].alive=false;
							}
							else {
								if(board[i,j].spawn_count!=0){
									Vector2i[] dead = DeadNeighbours(new Vector2i(i,j));
									SetAlive(dead,cells[i,j].spawn_count);
									cells[i,j].spawn_count -= dead.Length;

								}
							}
						}
					}
				}
			}
			yield return StartCoroutine("SpawnCells");
		}

		private override IEnumerator SpawnCells (){
			base.SpawnCells ();

			int ii = 0;
			for(int i=0;i<size.x;++i)
				for(int j=0;j<size.y;++j)
				if(cells[i,j]==true){
					go = Instantiate(cells[i,j].go,new Vector3(i*scale.x,0,j*scale.y)
					                 *cell_size+offset+cells[i,j].offset,
					                 Quaternion.identity) as GameObject;
					++ii;
					gos[ii] = go;
				}
			
			meshbaker.AddDeleteGameObjects(gos,null);
			meshbaker.Apply(true,true,true,true,true,true,false,false,false);
			
			yield return null;
		}

		private override void Init (){
			base.Init ();
		}
	}
}
