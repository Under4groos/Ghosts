using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000B8 RID: 184
	public class PoltergeistRoamingState : NPCState<PoltergeistNPC>
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00023971 File Offset: 0x00021B71
		protected virtual float attackDelay
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00023978 File Offset: 0x00021B78
		protected virtual float attackSearchRadius
		{
			get
			{
				return 1000f;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0002397F File Offset: 0x00021B7F
		protected virtual float attackHoldTime
		{
			get
			{
				return 3.5f;
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00023988 File Offset: 0x00021B88
		public PoltergeistRoamingState(PoltergeistNPC host) : base(host)
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00023A03 File Offset: 0x00021C03
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00023A1C File Offset: 0x00021C1C
		public override void Update(float deltaTime)
		{
			if (this.timeSinceLastThreatCalc > this.threatCalcDelay)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatBasedOnMovement(true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceLastThreatCalc = 0f;
			}
			if (!this.initialPositionSet)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.initialPosition = this.Host.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.initialPositionSet = true;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.Resource.RunSpeed;
			if (this.timeSinceLastAttack > this.attackDelay)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TryAttack();
			}
			if (this.Host.Position.AlmostEqual(this.Host.Steer.Target, 55f) || this.timeSinceLastCirclePoint > this.maximumCircleTime || (this.timeSinceLastCirclePoint > 0.5f && this.Host.Velocity.Length < 30f))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ChooseCirclePoint();
				return;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00023B44 File Offset: 0x00021D44
		private void ChooseCirclePoint()
		{
			Vector3 point;
			if (this.Host.TargetingComponent.ValidateTarget())
			{
				point = base.Target.Position + Vector3.Random.Normal * Rand.Float(this.minimumCircleDistance, this.maximumCircleDistance);
			}
			else
			{
				point = this.initialPosition + Vector3.Random.Normal * Rand.Float(this.minimumCircleDistance, this.maximumCircleDistance);
			}
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
			this.timeSinceLastCirclePoint = 0f;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00023C18 File Offset: 0x00021E18
		protected virtual void TryAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastAttack = 0f;
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				return;
			}
			foreach (Entity entity in Entity.FindInSphere(base.Target.Position, this.attackSearchRadius))
			{
				if (TelekinesisThrowComponent.CanAddToEntity(entity, base.Target))
				{
					TelekinesisThrowComponent c = entity.Components.Create<TelekinesisThrowComponent>(true);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					c.SetUp(base.Target, this.attackHoldTime, 300f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.componentList.Add(c);
					break;
				}
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00023CE0 File Offset: 0x00021EE0
		public override void OnDestroyed()
		{
			foreach (TelekinesisThrowComponent telekinesisThrowComponent in from component in this.componentList
			where component.Entity.IsValid()
			select component)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				telekinesisThrowComponent.Release();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				telekinesisThrowComponent.Remove();
			}
		}

		// Token: 0x040002A0 RID: 672
		private float minimumCircleDistance = 100f;

		// Token: 0x040002A1 RID: 673
		private float maximumCircleDistance = 2000f;

		// Token: 0x040002A2 RID: 674
		private float maximumCircleTime = 4f;

		// Token: 0x040002A3 RID: 675
		private float threatCalcDelay = 0.5f;

		// Token: 0x040002A4 RID: 676
		private bool initialPositionSet;

		// Token: 0x040002A5 RID: 677
		private Vector3 initialPosition;

		// Token: 0x040002A6 RID: 678
		private List<TelekinesisThrowComponent> componentList = new List<TelekinesisThrowComponent>();

		// Token: 0x040002A7 RID: 679
		private TimeSince timeSinceLastThreatCalc = 0f;

		// Token: 0x040002A8 RID: 680
		private TimeSince timeSinceLastCirclePoint = 0f;

		// Token: 0x040002A9 RID: 681
		protected TimeSince timeSinceLastAttack = 0f;
	}
}
