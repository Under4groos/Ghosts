using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000A7 RID: 167
	public class FleshFleeState : NPCState<FleshNPC>
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00021945 File Offset: 0x0001FB45
		private float MaxCircleRange
		{
			get
			{
				return 1000f;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0002194C File Offset: 0x0001FB4C
		private float MinCircleRange
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00021953 File Offset: 0x0001FB53
		public FleshFleeState(FleshNPC host) : base(host)
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0002197C File Offset: 0x0001FB7C
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000219D5 File Offset: 0x0001FBD5
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00021A14 File Offset: 0x0001FC14
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				if (!this.Host.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.IdleState);
					return;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateCirclingStatus();
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00021AA4 File Offset: 0x0001FCA4
		private void UpdateCirclingStatus()
		{
			if (this.timeSinceLastTick < 0.2f)
			{
				return;
			}
			if (this.Host.Position.AlmostEqual(this.targetPoint, 20f))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ChooseCirclePoint();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastTick = 0f;
			if (this.CanStopFleeing())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.Reset();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			if (this.TimeSinceEntered > 10f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00021B75 File Offset: 0x0001FD75
		private bool CanStopFleeing()
		{
			if (this.Host.TargetingComponent.CanSeeTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceCouldSeeTarget = 0f;
				return false;
			}
			return this.TimeSinceCouldSeeTarget > 3f;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00021BB4 File Offset: 0x0001FDB4
		private void ChooseCirclePoint()
		{
			Rotation rotation = Rotation.LookAt(this.Host.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 dir = rotation.RotateAroundAxis(Vector3.Up, Rand.Float(-70f, 70f)).Forward;
			float length = Rand.Float(this.MinCircleRange, this.MaxCircleRange);
			Vector3 newPos = this.Host.Position + dir * length;
			Vector3? point = NavMesh.GetClosestPoint(newPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.targetPoint = (point ?? newPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.targetPoint;
		}

		// Token: 0x04000279 RID: 633
		private TimeSince TimeSinceCouldSeeTarget = 0f;

		// Token: 0x0400027A RID: 634
		private Vector3 targetPoint;

		// Token: 0x0400027B RID: 635
		private TimeSince timeSinceLastTick = 0f;
	}
}
