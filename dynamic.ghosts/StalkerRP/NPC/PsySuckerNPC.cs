using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000DB RID: 219
	[DebugSpawnable(Name = "Psysucker", Category = "Mutants")]
	[Title("Psysucker")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/psysucker/psysucker.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class PsySuckerNPC : BloodSuckerNPC, IPsyCreature
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00028D0A File Offset: 0x00026F0A
		public override float AttackDamage
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00028D11 File Offset: 0x00026F11
		public override float UncloakRange
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00028D18 File Offset: 0x00026F18
		public override float VisibilityMinAlpha
		{
			get
			{
				return 0.12f;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00028D1F File Offset: 0x00026F1F
		protected override string AggroSound
		{
			get
			{
				return "psysucker.attackhit";
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00028D26 File Offset: 0x00026F26
		protected override string CloakStartSound
		{
			get
			{
				return "psysucker.cloak.start";
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00028D2D File Offset: 0x00026F2D
		protected override string CloakEndSound
		{
			get
			{
				return "psysucker.cloak.end";
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00028D34 File Offset: 0x00026F34
		protected string IllusionCreateSound
		{
			get
			{
				return "psysucker.illusioncreate";
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00028D3B File Offset: 0x00026F3B
		protected virtual bool IsIllusion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00028D3E File Offset: 0x00026F3E
		private int MaximumIllusions
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00028D41 File Offset: 0x00026F41
		private float DelayBetweenIllusions
		{
			get
			{
				return 6f;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00028D48 File Offset: 0x00026F48
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(62f, 28f);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00028D59 File Offset: 0x00026F59
		protected override string NPCAssetID
		{
			get
			{
				return "psysucker";
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00028D60 File Offset: 0x00026F60
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PostSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = 0.8f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 42f;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00028D97 File Offset: 0x00026F97
		protected override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			if (this.CanSummonIllusion())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SummonIllusion();
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00028DB7 File Offset: 0x00026FB7
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CleanUpIllusions();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00028DCF File Offset: 0x00026FCF
		public void RemoveIllusion(PsySuckerIllusionNPC npc)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastIllusion -= 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Illusions.Remove(npc);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00028E04 File Offset: 0x00027004
		private bool CanSummonIllusion()
		{
			return !this.IsIllusion && base.TargetingComponent.ValidateTarget() && this.timeSinceLastIllusion > this.DelayBetweenIllusions && this.Illusions.Count < this.MaximumIllusions;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00028E54 File Offset: 0x00027054
		private void SummonIllusion()
		{
			PsySuckerIllusionNPC illusionNPC = new PsySuckerIllusionNPC
			{
				Position = this.Position,
				Rotation = this.Rotation
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			illusionNPC.TargetingComponent.TrySetTarget(base.Target);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			illusionNPC.SetCreator(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Illusions.Add(illusionNPC);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastIllusion = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/psy_small_explosion.vpcf", base.WorldSpaceBounds.Center);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound(this.IllusionCreateSound);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00028EF8 File Offset: 0x000270F8
		private void CleanUpIllusions()
		{
			foreach (PsySuckerIllusionNPC illusionNPC in this.Illusions.ToArray())
			{
				if (illusionNPC.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					illusionNPC.OnKilled();
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Illusions.Clear();
		}

		// Token: 0x04000307 RID: 775
		private readonly List<PsySuckerIllusionNPC> Illusions = new List<PsySuckerIllusionNPC>();

		// Token: 0x04000308 RID: 776
		private TimeSince timeSinceLastIllusion = 0f;
	}
}
