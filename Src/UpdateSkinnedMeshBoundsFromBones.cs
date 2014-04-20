using UnityEngine;
using System.Collections;
using DigitalOpus.MB.Core;

public class UpdateSkinnedMeshBoundsFromBones : MonoBehaviour {
    SkinnedMeshRenderer smr;
	Transform[] bones;
     
	void Start () {
		smr = GetComponent<SkinnedMeshRenderer>();
		if (smr == null){
			Debug.LogError("Need to attach UpdateSkinnedMeshBoundsFromBones script to an object with a SkinnedMeshRenderer component attached.");
			return;
		}
		bones = smr.bones;
		smr.updateWhenOffscreen = true;
		smr.updateWhenOffscreen = false;
    }
    
	void Update () {
        if (smr != null){
			MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBonesStatic(bones,smr);
		}
	}
}
