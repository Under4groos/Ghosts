using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Debug;
using Sandbox.Internal;

// Token: 0x0200000B RID: 11
public class NavPath
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000064 RID: 100 RVA: 0x000064E5 File Offset: 0x000046E5
	public bool IsEmpty
	{
		get
		{
			return this.Points.Count <= 1;
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000064F8 File Offset: 0x000046F8
	public void Update(Vector3 from, Vector3 to)
	{
		bool needsBuild = false;
		if (!this.TargetPosition.AlmostEqual(to, 5f))
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetPosition = to;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			needsBuild = true;
		}
		if (needsBuild)
		{
			Vector3? from_fixed = NavMesh.GetClosestPoint(from);
			Vector3? tofixed = NavMesh.GetClosestPoint(to);
			if (from_fixed != null && tofixed != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Points.Clear();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				NavMesh.BuildPath(from_fixed.Value, tofixed.Value, this.Points);
			}
		}
		if (this.Points.Count <= 1)
		{
			return;
		}
		from - this.Points[0];
		Vector3 deltaToNext = from - this.Points[1];
		Vector3 deltaNormal = (this.Points[1] - this.Points[0]).Normal;
		if (deltaToNext.WithZ(0f).Length < 20f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Points.RemoveAt(0);
			return;
		}
		if (deltaToNext.Normal.Dot(deltaNormal) >= 1f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Points.RemoveAt(0);
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00006634 File Offset: 0x00004834
	public float Distance(int point, Vector3 from)
	{
		if (this.Points.Count <= point)
		{
			return float.MaxValue;
		}
		return this.Points[point].WithZ(from.z).Distance(from);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000667C File Offset: 0x0000487C
	public Vector3 GetDirection(Vector3 position)
	{
		if (this.Points.Count == 1)
		{
			return (this.Points[0] - position).WithZ(0f).Normal;
		}
		return (this.Points[1] - position).WithZ(0f).Normal;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000066E8 File Offset: 0x000048E8
	public static List<Vector3> GeneratePointsFromPath(Vector3 from, GenericPathEntity pathEntity)
	{
		List<Vector3> points = new List<Vector3>();
		if (pathEntity.PathNodes.Count <= 1)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Error("Tried to generate points from a path with 1 or 0 nodes!");
		}
		Vector3 initialPosition = NavMesh.GetClosestPoint(from).Value;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		points.Add(initialPosition);
		Vector3 lastValue = initialPosition;
		foreach (BasePathNode basePathNode in pathEntity.PathNodes)
		{
			Vector3 pathPos = NavMesh.GetClosestPoint(basePathNode.Position + pathEntity.Position).Value;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			points.AddRange(NavMesh.BuildPath(lastValue, pathPos));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			lastValue = pathPos;
		}
		return points;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x000067B4 File Offset: 0x000049B4
	public void DebugDraw(float time, float opacity = 1f)
	{
		Draw draw = Draw.ForSeconds(time);
		Vector3 lift = Vector3.Up * 2f;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		draw.WithColor(Color.White.WithAlpha(opacity)).Circle(lift + this.TargetPosition, Vector3.Up, 20f, 32, 360f);
		int i = 0;
		Vector3 lastPoint = Vector3.Zero;
		foreach (Vector3 point in this.Points)
		{
			if (i > 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				draw.WithColor((i == 1) ? Color.Green.WithAlpha(opacity) : Color.Cyan.WithAlpha(opacity)).Arrow(lastPoint + lift, point + lift, Vector3.Up, 5f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			lastPoint = point;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			i++;
		}
	}

	// Token: 0x04000027 RID: 39
	public Vector3 TargetPosition;

	// Token: 0x04000028 RID: 40
	public List<Vector3> Points = new List<Vector3>();
}
