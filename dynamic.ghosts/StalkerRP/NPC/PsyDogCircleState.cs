using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000CD RID: 205
	public class PsyDogCircleState : NPCState<PsyDogNPC>
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00026F4E File Offset: 0x0002514E
		private float MaxCircleRange
		{
			get
			{
				return 1300f;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00026F55 File Offset: 0x00025155
		private float MinCircleRange
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00026F5C File Offset: 0x0002515C
		public PsyDogCircleState(PsyDogNPC host) : base(host)
		{
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00026F80 File Offset: 0x00025180
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

		// Token: 0x060008F1 RID: 2289 RVA: 0x00026FD9 File Offset: 0x000251D9
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00027018 File Offset: 0x00025218
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			if (this.Host.IsIllusion())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChargeState);
				return;
			}
			if (this.Host.ShouldCreateIllusions())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.SummonState);
				return;
			}
			if (this.ShouldCharge())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChargeState);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateCirclingStatus();
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00027104 File Offset: 0x00025304
		private void UpdateCirclingStatus()
		{
			if (this.timeSinceLastCircleCheck < 0.2f)
			{
				return;
			}
			if (this.Host.Position.AlmostEqual(this.targetPoint, 20f))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ChooseCirclePoint();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastCircleCheck = 0f;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00027164 File Offset: 0x00025364
		private bool ShouldCharge()
		{
			if (this.Host.TargetingComponent.DistanceToTarget < this.Host.ChargeState.ForceChargeRange)
			{
				return true;
			}
			int numNear = 0;
			using (IEnumerator<PsyDogIllusionNPC> enumerator = this.Host.IllusionGroup.Members.OfType<PsyDogIllusionNPC>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Position.Distance(base.Target.Position) < this.MinDistanceForIllusionCount)
					{
						numNear++;
					}
				}
			}
			return numNear > 3;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002720C File Offset: 0x0002540C
		private void ChooseCirclePoint()
		{
			Vector3 tPos = base.Target.Position;
			Rotation rotation = Rotation.LookAt((tPos - this.Host.Position).Normal);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 dir = rotation.RotateAroundAxis(Vector3.Up, Rand.Float(-70f, 70f)).Forward;
			float length = Rand.Float(this.MinCircleRange, this.MaxCircleRange);
			Vector3? point = NavMesh.GetClosestPoint(tPos + dir * length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.targetPoint = (point ?? tPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.targetPoint;
		}

		// Token: 0x040002E0 RID: 736
		private Vector3 targetPoint;

		// Token: 0x040002E1 RID: 737
		private TimeSince timeSinceLastCircleCheck = 0f;

		// Token: 0x040002E2 RID: 738
		private float MinDistanceForIllusionCount = 350f;
	}
}
