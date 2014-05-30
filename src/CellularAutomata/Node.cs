using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Survive{
	[System.Serializable]
	public class Node : ScriptableObject{
		Rect area;

		public float Area{
			get{
				return area.width*area.height;
			}
		}
		public CellularAutomata gen;
		public Node parent;
		public List<Node> leafs;

		bool dirty;

		public int Children{
			get{
				int i = (this.dirty) ? 2 : 0;
				Node current_node = GetNode(this);
				while(current_node.leafs[0].dirty){
					i*=2;
					current_node = current_node.leafs[0];
				}
				return i;
			}
		}

		public int Depth{
			get{
				int i = 0;
				Node current_node = GetNode(this);
				while(current_node.parent!=null){
					i++;
					current_node = current_node.parent;
				}
				return i;
			}
		}

		public Node(Rect area){
			this.area.xMax = Mathf.Floor(area.xMax);
			this.area.yMax = Mathf.Floor(area.yMax);
			this.area.xMin = Mathf.Floor(area.xMin);
			this.area.yMin = Mathf.Floor(area.yMin);
			this.area = area;

			leafs = new List<Node>(2);

		}

		public void PickGen(){

		}

		public void Split(bool flip){
			if(flip){
				leafs.Add(new Node(
				new Rect(area.xMin,area.yMax,
					Random.Range(area.width/4,area.width/2),
					Random.Range(area.height/4,area.height/2)))
				);

				leafs.Add(new Node(
					new Rect(area.xMin,area.yMax,
				         Random.Range(area.width/4,area.width/2),
				         Random.Range(area.height/4,area.height/2)))
				);
			}
			else{

			}
			leafs[0].parent.SetParent(this);
			leafs[1].parent.SetParent(this);

			dirty = true;
		}

		public void SetParent(Node node){
			parent = node;
		}

		public Node GetNode(){
			return GetNode(this);
		}

		private Node GetNode(Node node){
			return node;
		}

		public bool initAutomata(){
			gen.size.x = Mathf.RoundToInt(area.width);
			gen.size.y = Mathf.RoundToInt(area.height);
			return gen.CellGen();
		}
	}
}
