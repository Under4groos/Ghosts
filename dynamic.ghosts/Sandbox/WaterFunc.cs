using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000199 RID: 409
	[Library("func_water")]
	[HammerEntity]
	[Solid]
	[HideProperty("enable_shadows")]
	[HideProperty("SetColor")]
	[Title("Water Volume")]
	[Category("Gameplay")]
	[Icon("water")]
	[Description("Generic water volume. Make sure to have light probe volume envelop the volume of the water for the water to gain proper lighting.")]
	public class WaterFunc : Water
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x00052C63 File Offset: 0x00050E63
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePhysics();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = false;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00052C93 File Offset: 0x00050E93
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("ClientSpawn");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePhysics();
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00052CBA File Offset: 0x00050EBA
		private void CreatePhysics()
		{
			PhysicsGroup physicsGroup = base.SetupPhysicsFromModel(PhysicsMotionType.Keyframed, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			physicsGroup.SetSurface("water");
		}
	}
}
