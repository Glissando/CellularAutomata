using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Survive{
	public enum MapSize{
		small,
		medium,
		large
	}

	public class BSPTree : MonoBehaviour{
		Node root;
		int depth;

		public Biome[] generators;
		float progress;

		public float Progress{
			get{
				return progress*100;
			}
			set{
				progress = value;
			}
		}

		public MapSize ReadSize(string map){
			return (MapSize) Enum.Parse(typeof(MapSize),map,true);
		}

		public IEnumerator GenerateWorld(){
			bool flip = false;
			Queue<Node> current_node = new Queue<Node>();
			Queue<Node> tree = new Queue<Node>();
			current_node.Enqueue(root);
			tree.Enqueue(root);
			for(int i=0;i<depth;i++){
				for(int j=0;j<current_node.Count;j++){
					flip = !flip;
					current_node.Peek().Split(flip);

					current_node.Enqueue(current_node.Peek().leafs[0]);
					current_node.Enqueue(current_node.Peek().leafs[1]);
					tree.Enqueue(current_node.Peek().leafs[0]);
					tree.Enqueue(current_node.Peek().leafs[1]);
					current_node.Dequeue();
				}
			}

			for(int i=0;i<tree.Count;i++){
				if(tree.Peek().initAutomata()){
					progress+=1/tree.Count;
					tree.Dequeue();
				}
			}
			yield return null;
		}

		public void Biome(Node node){
			int i = 0;
			while(!generators[i].Match(node)&&i<generators.Length)
				i++;
			node.gen = generators[i].generator;
			node.gen = (node.gen==null) ? generators[UnityEngine.Random.Range(0,generators.Length)].generator : node.gen;
		}
	}
}
