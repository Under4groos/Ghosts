using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200009B RID: 155
	public class BlindDogCircleState : NPCState<BlindDogNPC>
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001FD49 File Offset: 0x0001DF49
		private float MaxCircleRange
		{
			get
			{
				return 1300f;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001FD50 File Offset: 0x0001DF50
		private float MinCircleRange
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001FD57 File Offset: 0x0001DF57
		public BlindDogCircleState(BlindDogNPC host) : base(host)
		{
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001FD90 File Offset: 0x0001DF90
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

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001FDFB File Offset: 0x0001DFFB
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001FE3C File Offset: 0x0001E03C
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
			if (this.Host.TargetingComponent.DistanceToTarget < 350f && this.TimeSinceEntered > 3f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChargeState);
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

		// Token: 0x060006FA RID: 1786 RVA: 0x0001FF52 File Offset: 0x0001E152
		private bool ShouldForceCharge()
		{
			return this.Host.TargetingComponent.DistanceToTarget < this.Host.ChargeState.ForceChargeRange;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001FF78 File Offset: 0x0001E178
		private void UpdateCirclingStatus()
		{
			if (this.timeSinceLastCircleCheck < 0.2f)
			{
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

		// Token: 0x060006FC RID: 1788 RVA: 0x00020018 File Offset: 0x0001E218
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

		// Token: 0x0400025A RID: 602
		private TimeSince TimeSinceCouldSeeTarget = 0f;

		// Token: 0x0400025B RID: 603
		private int circleCount;

		// Token: 0x0400025C RID: 604
		private Vector3 targetPoint;

		// Token: 0x0400025D RID: 605
		private TimeSince timeSinceLastThreatCalc = 0f;

		// Token: 0x0400025E RID: 606
		private TimeSince timeSinceLastCircleCheck = 0f;
	}
}
