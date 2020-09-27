
using UnityEngine;
using UnityEngine.Rendering;

public class BloodVFXSpawner : MonoBehaviour
{

    public bool InfiniteDecal;
    public Light DirLight;
    public bool isVR = true;
    public GameObject BloodAttach;
    public GameObject[] BloodFX;


    Transform GetNearestObject(Transform hit, Vector3 hitPos)
    {
        var closestPos = 100f;
        Transform closestBone = null;
        var childs = hit.GetComponentsInChildren<Transform>();

        foreach (var child in childs)
        {
            var dist = Vector3.Distance(child.position, hitPos);
            if (dist < closestPos)
            {
                closestPos = dist;
                closestBone = child;
            }
        }

        var distRoot = Vector3.Distance(hit.position, hitPos);
        if (distRoot < closestPos)
        {
            closestPos = distRoot;
            closestBone = hit;
        }
        return closestBone;
    }

    public Vector3 direction;
    int effectIdx;
    public void SpawnBlood(Transform spawnTransform)
    {
        float angle = Mathf.Atan2(spawnTransform.forward.x, spawnTransform.forward.z) * Mathf.Rad2Deg + 180;

        //var effectIdx = Random.Range(0, BloodFX.Length);
        if (effectIdx == BloodFX.Length) effectIdx = 0;

        var instance = Instantiate(BloodFX[effectIdx], spawnTransform.position, Quaternion.Euler(0, angle + 90, 0));
        effectIdx++;

        var settings = instance.GetComponent<BFX_BloodSettings>();
        settings.DecalLiveTimeInfinite = InfiniteDecal;
        settings.LightIntencityMultiplier = DirLight.intensity;

        //var nearestBone = GetNearestObject(spawnTransform.root, spawnTransform.position);
        //if (nearestBone != null)
        //{
        //    var attachBloodInstance = Instantiate(BloodAttach, nearestBone);
        //    var bloodT = attachBloodInstance.transform;
        //    bloodT.position = spawnTransform.point;
        //    bloodT.localRotation = Quaternion.identity;
        //    bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.0f);
        //    bloodT.LookAt(spawnTransform.point + hit.normal, direction);
        //    bloodT.Rotate(90, 0, 0);
        //    Destroy(attachBloodInstance, 20);
        //}

    }


    public float CalculateAngle(Vector3 from, Vector3 to)
    {

        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;

    }

}
