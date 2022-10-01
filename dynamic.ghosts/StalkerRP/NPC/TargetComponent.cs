using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200006E RID: 110
	public class TargetComponent : EntityComponent, ISingletonComponent
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001846E File Offset: 0x0001666E
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00018476 File Offset: 0x00016676
		[DefaultValue(CreatureIdentity.None)]
		public CreatureIdentity Identity { get; set; }

		// Token: 0x0600050B RID: 1291 RVA: 0x0001847F File Offset: 0x0001667F
		public void LoadFromAsset(NPCResource resource)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Identity = resource.Identity;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00018492 File Offset: 0x00016692
		protected override void OnActivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			TargetComponent.All.Add(this);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000184A4 File Offset: 0x000166A4
		protected override void OnDeactivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			TargetComponent.All.Remove(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RemoveAsValidTarget();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000184C2 File Offset: 0x000166C2
		[Description("Removes the target component from all valid targeting components. Note: this doesn't stop them being readded.")]
		public void RemoveAsValidTarget()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			TargetingComponent.All.ForEach(delegate(TargetingComponent x)
			{
				x.RemovePotentialTarget(this);
			});
		}

		// Token: 0x040001C0 RID: 448
		public static readonly List<TargetComponent> All = new List<TargetComponent>();
	}
}
