using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;
using StalkerRP.PostProcessing;

namespace StalkerRP.NPC
{
	// Token: 0x020000BA RID: 186
	[DebugSpawnable(Name = "Pyrogeist", Category = "Mutants")]
	[Title("Pyrogeist")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/poltergeist/pyrogesit.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class PyrogeistNPC : PoltergeistNPC
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00024091 File Offset: 0x00022291
		protected override string bodyEffect
		{
			get
			{
				return "particles/stalker/monsters/pyrogeist_body.vpcf";
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00024098 File Offset: 0x00022298
		protected override string deathEffect
		{
			get
			{
				return "particles/stalker/monsters/pyrogeist_death_explode.vpcf";
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0002409F File Offset: 0x0002229F
		protected override string bodyIdleSound
		{
			get
			{
				return "poltergeist.pyro.idle";
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x000240A6 File Offset: 0x000222A6
		protected override string effectSound
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x000240A9 File Offset: 0x000222A9
		protected override Color screenColor
		{
			get
			{
				return EffectsPostProcessManager.STALKER_ANOMALY_BURNER;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x000240B0 File Offset: 0x000222B0
		protected override float effectRange
		{
			get
			{
				return 1200f;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x000240B7 File Offset: 0x000222B7
		protected override float effectStrength
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x000240BE File Offset: 0x000222BE
		protected override string NPCAssetID
		{
			get
			{
				return "pyrogeist";
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000240C5 File Offset: 0x000222C5
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RoamingState = new PyrogeistRoamingState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.RoamingState);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000240F0 File Offset: 0x000222F0
		public override void TakeDamage(DamageInfo info)
		{
			if (info.Flags.HasFlag(DamageFlags.Burn) || info.Flags.HasFlag(DamageFlags.Acid))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
		}
	}
}
