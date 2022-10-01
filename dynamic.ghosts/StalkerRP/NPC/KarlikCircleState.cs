using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000B3 RID: 179
	public class KarlikCircleState : NPCState<KarlikNPC>
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00022F63 File Offset: 0x00021163
		private float maxTimeBetweenNewPoints
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00022F6A File Offset: 0x0002116A
		private float maxCircleDistance
		{
			get
			{
				return 1500f;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00022F71 File Offset: 0x00021171
		private float minCircleDistance
		{
			get
			{
				return 200f;
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00022F78 File Offset: 0x00021178
		public KarlikCircleState(KarlikNPC host) : base(host)
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00022FC8 File Offset: 0x000211C8
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.Resource.RunSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.GrowlSound);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.numCircles = Rand.Int(2, 4);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002306F File Offset: 0x0002126F
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000230B0 File Offset: 0x000212B0
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
			if (this.Host.Position.AlmostEqual(this.retreatPos, 15f) || this.timeSinceLastCirclePoint > this.maxTimeBetweenNewPoints)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ChooseCirclePoint();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetSteerTarget();
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00023194 File Offset: 0x00021394
		public override void TakeDamage(DamageInfo info)
		{
			if (this.timeSinceLastDamageTaken < this.aggroDelay)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastDamageTaken = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.numCircles--;
			if (this.numCircles <= 0)
			{
				this.Host.SM.SetState(this.Host.ChaseState);
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00023204 File Offset: 0x00021404
		private void SetSteerTarget()
		{
			Vector3 targetPos = this.Host.Target.Position;
			Vector3 dir = (this.Host.Position - targetPos).Normal;
			if (this.Host.TargetingComponent.DistanceToTarget < 80f && this.Host.IsStuck)
			{
				this.Host.Steer.Target = targetPos + dir * 150f;
				return;
			}
			if (this.Host.TimeSinceLastStuck > 1f)
			{
				this.Host.Steer.Target = this.retreatPos;
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000232B0 File Offset: 0x000214B0
		private void ChooseCirclePoint()
		{
			Vector3 point = base.Target.Position + Vector3.Random.WithZ(0f).Normal * Rand.Float(this.minCircleDistance, this.maxCircleDistance);
			Vector3? navPoint = NavMesh.GetClosestPoint(point);
			if (navPoint != null)
			{
				this.retreatPos = navPoint.Value;
			}
			else
			{
				this.retreatPos = point;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.retreatPos;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastCirclePoint = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.numCircles--;
			if (this.numCircles <= 0)
			{
				this.Host.SM.SetState(this.Host.ChaseState);
			}
		}

		// Token: 0x04000292 RID: 658
		private Vector3 retreatPos;

		// Token: 0x04000293 RID: 659
		private int numCircles;

		// Token: 0x04000294 RID: 660
		private TimeSince timeSinceLastCirclePoint = 0f;

		// Token: 0x04000295 RID: 661
		private TimeSince timeSinceLastThreatCalc = 0f;

		// Token: 0x04000296 RID: 662
		private TimeSince timeSinceLastDamageTaken = 0f;

		// Token: 0x04000297 RID: 663
		private float aggroDelay = 0.5f;
	}
}
