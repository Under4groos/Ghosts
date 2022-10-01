using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Sandbox.Internal
{
	// Token: 0x020001E4 RID: 484
	[Library("break_change_material_group")]
	[Description("Overrides the materialgroup on spawned breakpieces. (By default the active material group of the parent model is propagated to the breakpieces.)")]
	internal class ModelBreakPieceMaterialGroup : IModelBreakCommand
	{
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0006442A File Offset: 0x0006262A
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x00064432 File Offset: 0x00062632
		[JsonPropertyName("material_group_name")]
		[FGDType("materialgroup", "", "")]
		[Description("Material group name to switch to.")]
		public string MaterialGroup { get; set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0006443B File Offset: 0x0006263B
		// (set) Token: 0x06001817 RID: 6167 RVA: 0x00064443 File Offset: 0x00062643
		[JsonPropertyName("limit_to_piece")]
		[FGDType("model_breakpiece", "", "")]
		[Description("If set, only apply this command to a particular piece.")]
		public string LimitToPiece { get; set; }

		// Token: 0x06001818 RID: 6168 RVA: 0x0006444C File Offset: 0x0006264C
		public void OnBreak(Breakables.Result res)
		{
			foreach (ModelEntity gib in res.Props)
			{
				if (!string.IsNullOrEmpty(this.LimitToPiece))
				{
					PropGib propGib = gib as PropGib;
					if (propGib != null && propGib.BreakpieceName != this.LimitToPiece)
					{
						continue;
					}
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				gib.SetMaterialGroup(this.MaterialGroup);
			}
		}
	}
}
