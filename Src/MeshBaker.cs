using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Text;
using DigitalOpus.MB.Core;

public class MeshBaker : MeshBakerCommon {	
	
	[HideInInspector] public MeshCombiner meshCombiner = new MeshCombiner();
		
	public bool doUV2(){return meshCombiner.doUV2();}
	public Mesh GetMesh(){return meshCombiner.GetMesh();}
	public int GetLightmapIndex(){return meshCombiner.GetLightmapIndex();}
	
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
	
	public void BuildSceneMeshObject(){
		if (resultSceneObject == null){
			resultSceneObject = new GameObject("CombinedMesh-" + name);
		}
		_update_MeshCombiner();
		meshCombiner.buildSceneMeshObject(resultSceneObject, meshCombiner.GetMesh());
		//_update_MeshCombiner();
	}
	
	public override int GetNumObjectsInCombined(){
		return meshCombiner.GetNumObjectsInCombined();	
	}
	
	public override int GetNumVerticesFor(GameObject go){
		return meshCombiner.GetNumVerticesFor(go);
	}
	
	public override Mesh AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource, bool fixOutOfBoundUVs){
		if ((meshCombiner.outputOption == OutputOptions.bakeIntoSceneObject || (meshCombiner.outputOption == OutputOptions.bakeIntoPrefab && meshCombiner.renderType == MB_RenderType.skinnedMeshRenderer) )) BuildSceneMeshObject();
		_update_MeshCombiner();
		return meshCombiner.AddDeleteGameObjects(gos,deleteGOs,disableRendererInSource,fixOutOfBoundUVs);		
	}
	
	public override Mesh AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource, bool fixOutOfBoundUVs){
		if ((meshCombiner.outputOption == OutputOptions.bakeIntoSceneObject || (meshCombiner.outputOption == OutputOptions.bakeIntoPrefab && meshCombiner.renderType == MB_RenderType.skinnedMeshRenderer) )) BuildSceneMeshObject();
		_update_MeshCombiner();
		return meshCombiner.AddDeleteGameObjectsByID(gos,deleteGOinstanceIDs,disableRendererInSource,fixOutOfBoundUVs);		

	}
	
	public bool ShowHide(GameObject[] gos, GameObject[] deleteGOs){
		_update_MeshCombiner();
		return meshCombiner.ShowHideGameObjects(gos, deleteGOs);
	}
	
	public override bool CombinedMeshContains(GameObject go){return meshCombiner.CombinedMeshContains(go);}
	public override void UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true,
									    bool updateUV = false, bool updateUV1 = false, bool updateUV2 = false,
										bool updateColors = false, bool updateSkinningInfo = false){
		_update_MeshCombiner();
		meshCombiner.UpdateGameObjects(gos,recalcBounds,updateVertices, updateNormals, updateTangents, updateUV, updateUV1, updateUV2, updateColors, updateSkinningInfo);
	}
	public override void Apply(MeshCombiner.GenerateUV2Delegate uv2GenerationMethod=null){
		_update_MeshCombiner();
		meshCombiner.Apply(uv2GenerationMethod);
	}
	
	public void ApplyShowHide(){
		_update_MeshCombiner();
		meshCombiner.ApplyShowHide();		
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
		meshCombiner.Apply(triangles,vertices,normals,tangents,uvs,colors,uv1,uv2,bones,uv2GenerationMethod);
	}
	
	bool _ValidateForUpdateSkinnedMeshBounds(){
		if (outputOption == OutputOptions.bakeMeshAssetsInPlace){
			Debug.LogWarning("Can't UpdateSkinnedMeshApproximateBounds when output type is bakeMeshAssetsInPlace");
			return false;
		}
		if (resultSceneObject == null){
			Debug.LogWarning("Result Scene Object does not exist. No point in calling UpdateSkinnedMeshApproximateBounds.");
			return false;			
		}
		SkinnedMeshRenderer smr = resultSceneObject.GetComponentInChildren<SkinnedMeshRenderer>();	
		if (smr == null){
			Debug.LogWarning("No SkinnedMeshRenderer on result scene object.");
			return false;			
		}
		return true;
	}
	
	public void UpdateSkinnedMeshApproximateBounds(){
		if (_ValidateForUpdateSkinnedMeshBounds()){
			meshCombiner.UpdateSkinnedMeshApproximateBounds();
		}
	}

	public void UpdateSkinnedMeshApproximateBoundsFromBones(){
		if (_ValidateForUpdateSkinnedMeshBounds()){
			meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBones();
		}
	}

	public void UpdateSkinnedMeshApproximateBoundsFromBounds(){
		if (_ValidateForUpdateSkinnedMeshBounds()){
			meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBounds();
		}
	}
	
	public void _update_MeshCombiner(){
		
		meshCombiner.name = name;
		meshCombiner.textureBakeResults = textureBakeResults;
		meshCombiner.renderType = renderType;
		meshCombiner.outputOption = outputOption;
		meshCombiner.lightmapOption = lightmapOption;
		meshCombiner.doNorm = doNorm;
		meshCombiner.doTan = doTan;
		meshCombiner.doCol = doCol;	
		meshCombiner.doUV = doUV;
		meshCombiner.doUV1 = doUV1;
		if (resultSceneObject != null){
			Renderer r = resultSceneObject.GetComponentInChildren<Renderer>();
			if (renderType == MB_RenderType.skinnedMeshRenderer && !(r is SkinnedMeshRenderer)){
				meshCombiner.buildSceneMeshObject(resultSceneObject, meshCombiner.GetMesh());	
			} if (renderType == MB_RenderType.meshRenderer && !(r is MeshRenderer)){
				meshCombiner.buildSceneMeshObject(resultSceneObject, meshCombiner.GetMesh());
			}
			if (meshCombiner.LOG_LEVEL >= LogLevel.trace) Log.Trace("Copying settings from MeshBaker to MeshCombiner. Setting target renderer=" + meshCombiner.targetRenderer);			
			meshCombiner.targetRenderer = resultSceneObject.GetComponentInChildren<Renderer>();
		} else {
			if (meshCombiner.LOG_LEVEL >= LogLevel.trace) Log.Trace("Copying settings from MeshBaker to MeshCombiner. Setting target renderer=null");						
			meshCombiner.targetRenderer = null;
		}		
	}
}

