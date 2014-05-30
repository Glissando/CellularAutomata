using UnityEngine;
using System.Collections;

namespace Survive{
	[System.Serializable]
	public class Biome{
		//Stores the minimum and maximum value of the that the biome supports
		Rect area;
		public float Area{
			get{
				return area.width*area.height;
			}
		}

		float chance;
		public int depth; //Maximum depth the biome in the tree the biome will support
		public int max_children; //Maximum amount of children in the node this biome will support
		public int min_children; //Minimum amount of children in the node this biome will support
		public CellularAutomata generator;

		public bool Match(Node node){
			Mathf.Clamp (chance,0.0f,100.0f);
			if(node.Area>=(area.yMin*area.xMin)&&node.Area<=(area.xMax*area.yMax))
				if(node.Depth<=depth)
					if(node.Children<=max_children&&node.Children>=min_children)
						if(Random.Range(0,100)<=chance)
							return true;
			return false;
		}
	}
}