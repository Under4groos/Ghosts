using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000044 RID: 68
	public class CoverFinder
	{
		// Token: 0x060002EB RID: 747 RVA: 0x00011818 File Offset: 0x0000FA18
		public bool FindCoverInDirection(Vector3 startPos, Vector3 dir, float range)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dir = dir.Normal;
			CoverFinder.CoverRayResult result = this.FindNewLocationWithRays(startPos, dir, range, 3);
			if (result.Position != null)
			{
				Vector3? from_fixed = NavMesh.GetClosestPoint(startPos);
				Vector3? tofixed = NavMesh.GetClosestPoint(result.Position.Value);
				if (from_fixed != null && tofixed != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Path.Clear();
					if (NavMesh.BuildPath(from_fixed.Value, tofixed.Value, this.Path))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000118A4 File Offset: 0x0000FAA4
		[Description("This is about finding cover FROM something. For instance, cover from the player. We fire numRays out away from the targets EyePosition in a circle, and find somewhere out of LOS that is blocked.")]
		public bool FindCoverFromTarget(Entity target, Vector3 fleePosition, float range, float downAngle = 0f, int numRays = 8, float searchDegrees = 360f)
		{
			Rotation startRotation = target.Rotation;
			List<Vector3> validPositions = new List<Vector3>();
			float degPerRay = searchDegrees / (float)numRays;
			for (int i = 0; i < numRays; i++)
			{
				Rotation rayRot = startRotation;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				rayRot = rayRot.RotateAroundAxis(Vector3.Up, degPerRay * (float)i);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				rayRot = rayRot.RotateAroundAxis(Vector3.Right, downAngle);
				CoverFinder.CoverRayResult result = this.FindNewLocationWithRays(target.EyePosition, rayRot.Forward, range, 3);
				if (result.Position != null)
				{
					if (result.IsInCover)
					{
						Vector3? from_fixed = NavMesh.GetClosestPoint(fleePosition);
						Vector3? tofixed = NavMesh.GetClosestPoint(result.Position.Value);
						if (from_fixed != null && tofixed != null)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							this.Path.Clear();
							if (NavMesh.BuildPath(from_fixed.Value, tofixed.Value, this.Path))
							{
								return true;
							}
						}
					}
					else if (NavMesh.GetPointWithinRadius(result.Position.Value, 0f, 350f) != null)
					{
						validPositions.Add(result.Position.Value);
					}
				}
			}
			if (validPositions.Count > 0)
			{
				Vector3 defPos = validPositions[Rand.Int(0, validPositions.Count - 1)];
				Vector3? from_fixed2 = NavMesh.GetClosestPoint(fleePosition);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Path.Clear();
				if (NavMesh.BuildPath(from_fixed2.Value, defPos, this.Path))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00011A1C File Offset: 0x0000FC1C
		private CoverFinder.CoverRayResult FindNewLocationWithRays(Vector3 startPos, Vector3 dir, float range, int numExtraRays)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dir = dir.Normal;
			Rotation.LookAt(dir.Normal, Vector3.Up);
			float num = 130f / (float)numExtraRays;
			Vector3 endPos = startPos + dir * range;
			TraceResult tr = this.FireRay(startPos, endPos, false);
			if (tr.Hit)
			{
				CoverFinder.CoverRayResult result = this.FindLocationBehindObstacleUsingTrace(tr.EndPosition, dir, range / 2f, numExtraRays);
				if (result != null)
				{
					return result;
				}
			}
			for (int i = 0; i < numExtraRays; i++)
			{
			}
			return new CoverFinder.CoverRayResult(NavMesh.GetPointWithinRadius(tr.EndPosition, 0f, 50f), false, tr);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		private TraceResult FireRay(Vector3 startPos, Vector3 endPos, bool ignoreWorld = false)
		{
			return Trace.Ray(startPos, endPos).WorldOnly().Radius(1f).Run();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00011AEC File Offset: 0x0000FCEC
		private TraceResult FireRayIgnoreWorld(Vector3 startPos, Vector3 endPos, bool ignoreWorld = false)
		{
			return Trace.Ray(startPos, endPos).Radius(1f).Run();
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00011B18 File Offset: 0x0000FD18
		private CoverFinder.CoverRayResult FindLocationBehindObstacleUsingTrace(Vector3 startPos, Vector3 dir, float checkRange, int numExtraRays)
		{
			TraceResult firstTrace = this.FireRayIgnoreWorld(startPos + dir * 20f, startPos + dir * checkRange, false);
			if (!firstTrace.Hit)
			{
				return new CoverFinder.CoverRayResult(NavMesh.GetPointWithinRadius(firstTrace.EndPosition, 0f, 600f), true, firstTrace);
			}
			return null;
		}

		// Token: 0x040000EB RID: 235
		public List<Vector3> Path = new List<Vector3>();

		// Token: 0x020001F5 RID: 501
		private class CoverRayResult
		{
			// Token: 0x06001841 RID: 6209 RVA: 0x00064C21 File Offset: 0x00062E21
			public CoverRayResult(Vector3? endPos, bool inCover, TraceResult tr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Position = endPos;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsInCover = inCover;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TResult = tr;
			}

			// Token: 0x04000816 RID: 2070
			public bool IsInCover;

			// Token: 0x04000817 RID: 2071
			public TraceResult TResult;

			// Token: 0x04000818 RID: 2072
			public Vector3? Position;
		}
	}
}
