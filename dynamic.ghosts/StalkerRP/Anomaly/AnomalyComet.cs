using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.PostProcessing;

namespace StalkerRP.Anomaly
{
	// Token: 0x0200013A RID: 314
	[Title("Comet")]
	[HammerEntity]
	[EditorModel("models/stalker/anomalies/comet.vmdl", "white", "white")]
	[Category("Anomalies")]
	public class AnomalyComet : AnomalyMoving
	{
		// Token: 0x06000E02 RID: 3586 RVA: 0x00038515 File Offset: 0x00036715
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel("models/stalker/anomalies/comet.vmdl");
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00038534 File Offset: 0x00036734
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			PostProcessEmitterComponent postProcessEmitterComponent = this.Components.Create<PostProcessEmitterComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			postProcessEmitterComponent.SetUpEmitter(EffectsPostProcessManager.STALKER_ANOMALY_BURNER, 700f, 2f, "", 0f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.sfx = base.PlaySound("comet.idle");
		}

		// Token: 0x0400047D RID: 1149
		private Sound sfx;
	}
}
