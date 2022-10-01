using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200007A RID: 122
	public class BoarCircleState : NPCState<BoarNPC>
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001A29C File Offset: 0x0001849C
		private float MaxCircleRange
		{
			get
			{
				return 1300f;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001A2A3 File Offset: 0x000184A3
		private float MinCircleRange
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001A2AA File Offset: 0x000184AA
		public BoarCircleState(BoarNPC host) : base(host)
		{
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001A2E4 File Offset: 0x000184E4
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.circleCount = Rand.Int(0, 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001A34F File Offset: 0x0001854F
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001A390 File Offset: 0x00018590
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
			this.Host.Speed = this.Host.GetWishSpeed();
			if (this.Host.TargetingComponent.DistanceToTarget < 600f && this.TimeSinceEntered > 3f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChargeState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateCirclingStatus();
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001A480 File Offset: 0x00018680
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

		// Token: 0x06000596 RID: 1430 RVA: 0x0001A520 File Offset: 0x00018720
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

		// Token: 0x040001FA RID: 506
		private TimeSince TimeSinceCouldSeeTarget = 0f;

		// Token: 0x040001FB RID: 507
		private int circleCount;

		// Token: 0x040001FC RID: 508
		private Vector3 targetPoint;

		// Token: 0x040001FD RID: 509
		private TimeSince timeSinceLastThreatCalc = 0f;

		// Token: 0x040001FE RID: 510
		private TimeSince timeSinceLastCircleCheck = 0f;
	}
}
