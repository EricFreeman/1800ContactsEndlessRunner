using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public static class Constants
    {
	    public static readonly List<Color> TreeFurthestAway = new List<Color>
	    {
			new Color(.346f, .158f, .554f),
			new Color(.396f, .208f, .604f),
			new Color(.446f, .258f, .654f),
	    };
		public static readonly List<Color> TreeMiddle = new List<Color>
	    {
			new Color(.126f, .017f, .342f),
			new Color(.176f, .067f, .392f),
			new Color(.226f, .117f, .442f),
	    };
		public static readonly List<Color> TreeClosest = new List<Color>
	    {
		    new Color(0, 0, 0),
			new Color(0.05f, 0.05f, 0.05f),
			new Color(0.1f, 0.1f, 0.1f)
	    };
    }
}