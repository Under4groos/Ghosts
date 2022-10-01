using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000CB RID: 203
	public class GiantStompState : NPCState<GiantNPC>
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00026A5D File Offset: 0x00024C5D
		public float Cooldown
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00026A64 File Offset: 0x00024C64
		public GiantStompState(GiantNPC host) : base(host)
		{
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00026A7D File Offset: 0x00024C7D
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00026AAB File Offset: 0x00024CAB
		public override void OnStateExited()
		{
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00026AB0 File Offset: 0x00024CB0
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			if (this.TimeSinceEntered > 3.5f)
			{
				this.Host.SM.SetState(this.Host.ChaseState);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00026B00 File Offset: 0x00024D00
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			if (intData == 1)
			{
				this.DoStomp();
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00026B0C File Offset: 0x00024D0C
		public bool CanStomp()
		{
			return this.timeSinceLastStomp >= this.Cooldown && this.Host.TargetingComponent.DistanceToTarget <= this.Host.StompFallOffRange;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00026B43 File Offset: 0x00024D43
		public void SetStompCooldown(float n)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastStomp = this.Cooldown - n;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00026B60 File Offset: 0x00024D60
		private void DoStomp()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.DoStompAttackFX(this.Host.Position);
			foreach (Entity ent in Entity.FindInSphere(this.Host.Position, this.Host.StompFallOffRange))
			{
				if (ent != this.Host && ((ent.GroundEntity != null && ent.GroundEntity.IsWorld) || ent is Prop))
				{
					float stompDamage = this.Host.StompDamage;
					float dist = ent.Position.Distance(this.Host.Position);
					float frac = 1f - dist / this.Host.StompFallOffRange;
					Vector3 dir = (ent.EyePosition - this.Host.Position).Normal + Vector3.Up * 0.5f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					float x = stompDamage * frac;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					DamageInfo info = DamageInfo.Generic(MathF.Max(x, 0f)).WithAttacker(this.Host, null).WithFlag(DamageFlags.Crush).WithFlag(DamageFlags.Cook).WithPosition(ent.Position).WithForce(dir * this.Host.StompForce * frac);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent.TakeDamage(info);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastStomp = 0f;
		}

		// Token: 0x040002DD RID: 733
		private TimeSince timeSinceLastStomp = 0f;
	}
}
