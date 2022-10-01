using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000BD RID: 189
	public class PseudoDogCircleState : NPCState<PseudoDogNPC>
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000244CD File Offset: 0x000226CD
		private float MaxCircleRange
		{
			get
			{
				return 1300f;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x000244D4 File Offset: 0x000226D4
		private float MinCircleRange
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000244DB File Offset: 0x000226DB
		public PseudoDogCircleState(PseudoDogNPC host) : base(host)
		{
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00024504 File Offset: 0x00022704
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.circleCount = Rand.Int(0, 2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002456F File Offset: 0x0002276F
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000245B0 File Offset: 0x000227B0
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateCirclingStatus();
			if (this.ShouldForceCharge())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChargeState);
				return;
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0002463F File Offset: 0x0002283F
		private bool ShouldForceCharge()
		{
			return this.Host.TargetingComponent.DistanceToTarget < this.Host.ChargeState.ForceChargeRange;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00024664 File Offset: 0x00022864
		private void UpdateCirclingStatus()
		{
			if (this.timeSinceLastCircleCheck < 0.2f)
			{
				return;
			}
			if (this.Host.TargetingComponent.DistanceToTarget < 350f && this.TimeSinceEntered > 3f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChargeState);
				return;
			}
			if (this.Host.Position.AlmostEqual(this.targetPoint, 20f))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.circleCount--;
				if (this.circleCount > 0)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.ChooseCirclePoint();
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.ChargeState);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastCircleCheck = 0f;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002474C File Offset: 0x0002294C
		private void ChooseCirclePoint()
		{
			Vector3 tPos = base.Target.Position;
			Rotation rotation = Rotation.LookAt((this.Host.Position - tPos).Normal);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 dir = rotation.RotateAroundAxis(Vector3.Up, Rand.Float(-135f, 135f)).Forward;
			float length = Rand.Float(this.MinCircleRange, this.MaxCircleRange);
			Vector3? point = NavMesh.GetClosestPoint(tPos + dir * length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.targetPoint = (point ?? tPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.targetPoint;
		}

		// Token: 0x040002B0 RID: 688
		private TimeSince TimeSinceCouldSeeTarget = 0f;

		// Token: 0x040002B1 RID: 689
		private int circleCount;

		// Token: 0x040002B2 RID: 690
		private Vector3 targetPoint;

		// Token: 0x040002B3 RID: 691
		private TimeSince timeSinceLastCircleCheck = 0f;
	}
}
