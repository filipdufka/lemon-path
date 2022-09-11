using System.Collections.Generic;
using UnityEngine;

namespace FruitBowl.Lemon {
	enum ShapeAdditions
    {
		Union, Subtraction, Intersection 
    }
	
	/// <summary>
	/// Lemon Shape consist of multiple closed LemonPaths.
	/// </summary>
	/// 

	public class LemonShape {

		LemonBounds bounds = new LemonBounds();
		List<(LemonPath,ShapeAdditions)> pathList = new List<(LemonPath, ShapeAdditions)>();

		public void AddPathUnion(LemonPath path) { 
			pathList.Add((path,ShapeAdditions.Union));
		}

	}
}