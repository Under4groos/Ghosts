using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x0200008D RID: 141
	[DebugSpawnable(Name = "Cat", Category = "Mutants")]
	[Title("Cat")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/cat/cat.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class CatNPC : BlindDogNPC
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001D970 File Offset: 0x0001BB70
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(32f, 20f);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001D981 File Offset: 0x0001BB81
		public override string PanicSound
		{
			get
			{
				return "cat.growl";
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001D988 File Offset: 0x0001BB88
		public override string GrowlSound
		{
			get
			{
				return "cat.growl";
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001D98F File Offset: 0x0001BB8F
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001D996 File Offset: 0x0001BB96
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001D99D File Offset: 0x0001BB9D
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001D9A4 File Offset: 0x0001BBA4
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001D9A7 File Offset: 0x0001BBA7
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001D9AE File Offset: 0x0001BBAE
		protected override string NPCAssetID
		{
			get
			{
				return "cat";
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001D9B5 File Offset: 0x0001BBB5
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PostSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetMaterialGroup(Rand.Int(base.MaterialGroupCount - 1));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		protected override void FindGroup()
		{
			foreach (CatNPC cat in Entity.All.OfType<CatNPC>())
			{
				if (this.Position.Distance(cat.Position) < base.MaxGroupFormDistance)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					cat.JoinGroup(this);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.CatGroup = cat.CatGroup;
					return;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CatGroup = new CatGroup();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CatGroup.SetLeader(this);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001DA84 File Offset: 0x0001BC84
		protected override void OnTakeDamage(DamageInfo info)
		{
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001DA86 File Offset: 0x0001BC86
		public override void JoinGroup(NPCBase cat)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CatGroup.AddMember(cat);
		}

		// Token: 0x04000234 RID: 564
		public CatGroup CatGroup;
	}
}
