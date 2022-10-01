using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000BF RID: 191
	public class PseudoDogFleeState : NPCState<PseudoDogNPC>
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00024B3D File Offset: 0x00022D3D
		private float FleeTime
		{
			get
			{
				return 15f;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00024B44 File Offset: 0x00022D44
		private float MaxCircleRange
		{
			get
			{
				return 1300f;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00024B4B File Offset: 0x00022D4B
		private float MinCircleRange
		{
			get
			{
				return 600f;
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00024B52 File Offset: 0x00022D52
		public PseudoDogFleeState(PseudoDogNPC host) : base(host)
		{
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00024B7C File Offset: 0x00022D7C
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound("pdog.aggro");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00024BEB File Offset: 0x00022DEB
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00024C2C File Offset: 0x00022E2C
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

		// Token: 0x0600084E RID: 2126 RVA: 0x00024CBC File Offset: 0x00022EBC
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
			if (this.TimeSinceEntered > this.FleeTime)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChargeState);
				return;
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00024D90 File Offset: 0x00022F90
		private bool CanStopFleeing()
		{
			if (this.Host.TargetingComponent.ValidateTarget() && this.Host.TargetingComponent.CanSeeTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceCouldSeeTarget = 0f;
				return false;
			}
			return this.TimeSinceCouldSeeTarget > 3f;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00024DEC File Offset: 0x00022FEC
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

		// Token: 0x040002B9 RID: 697
		private TimeSince TimeSinceCouldSeeTarget = 0f;

		// Token: 0x040002BA RID: 698
		private Vector3 targetPoint;

		// Token: 0x040002BB RID: 699
		private TimeSince timeSinceLastTick = 0f;
	}
}
