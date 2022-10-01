using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200008E RID: 142
	public class ChimeraChaseState : ChimeraState
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x0001DAA1 File Offset: 0x0001BCA1
		public ChimeraChaseState(ChimeraNPC host) : base(host)
		{
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001DABC File Offset: 0x0001BCBC
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
			this.ShouldCircle = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001DB41 File Offset: 0x0001BD41
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001DB80 File Offset: 0x0001BD80
		public override void Update(float deltaTime)
		{
			if (this.timeSinceLastThreatCalc > 1f)
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
			this.Host.Speed = ((!this.Host.GetAnimParameterBool("bDamaged")) ? this.Host.Resource.RunSpeed : this.Host.Resource.CrippledSpeed);
			if (this.Host.LeapState.CanLeap())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.LeapState);
				return;
			}
			if (this.Host.TargetingComponent.DistanceToTarget < this.Host.MinLeapRange && !this.ShouldCircle)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ChooseCirclePoint();
				return;
			}
			if (this.Host.Position.AlmostEqual(this.RetreatPos, 15f) || base.Target.Position.Distance(this.RetreatPos) > this.Host.MaxLeapRange)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ChooseCirclePoint();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetSteerTarget();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001DD04 File Offset: 0x0001BF04
		private void SetSteerTarget()
		{
			Vector3 targetPos = this.Host.Target.Position;
			Vector3 normal = (this.Host.Position - targetPos).Normal;
			if (this.ShouldCircle)
			{
				this.Host.Steer.Target = this.RetreatPos;
				return;
			}
			this.Host.Steer.Target = this.Host.Target.Position;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001DD7C File Offset: 0x0001BF7C
		private void ChooseCirclePoint()
		{
			Vector3 point = base.Target.Position + Vector3.Random.WithZ(0f).Normal * Rand.Float(this.Host.MinLeapRange, this.Host.MaxLeapRange);
			Vector3? navPoint = NavMesh.GetClosestPoint(point);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShouldCircle = (this.Host.TargetingComponent.DistanceToTarget < this.Host.MinLeapRange);
			if (navPoint != null)
			{
				this.RetreatPos = navPoint.Value;
			}
			else
			{
				this.RetreatPos = point;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.RetreatPos;
		}

		// Token: 0x04000235 RID: 565
		private Vector3 RetreatPos;

		// Token: 0x04000236 RID: 566
		private bool ShouldCircle;

		// Token: 0x04000237 RID: 567
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
