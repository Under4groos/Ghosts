using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000DA RID: 218
	[Library("npc_psysucker_illusion", Title = "Psy Sucker Illusion")]
	[Spawnable]
	public class PsySuckerIllusionNPC : PsySuckerNPC, IPsyIllusion
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00028C2F File Offset: 0x00026E2F
		public override float AttackDamage
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00028C36 File Offset: 0x00026E36
		protected override bool IsIllusion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00028C39 File Offset: 0x00026E39
		public override bool EmitAggroSoundOnLeavingIdle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00028C3C File Offset: 0x00026E3C
		protected override string NPCAssetID
		{
			get
			{
				return "psysucker.illusion";
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00028C43 File Offset: 0x00026E43
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PostSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Health = 1f;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00028C60 File Offset: 0x00026E60
		public void SetCreator(PsySuckerNPC creator)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Creator = creator;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00028C6E File Offset: 0x00026E6E
		protected override void DoNPCDeath()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoIllusionDeath();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CleanUpIllusion();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028C86 File Offset: 0x00026E86
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CleanUpIllusion();
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00028CA0 File Offset: 0x00026EA0
		private void DoIllusionDeath()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LifeState = LifeState.Dead;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/psy_implosion.vpcf", base.WorldSpaceBounds.Center);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00028CE2 File Offset: 0x00026EE2
		private void CleanUpIllusion()
		{
			if (this.Creator.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Creator.RemoveIllusion(this);
			}
		}

		// Token: 0x04000306 RID: 774
		private PsySuckerNPC Creator;
	}
}
