using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200007B RID: 123
	public class BoarFleeState : NPCState<BoarNPC>
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001A5E5 File Offset: 0x000187E5
		private float MaxCircleRange
		{
			get
			{
				return 1000f;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001A5EC File Offset: 0x000187EC
		private float MinCircleRange
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001A5F3 File Offset: 0x000187F3
		public BoarFleeState(BoarNPC host) : base(host)
		{
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001A61C File Offset: 0x0001881C
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

		// Token: 0x0600059B RID: 1435 RVA: 0x0001A675 File Offset: 0x00018875
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001A6B4 File Offset: 0x000188B4
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

		// Token: 0x0600059D RID: 1437 RVA: 0x0001A744 File Offset: 0x00018944
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

		// Token: 0x0600059E RID: 1438 RVA: 0x0001A815 File Offset: 0x00018A15
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

		// Token: 0x0600059F RID: 1439 RVA: 0x0001A854 File Offset: 0x00018A54
		private void ChooseCirclePoint()
		{
			Rotation rotation = Rotation.LookAt(base.Target.Position);
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

		// Token: 0x040001FF RID: 511
		private TimeSince TimeSinceCouldSeeTarget = 0f;

		// Token: 0x04000200 RID: 512
		private Vector3 targetPoint;

		// Token: 0x04000201 RID: 513
		private TimeSince timeSinceLastTick = 0f;
	}
}
