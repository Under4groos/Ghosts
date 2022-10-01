using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A5 RID: 421
	[Library("post_processing_volume")]
	[HammerEntity]
	[Solid]
	[AutoApplyMaterial("materials/tools_postprocess_volume.vmat")]
	[PostProcessingVolume]
	[Title("Post Processing Volume")]
	[Category("Effects")]
	[Icon("filter")]
	[Description("Applies given post processing effect to players inside this volume. Takes priority over <see cref=\"T:Sandbox.PostProcessingEntity\">post_processing_entity</see> for all players inside. For instances of overlapping volumes, last entered volume will take priority.")]
	public class PostProcessingVolume : PostProcessingEntity
	{
		// Token: 0x0600153A RID: 5434 RVA: 0x00055102 File Offset: 0x00053302
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("trigger");
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00055124 File Offset: 0x00053324
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Static, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableSolidCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableTouch = true;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005514C File Offset: 0x0005334C
		public override void StartTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StartTouch(other);
			if (other != Local.Pawn || Host.IsServer)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TurnOnEffect();
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00055175 File Offset: 0x00053375
		public override void Touch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Touch(other);
			if (other != Local.Pawn || Host.IsServer)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TurnOnEffect();
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0005519E File Offset: 0x0005339E
		public override void EndTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EndTouch(other);
			if (other != Local.Pawn || Host.IsServer)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TurnOffEffect();
		}
	}
}
