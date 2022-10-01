using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000D0 RID: 208
	public class PsyDogFleeState : NPCState<PsyDogNPC>
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00027705 File Offset: 0x00025905
		private float FleeTime
		{
			get
			{
				return 7f;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0002770C File Offset: 0x0002590C
		private float MaxCircleRange
		{
			get
			{
				return 1300f;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00027713 File Offset: 0x00025913
		private float MinCircleRange
		{
			get
			{
				return 600f;
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002771A File Offset: 0x0002591A
		public PsyDogFleeState(PsyDogNPC host) : base(host)
		{
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00027744 File Offset: 0x00025944
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

		// Token: 0x0600090A RID: 2314 RVA: 0x000277B3 File Offset: 0x000259B3
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000277F4 File Offset: 0x000259F4
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
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002785C File Offset: 0x00025A5C
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
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002792E File Offset: 0x00025B2E
		private bool CanStopFleeing()
		{
			if (this.Host.TargetingComponent.CanSeeTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceCouldSeeTarget = 0f;
				return false;
			}
			return this.TimeSinceCouldSeeTarget > 1f;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002796C File Offset: 0x00025B6C
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

		// Token: 0x040002EA RID: 746
		private TimeSince TimeSinceCouldSeeTarget = 0f;

		// Token: 0x040002EB RID: 747
		private Vector3 targetPoint;

		// Token: 0x040002EC RID: 748
		private TimeSince timeSinceLastTick = 0f;
	}
}
