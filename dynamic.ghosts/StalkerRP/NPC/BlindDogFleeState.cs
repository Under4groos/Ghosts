using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200009C RID: 156
	public class BlindDogFleeState : NPCState<BlindDogNPC>
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x000200DD File Offset: 0x0001E2DD
		private float fleeTime
		{
			get
			{
				return 15f;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x000200E4 File Offset: 0x0001E2E4
		private float maxCircleRange
		{
			get
			{
				return 1300f;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000200EB File Offset: 0x0001E2EB
		private float minCircleRange
		{
			get
			{
				return 600f;
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000200F2 File Offset: 0x0001E2F2
		public BlindDogFleeState(BlindDogNPC host) : base(host)
		{
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0002011C File Offset: 0x0001E31C
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.PanicSound);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00020191 File Offset: 0x0001E391
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000201D0 File Offset: 0x0001E3D0
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

		// Token: 0x06000704 RID: 1796 RVA: 0x00020260 File Offset: 0x0001E460
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
			if (this.TimeSinceEntered > this.fleeTime)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChargeState);
				return;
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00020334 File Offset: 0x0001E534
		private bool CanStopFleeing()
		{
			if (this.Host.TargetingComponent.ValidateTarget() && this.Host.TargetingComponent.CanSeeTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceCouldSeeTarget = 0f;
				return false;
			}
			return this.timeSinceCouldSeeTarget > 3f;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00020390 File Offset: 0x0001E590
		private void ChooseCirclePoint()
		{
			Rotation rotation = Rotation.LookAt(this.Host.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 dir = rotation.RotateAroundAxis(Vector3.Up, Rand.Float(-70f, 70f)).Forward;
			float length = Rand.Float(this.minCircleRange, this.maxCircleRange);
			Vector3 newPos = this.Host.Position + dir * length;
			Vector3? point = NavMesh.GetClosestPoint(newPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.targetPoint = (point ?? newPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.targetPoint;
		}

		// Token: 0x0400025F RID: 607
		private TimeSince timeSinceCouldSeeTarget = 0f;

		// Token: 0x04000260 RID: 608
		private Vector3 targetPoint;

		// Token: 0x04000261 RID: 609
		private TimeSince timeSinceLastTick = 0f;
	}
}
