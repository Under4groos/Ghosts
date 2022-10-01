using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000075 RID: 117
	public class BloodSuckerCircleState : NPCState<BloodSuckerNPC>
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x00019404 File Offset: 0x00017604
		public BloodSuckerCircleState(BloodSuckerNPC bloodSucker) : base(bloodSucker)
		{
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001945C File Offset: 0x0001765C
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastThreatCalc = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.circleNum = Rand.Int(2, 3);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.BeginFade();
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000194EC File Offset: 0x000176EC
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00019500 File Offset: 0x00017700
		public override void TakeDamage(DamageInfo info)
		{
			if (this.circleNum > 1)
			{
				Vector3 fleeDir = (this.Host.Position - base.Target.Position).Normal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				fleeDir *= Rotation.FromYaw(Rand.Float(-90f, 90f));
				float fleeDistance = Rand.Float(100f, this.maximumCircleDistance);
				Vector3 fleePoint = this.Host.Position + fleeDir.WithZ(0f) * fleeDistance;
				Vector3? navPoint = NavMesh.GetClosestPoint(fleePoint);
				if (navPoint != null)
				{
					this.Host.Steer.Target = navPoint.Value;
					return;
				}
				this.Host.Steer.Target = fleePoint;
				return;
			}
			else
			{
				if (this.circleNum <= 1)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.circleNum = 0;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.Steer = null;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.AttackState);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.EmitAggroSound();
					return;
				}
				return;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00019624 File Offset: 0x00017824
		public override void OnCrit(DamageInfo info)
		{
			if (this.circleNum > 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.circleNum = 0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer = null;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.AttackState);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.EmitAggroSound();
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00019688 File Offset: 0x00017888
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
			if (this.Host.TargetingComponent.DistanceToTarget < 250f && this.TimeSinceEntered > 2f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer = null;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.AttackState);
				return;
			}
			if (!this.Host.Position.AlmostEqual(this.Host.Steer.Target, 15f) && this.timeSinceLastCirclePoint <= this.maximumCircleTime)
			{
				return;
			}
			if (this.circleNum <= 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FinishCircling();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChooseCirclePoint();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000197C0 File Offset: 0x000179C0
		private void FinishCircling()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			if (Rand.Int(9) == 9 && this.Host.TargetingComponent.DistanceToTarget > 280f)
			{
				this.Host.SM.SetState(this.Host.TauntState);
			}
			else
			{
				this.Host.SM.SetState(this.Host.AttackState);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastCirclePoint = 0f;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00019850 File Offset: 0x00017A50
		private void ChooseCirclePoint()
		{
			Vector3 point = base.Target.Position + Vector3.Random.WithZ(0f).Normal * Rand.Float(this.minimumCircleDistance, this.maximumCircleDistance);
			Vector3? navPoint = NavMesh.GetClosestPoint(point);
			if (navPoint != null)
			{
				this.Host.Steer.Target = navPoint.Value;
			}
			else
			{
				this.Host.Steer.Target = point;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.circleNum--;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastCirclePoint = 0f;
		}

		// Token: 0x040001EB RID: 491
		private float minimumCircleDistance = 400f;

		// Token: 0x040001EC RID: 492
		private float maximumCircleDistance = 800f;

		// Token: 0x040001ED RID: 493
		private float maximumCircleTime = 3f;

		// Token: 0x040001EE RID: 494
		private int circleNum;

		// Token: 0x040001EF RID: 495
		private TimeSince timeSinceLastCirclePoint = 0f;

		// Token: 0x040001F0 RID: 496
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
