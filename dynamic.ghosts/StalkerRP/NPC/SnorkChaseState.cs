using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000DC RID: 220
	public class SnorkChaseState : NPCState<SnorkNPC>
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x00028F69 File Offset: 0x00027169
		public SnorkChaseState(SnorkNPC host) : base(host)
		{
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00028FA0 File Offset: 0x000271A0
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.Resource.RunSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound("snork.aggro");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002902F File Offset: 0x0002722F
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00029070 File Offset: 0x00027270
		public override void Update(float deltaTime)
		{
			if (this.timeSinceLastThreatCalc > 0.3f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceLastThreatCalc = 0f;
			}
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			if (this.Host.LeapState.CanLeap())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.LeapState);
				return;
			}
			if (this.Host.MeleeState.CanDoMelee())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.MeleeState);
				return;
			}
			if (this.Host.Position.AlmostEqual(this.retreatPos, 15f) || this.Host.Position.Distance(this.retreatPos) > this.Host.MaxLeapRange || this.timeSinceLastCirclePoint > this.maxTimeBetweenNewPoints)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ChooseCirclePoint();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetSteerTarget();
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000291E0 File Offset: 0x000273E0
		private void SetSteerTarget()
		{
			Vector3 targetPos = this.Host.Target.Position;
			Vector3 dir = (this.Host.Position - targetPos).Normal;
			if (this.Host.TargetingComponent.DistanceToTarget < 80f && this.Host.IsStuck)
			{
				this.Host.Steer.Target = targetPos + dir * 150f;
				return;
			}
			if (this.Host.TimeSinceLastStuck > 1f)
			{
				this.Host.Steer.Target = this.retreatPos;
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002928C File Offset: 0x0002748C
		private void ChooseCirclePoint()
		{
			Vector3 point = base.Target.Position + Vector3.Random.WithZ(0f).Normal * Rand.Float(this.Host.MinLeapRange, this.Host.MaxLeapRange);
			Vector3? navPoint = NavMesh.GetClosestPoint(point);
			if (navPoint != null)
			{
				this.retreatPos = navPoint.Value;
			}
			else
			{
				this.retreatPos = point;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.retreatPos;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastCirclePoint = 0f;
		}

		// Token: 0x04000309 RID: 777
		private Vector3 retreatPos;

		// Token: 0x0400030A RID: 778
		private float maxTimeBetweenNewPoints = 1f;

		// Token: 0x0400030B RID: 779
		private TimeSince timeSinceLastCirclePoint = 0f;

		// Token: 0x0400030C RID: 780
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
