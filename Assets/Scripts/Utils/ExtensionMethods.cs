using UnityEngine;
using Random = UnityEngine.Random;

public static class ExtensionMethods
{
	public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
	{
		float fromAbs = from - fromMin;
		float fromMaxAbs = fromMax - fromMin;

		float normal = fromAbs / fromMaxAbs;

		float toMaxAbs = toMax - toMin;
		float toAbs = toMaxAbs * normal;

		float to = toAbs + toMin;

		return to;
	}

	public static Vector3 SpawnAroundCircle(this Transform t, Transform center, float minX, float maxX, float minZ, float maxZ)
	{
		float radiusX = Random.Range(minX, maxX);
		float radiusZ = Random.Range(minZ, maxZ);
		Vector3 randomPos = Random.insideUnitSphere * radiusZ;
		randomPos += center.position;
		randomPos.y = 0f;

		Vector3 direction = randomPos - center.position;
		direction.Normalize();

		float dotProduct = Vector3.Dot(center.forward, direction);
		float dotProductAngle = Mathf.Acos(dotProduct / center.forward.magnitude * direction.magnitude);

		randomPos.x = Mathf.Cos(dotProductAngle) * radiusX + center.position.x;
		randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radiusZ + center.position.z;
		return randomPos;
	}
}
