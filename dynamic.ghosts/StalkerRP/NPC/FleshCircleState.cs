using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000A6 RID: 166
	public class FleshCircleState : NPCState<FleshNPC>
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x000215FA File Offset: 0x0001F7FA
		private float MaxCircleRange
		{
			get
			{
				return 1300f;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00021601 File Offset: 0x0001F801
		private float MinCircleRange
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00021608 File Offset: 0x0001F808
		public FleshCircleState(FleshNPC host) : base(host)
		{
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00021644 File Offset: 0x0001F844
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

		// Token: 0x0600075B RID: 1883 RVA: 0x000216AF File Offset: 0x0001F8AF
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000216F0 File Offset: 0x0001F8F0
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
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000217E0 File Offset: 0x0001F9E0
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

		// Token: 0x0600075E RID: 1886 RVA: 0x00021880 File Offset: 0x0001FA80
		private void ChooseCirclePoint()
		{
			Vector3 tPos = base.Target.Position;
			Rotation rotation = Rotation.LookAt((tPos - this.Host.Position).Normal);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 dir = rotation.RotateAroundAxis(Vector3.Up, Rand.Float(-90f, 90f)).Forward;
			float length = Rand.Float(this.MinCircleRange, this.MaxCircleRange);
			Vector3? point = NavMesh.GetClosestPoint(tPos + dir * length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.targetPoint = (point ?? tPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.targetPoint;
		}

		// Token: 0x04000274 RID: 628
		private TimeSince TimeSinceCouldSeeTarget = 0f;

		// Token: 0x04000275 RID: 629
		private int circleCount;

		// Token: 0x04000276 RID: 630
		private Vector3 targetPoint;

		// Token: 0x04000277 RID: 631
		private TimeSince timeSinceLastThreatCalc = 0f;

		// Token: 0x04000278 RID: 632
		private TimeSince timeSinceLastCircleCheck = 0f;
	}
}
