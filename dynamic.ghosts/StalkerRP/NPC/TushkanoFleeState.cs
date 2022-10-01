using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000E2 RID: 226
	public class TushkanoFleeState : NPCState<TushkanoNPC>
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0002A236 File Offset: 0x00028436
		private float fleeTime
		{
			get
			{
				return 4f;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0002A23D File Offset: 0x0002843D
		private float maxCircleRange
		{
			get
			{
				return 700f;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002A244 File Offset: 0x00028444
		private float minCircleRange
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002A24B File Offset: 0x0002844B
		public TushkanoFleeState(TushkanoNPC host) : base(host)
		{
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002A274 File Offset: 0x00028474
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

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002A2E9 File Offset: 0x000284E9
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002A328 File Offset: 0x00028528
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

		// Token: 0x060009CA RID: 2506 RVA: 0x0002A3B8 File Offset: 0x000285B8
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
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002A48A File Offset: 0x0002868A
		private bool CanStopFleeing()
		{
			if (this.Host.TargetingComponent.CanSeeTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceCouldSeeTarget = 0f;
				return false;
			}
			return this.timeSinceCouldSeeTarget > 3f;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002A4C8 File Offset: 0x000286C8
		private void ChooseCirclePoint()
		{
			Entity target = base.Target;
			Rotation rotation = Rotation.LookAt((target != null) ? target.Position : this.Host.Position);
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

		// Token: 0x04000321 RID: 801
		private TimeSince timeSinceCouldSeeTarget = 0f;

		// Token: 0x04000322 RID: 802
		private Vector3 targetPoint;

		// Token: 0x04000323 RID: 803
		private TimeSince timeSinceLastTick = 0f;
	}
}
