using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FruitBowl.Lemon {
	/// <summary>
	/// Base Class for managing 2D Paths	
	/// </summary>
	public class LemonPath : MeasuredPath {


		public LemonPath(bool closed = false) {
			Init(closed);
		}

		public LemonPath(List<Vector3> path, bool closed = false) {
			Init(closed);
			AddPointRange(path);
		}

		// TODO: use Oriented Point
		public Vector3 GetPoint(float position) {
			float p = Mathf.Clamp01(position);
			for (int i = 0; i < segmentCount; i++) {
				Pair<float> minMax = GetNormLengthSegmentPair(i);
				if (p >= minMax.a && p <= minMax.b) {
					float f = Mathf.InverseLerp(minMax.a, minMax.b, p);
					Pair<Vector3> points = GetPathSegment(i);
					return Vector3.Lerp(points.a, points.b, f);
				}
			}
			Debug.LogError("Error with getting Point on path! Unbelievable!");
			return new Vector3(0,0);
		}

		// FIXME: take in consideration fact, that this could be closed
		public List<int> GetIndicesBetween(float a, float b) {
			float mn = Mathf.Min(a, b);
			float mx = Mathf.Max(a, b);

			List<int> points = new List<int>();

			for (int i = 0; i < lengths.Count; i++) {
				float l = lengths[i] / length;
				if (mn <= l && l <= mx) {
					points.Add(i);
				}
			}

			if (b < a) {
				points.Reverse();
			}
			return points;
		}
	}
}