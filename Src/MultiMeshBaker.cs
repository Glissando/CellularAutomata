using UnityEngine;
using System.Collections;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Text;
using DigitalOpus.MB.Core;

public class MultiMeshBaker : MeshBakerCommon {
		
	[HideInInspector] public MultiMeshCombiner meshCombiner = new MultiMeshCombiner();
	
	public override void ClearMesh(){
		_update_MeshCombiner();
		meshCombiner.ClearMesh();
	}
	public override void DestroyMesh(){
		_update_MeshCombiner();
		meshCombiner.DestroyMesh();
	}

	public override void DestroyMeshEditor(EditorMethodsInterface editorMethods){
		_update_MeshCombiner();
		meshCombiner.DestroyMeshEditor(editorMethods);
	}
	
	public override int GetNumObjectsInCombined(){
		return meshCombiner.GetNumObjectsInCombined();	
	}
	
	public override int GetNumVerticesFor(GameObject go){
		return meshCombiner.GetNumVerticesFor(go);
	}
	
	public override Mesh AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource, bool fixOutOfBoundUVs){
		if (resultSceneObject == null){
			resultSceneObject = new GameObject("CombinedMesh-" + name);	
		}
		_update_MeshCombiner();
		Mesh mesh = meshCombiner.AddDeleteGameObjects(gos,deleteGOs,disableRendererInSource,fixOutOfBoundUVs);		
		return mesh;
	}
	
	public override Mesh AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOs, bool disableRendererInSource, bool fixOutOfBoundUVs){
		if (resultSceneObject == null){
			resultSceneObject = new GameObject("CombinedMesh-" + name);	
		}
		_update_MeshCombiner();
		Mesh mesh = meshCombiner.AddDeleteGameObjectsByID(gos,deleteGOs,disableRendererInSource,fixOutOfBoundUVs);		
		return mesh;
	}	
	
	public override bool CombinedMeshContains(GameObject go){return meshCombiner.CombinedMeshContains(go);}
	public override void UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true,
									    bool updateUV = false, bool updateUV1 = false, bool updateUV2 = false,
										bool updateColors = false, bool updateSkinningInfo = false){
		_update_MeshCombiner();
		meshCombiner.UpdateGameObjects(gos,recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV1, updateUV2, updateColors, updateSkinningInfo);
	}
	public override void Apply(MeshCombiner.GenerateUV2Delegate uv2GenerationMethod=null){
		_update_MeshCombiner();
		meshCombiner.Apply();
	}
	
	public override void Apply(bool triangles,
					  bool vertices,
					  bool normals,
					  bool tangents,
					  bool uvs,
					  bool colors,
					  bool uv1,
					  bool uv2,
					  bool bones=false,
					  MeshCombiner.GenerateUV2Delegate uv2GenerationMethod=null){
		_update_MeshCombiner();
		meshCombiner.Apply(triangles,vertices,normals,tangents,uvs,colors,uv1,uv2,bones);
	}

	public void UpdateSkinnedMeshApproximateBounds(){
		meshCombiner.UpdateSkinnedMeshApproximateBounds();
	}

	public void UpdateSkinnedMeshApproximateBoundsFromBones(){
		meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBones();
	}

	public void UpdateSkinnedMeshApproximateBoundsFromBounds(){
		meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBounds();
	}
	
	void _update_MeshCombiner(){
		meshCombiner.name = name;
		meshCombiner.textureBakeResults = textureBakeResults;
		meshCombiner.resultSceneObject = resultSceneObject;
		meshCombiner.renderType = renderType;
		meshCombiner.outputOption = outputOption;
		meshCombiner.lightmapOption = lightmapOption;
		meshCombiner.doNorm = doNorm;
		meshCombiner.doTan = doTan;
		meshCombiner.doCol = doCol;	
		meshCombiner.doUV = doUV;
		meshCombiner.doUV1 = doUV1;		
	}
}
