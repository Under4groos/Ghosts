using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.NPC;

// Token: 0x0200000C RID: 12
public class NavSteer
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600006B RID: 107 RVA: 0x000068DF File Offset: 0x00004ADF
	// (set) Token: 0x0600006C RID: 108 RVA: 0x000068E7 File Offset: 0x00004AE7
	private protected global::NavPath Path { protected get; private set; }

	// Token: 0x0600006D RID: 109 RVA: 0x000068F0 File Offset: 0x00004AF0
	public NavSteer()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Path = new global::NavPath();
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00006908 File Offset: 0x00004B08
	public virtual void Tick(Vector3 currentPosition)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Path.Update(currentPosition, this.Target);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Output.Finished = this.Path.IsEmpty;
		if (this.Output.Finished)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Output.Direction = Vector3.Zero;
			return;
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Output.Direction = this.Path.GetDirection(currentPosition);
		Vector3 avoid = this.GetAvoidance(currentPosition, 500f);
		if (!avoid.IsNearlyZero(1E-45f))
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Output.Direction = (this.Output.Direction + avoid).Normal;
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000069CC File Offset: 0x00004BCC
	private Vector3 GetAvoidance(Vector3 position, float radius)
	{
		Vector3 position2 = position + this.Output.Direction * radius * 0.5f;
		float objectRadius = 100f;
		Vector3 avoidance = default(Vector3);
		foreach (Entity ent in Entity.FindInSphere(position2, radius))
		{
			if ((ent is NPCBase || ent is Player || ent is Prop) && !ent.IsWorld)
			{
				Vector3 delta = (position - ent.Position).WithZ(0f);
				float closeness = delta.Length;
				if (closeness >= 0.001f)
				{
					float thrust = ((objectRadius - closeness) / objectRadius).Clamp(0f, 1f);
					if (thrust > 0f)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						avoidance += delta.Normal * thrust * thrust;
					}
				}
			}
		}
		return avoidance;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00006AD8 File Offset: 0x00004CD8
	public void OverridePath(List<Vector3> navPoints)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Path.Points = navPoints;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00006AEB File Offset: 0x00004CEB
	public virtual void DebugDrawPath()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Path.DebugDraw(0.1f, 0.1f);
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000072 RID: 114 RVA: 0x00006B07 File Offset: 0x00004D07
	// (set) Token: 0x06000073 RID: 115 RVA: 0x00006B0F File Offset: 0x00004D0F
	public Vector3 Target { get; set; }

	// Token: 0x0400002B RID: 43
	public NavSteer.NavSteerOutput Output;

	// Token: 0x020001F0 RID: 496
	public struct NavSteerOutput
	{
		// Token: 0x0400080A RID: 2058
		public bool Finished;

		// Token: 0x0400080B RID: 2059
		public Vector3 Direction;
	}
}
