using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Survive{
	[System.Serializable]
	public class Node : ScriptableObject{
		Rect size;
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
			private set;
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
			private set;
		}

		public Node(Rect size){
			this.size.xMax = Mathf.Floor(size.xMax);
			this.size.yMax = Mathf.Floor(size.yMax);
			this.size.xMin = Mathf.Floor(size.xMin);
			this.size.yMin = Mathf.Floor(size.yMin);
			this.size = size;

			leafs = new List<Node>();

		}

		public void PickGen(){

		}

		public void Split(bool flip){
			if(flip){
				leafs.Add(new Node(
				new Rect(size.xMin,size.yMax,
					Random.Range(size.width/4,size.width/2),
					Random.Range(size.height/4,size.height/2)))
				);

				leafs.Add(new Node(
					new Rect(size.xMin,size.yMax,
				         Random.Range(size.width/4,size.width/2),
				         Random.Range(size.height/4,size.height/2)))
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

		public IEnumerator StartAutomata(){
			gen.size.x = size.width;
			gen.size.y = size.height;
			yield return gen.StartCoroutine(gen.CellGen);
		}

		public int depth(){
			return 0;
		}

		public int children(){
			return 0;
		}
	}
}
