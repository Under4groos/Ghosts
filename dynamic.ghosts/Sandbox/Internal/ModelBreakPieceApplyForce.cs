using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using ModelDoc;

namespace Sandbox.Internal
{
	// Token: 0x020001E5 RID: 485
	[Library("break_apply_force")]
	[Axis(Origin = "offset", Attachment = "attachment_point")]
	[Description("Applies extra velocity to breakpieces outwards from the influence point (default is the origin of the model)")]
	internal class ModelBreakPieceApplyForce : IModelBreakCommand
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x000644DC File Offset: 0x000626DC
		// (set) Token: 0x0600181B RID: 6171 RVA: 0x000644E4 File Offset: 0x000626E4
		[Description("Offset for the influence point (in the space of the model or attachment)")]
		public Vector3 Offset { get; set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x000644ED File Offset: 0x000626ED
		// (set) Token: 0x0600181D RID: 6173 RVA: 0x000644F5 File Offset: 0x000626F5
		[JsonPropertyName("attachment_point")]
		[FGDType("model_attachment", "", "")]
		[Description("Offset the influence point from the named attachment rather than the root of the model")]
		public string AttachmentPoint { get; set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x000644FE File Offset: 0x000626FE
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x00064506 File Offset: 0x00062706
		[JsonPropertyName("center_on_damage_point")]
		[DefaultValue(false)]
		[Description("Center the influence point (will ignore cause attachment to be ignored, but still honor offset)")]
		public bool CenterOnDamagePoint { get; set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0006450F File Offset: 0x0006270F
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x00064517 File Offset: 0x00062717
		[JsonPropertyName("limit_to_piece")]
		[FGDType("model_breakpiece", "", "")]
		[DefaultValue("")]
		[Description("If set, only apply this command to a particular piece.")]
		public string LimitToPiece { get; set; } = "";

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x00064520 File Offset: 0x00062720
		// (set) Token: 0x06001823 RID: 6179 RVA: 0x00064528 File Offset: 0x00062728
		[JsonPropertyName("burst_scale")]
		[MinMax(-500f, 500f)]
		[DefaultValue(0)]
		[Description("Velocity added to each piece (radially, away from the influence point)")]
		public float BurstScale { get; set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x00064531 File Offset: 0x00062731
		// (set) Token: 0x06001825 RID: 6181 RVA: 0x00064539 File Offset: 0x00062739
		[JsonPropertyName("burst_randomize")]
		[MinMax(0f, 500f)]
		[DefaultValue(0)]
		[Description("Magnitude of random vector that will be added to the burst velocity")]
		public float BurstRandomize { get; set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x00064542 File Offset: 0x00062742
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x0006454A File Offset: 0x0006274A
		[DefaultValue(ModelBreakPieceApplyForce.BreakForceType.RadialPush)]
		[Description("What kind of force to apply?<br /><b>Radial Push</b> - Applies a radial burst to breakpieces outwards from the influence point<br /><b>Angular Flip</b> - Applies an angular 'flip' to breakpieces (like objects tipping over from an explosion or flower petals opening) causing them to tip outwards from the influence point<br /><b>Angular Twist</b> - Applies an angular 'twist' to breakpieces, causing them to roll around the radial axis outward from the influence point<br />")]
		public ModelBreakPieceApplyForce.BreakForceType ForceType { get; set; }

		// Token: 0x06001828 RID: 6184 RVA: 0x00064554 File Offset: 0x00062754
		protected Vector3 ComputeBreakForcePoint(Breakables.Result res)
		{
			if (res.Source == null)
			{
				return Vector3.Zero;
			}
			if (this.CenterOnDamagePoint)
			{
				return res.Params.DamagePositon + res.Source.Transform.Rotation * this.Offset;
			}
			ModelEntity mdlEnt = res.Source as ModelEntity;
			if (mdlEnt != null && !string.IsNullOrEmpty(this.AttachmentPoint))
			{
				Transform? mTemp = mdlEnt.GetAttachment(this.AttachmentPoint, true);
				if (mTemp != null)
				{
					return mTemp.Value.TransformVector(this.Offset);
				}
			}
			return res.Source.Transform.TransformVector(this.Offset);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00064608 File Offset: 0x00062808
		public void OnBreak(Breakables.Result res)
		{
			Vector3 offset = this.ComputeBreakForcePoint(res);
			if (this.ForceType == ModelBreakPieceApplyForce.BreakForceType.AngularFlip || this.ForceType == ModelBreakPieceApplyForce.BreakForceType.AngularTwist)
			{
				using (List<ModelEntity>.Enumerator enumerator = res.Props.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ModelEntity gib = enumerator.Current;
						if (!string.IsNullOrEmpty(this.LimitToPiece))
						{
							PropGib propGib = gib as PropGib;
							if (propGib != null && propGib.BreakpieceName != this.LimitToPiece)
							{
								continue;
							}
						}
						Vector3 vRelativePos = gib.PhysicsBody.MassCenter - offset;
						if (this.BurstRandomize > 1E-06f)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vRelativePos += Vector3.Random * this.BurstRandomize;
						}
						RuntimeHelpers.EnsureSufficientExecutionStack();
						vRelativePos = vRelativePos.Normal;
						Vector3 vFlipAxis = vRelativePos;
						if (this.ForceType == ModelBreakPieceApplyForce.BreakForceType.AngularFlip)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vFlipAxis = vRelativePos.Cross(Vector3.Down);
						}
						float flFinalScale = this.BurstScale;
						if (this.BurstRandomize > 1E-06f)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							flFinalScale += Rand.Float(-this.BurstRandomize, this.BurstRandomize);
						}
						RuntimeHelpers.EnsureSufficientExecutionStack();
						gib.PhysicsBody.AngularVelocity += vFlipAxis * flFinalScale * 0.1f;
					}
					return;
				}
			}
			foreach (ModelEntity gib2 in res.Props)
			{
				if (!string.IsNullOrEmpty(this.LimitToPiece))
				{
					PropGib propGib2 = gib2 as PropGib;
					if (propGib2 != null && propGib2.BreakpieceName != this.LimitToPiece)
					{
						continue;
					}
				}
				Vector3 vecBurst = gib2.PhysicsBody.MassCenter - offset;
				float flBurstLen = vecBurst.Length;
				if (flBurstLen > 1E-06f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vecBurst *= Math.Abs(this.BurstScale) / flBurstLen;
				}
				if (this.BurstRandomize > 1E-06f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vecBurst += Vector3.Random * this.BurstRandomize;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				gib2.PhysicsBody.Velocity += vecBurst;
			}
		}

		// Token: 0x0200026C RID: 620
		public enum BreakForceType
		{
			// Token: 0x04000A28 RID: 2600
			RadialPush,
			// Token: 0x04000A29 RID: 2601
			AngularFlip,
			// Token: 0x04000A2A RID: 2602
			AngularTwist
		}
	}
}
