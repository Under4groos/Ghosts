using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000A4 RID: 164
	[DebugSpawnable(Name = "Flesh Alpha", Category = "Mutants")]
	[Title("Flesh Alpha")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/bloodsucker_anomaly/bloodsucker_anomaly.vmdl", "white", "white")]
	[Spawnable]
	public class FleshAlphaNPC : FleshNPC
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00021410 File Offset: 0x0001F610
		protected override string NPCAssetID
		{
			get
			{
				return "fleshalpha";
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00021417 File Offset: 0x0001F617
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PostSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetMaterialGroup("Alpha");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Group = new FleshGroup();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Group.SetLeader(this);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00021455 File Offset: 0x0001F655
		public void RecruitFlesh(FleshNPC fleshNPC)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Group.AddMember(fleshNPC);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00021468 File Offset: 0x0001F668
		public override void OnTargetAcquired(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Group.OnLeaderAcquiredTarget(target);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0002147B File Offset: 0x0001F67B
		public override void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Group.OnLeaderDeath();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Group.RemoveLeader();
		}

		// Token: 0x04000273 RID: 627
		private FleshGroup Group;
	}
}
