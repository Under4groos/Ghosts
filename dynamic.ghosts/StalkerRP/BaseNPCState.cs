using System;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200001D RID: 29
	public abstract class BaseNPCState
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00008E9B File Offset: 0x0000709B
		public virtual void OnStateEntered()
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00008E9D File Offset: 0x0000709D
		public virtual void OnStateExited()
		{
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00008E9F File Offset: 0x0000709F
		public virtual void Update(float deltaTime)
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00008EA1 File Offset: 0x000070A1
		public virtual void OnCrit(DamageInfo info)
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008EA3 File Offset: 0x000070A3
		public virtual void TakeDamage(DamageInfo info)
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008EA5 File Offset: 0x000070A5
		public virtual void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008EA7 File Offset: 0x000070A7
		public virtual void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008EA9 File Offset: 0x000070A9
		public virtual void OnKilled()
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008EAB File Offset: 0x000070AB
		[Description("Note that this is when the entity is deleted, not killed.")]
		public virtual void OnDestroyed()
		{
		}

		// Token: 0x04000059 RID: 89
		public TimeSince TimeSinceEntered = 0f;

		// Token: 0x0400005A RID: 90
		public TimeSince TimeSinceExited = 0f;

		// Token: 0x0400005B RID: 91
		public bool HasEntered;

		// Token: 0x0400005C RID: 92
		public bool IsActive;
	}
}
