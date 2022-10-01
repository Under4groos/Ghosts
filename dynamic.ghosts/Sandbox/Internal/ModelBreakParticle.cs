using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Sandbox.Internal
{
	// Token: 0x020001E2 RID: 482
	[Library("break_create_particle")]
	[Description("Spawn a particle system when this model breaks.")]
	internal class ModelBreakParticle : IModelBreakCommand
	{
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x00063F6D File Offset: 0x0006216D
		// (set) Token: 0x060017ED RID: 6125 RVA: 0x00063F75 File Offset: 0x00062175
		[JsonPropertyName("name")]
		[ResourceType("vpcf")]
		[Description("The particle to spawn when the model breaks.")]
		public string Particle { get; set; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00063F7E File Offset: 0x0006217E
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x00063F86 File Offset: 0x00062186
		[JsonPropertyName("cp0_model")]
		[ResourceType("vmdl")]
		[Description("(Optional) Set the particle control point #0 to the specified model.")]
		public string Model { get; set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00063F8F File Offset: 0x0006218F
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x00063F97 File Offset: 0x00062197
		[JsonPropertyName("cp0_snapshot")]
		[ResourceType("vsnap")]
		[Description("(Optional) Set the particle control point #0 to the specified snapshot.")]
		public string Snapshot { get; set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00063FA0 File Offset: 0x000621A0
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x00063FA8 File Offset: 0x000621A8
		[JsonPropertyName("damage_position_cp")]
		[DefaultValue(-1)]
		[Description("(Optional) Set this control point to the position of the break damage.")]
		public int? DamagePositionCP { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x00063FB1 File Offset: 0x000621B1
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x00063FB9 File Offset: 0x000621B9
		[JsonPropertyName("damage_direction_cp")]
		[DefaultValue(-1)]
		[Description("(Optional) Set this control point to the direction of the break damage.")]
		public int? DamageDirectionCP { get; set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00063FC2 File Offset: 0x000621C2
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x00063FCA File Offset: 0x000621CA
		[JsonPropertyName("velocity_cp")]
		[DefaultValue(-1)]
		[Description("(Optional) Set this control point to the velocity of the original prop.")]
		public int? VelocityCP { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00063FD3 File Offset: 0x000621D3
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x00063FDB File Offset: 0x000621DB
		[JsonPropertyName("angular_velocity_cp")]
		[DefaultValue(-1)]
		[Description("(Optional) Set this control point to the angular velocity of the original prop.")]
		public int? AngularVelocityCP { get; set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x00063FE4 File Offset: 0x000621E4
		// (set) Token: 0x060017FB RID: 6139 RVA: 0x00063FEC File Offset: 0x000621EC
		[JsonPropertyName("local_gravity_cp")]
		[DefaultValue(-1)]
		[Description("(Optional) Set this control point to global world gravity at the moment the model broke.")]
		public int? LocalGravityCP { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00063FF5 File Offset: 0x000621F5
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x00063FFD File Offset: 0x000621FD
		[JsonPropertyName("tint_cp")]
		[DefaultValue(-1)]
		[Description("(Optional) Set this control point to the tint color of the original prop as a vector with values 0 to 1.")]
		public int? TintCP { get; set; }

		// Token: 0x060017FE RID: 6142 RVA: 0x00064008 File Offset: 0x00062208
		public void OnBreak(Breakables.Result res)
		{
			ModelEntity ent = res.Source as ModelEntity;
			if (ent == null)
			{
				return;
			}
			Particles part = Particles.Create(this.Particle, ent.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			part.SetOrientation(0, ent.Rotation);
			if (!string.IsNullOrEmpty(this.Model))
			{
				part.SetModel(0, Sandbox.Model.Load(this.Model));
			}
			if (!string.IsNullOrEmpty(this.Snapshot))
			{
				part.SetSnapshot(0, this.Snapshot);
			}
			if (this.DamagePositionCP != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				part.SetPosition(this.DamagePositionCP.Value, res.Params.DamagePositon);
			}
			if (this.DamageDirectionCP != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				part.SetForward(this.DamageDirectionCP.Value, (res.Params.DamagePositon - ent.Position).Normal);
			}
			if (this.AngularVelocityCP != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				part.SetOrientation(this.AngularVelocityCP.Value, ent.AngularVelocity);
			}
			if (this.LocalGravityCP != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				part.SetPosition(this.LocalGravityCP.Value, GlobalGameNamespace.Map.Physics.Gravity);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				part.SetForward(this.LocalGravityCP.Value, GlobalGameNamespace.Map.Physics.Gravity.Normal);
			}
			if (this.VelocityCP != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				part.SetPosition(this.VelocityCP.Value, ent.Velocity);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				part.SetForward(this.VelocityCP.Value, ent.Velocity.Normal);
			}
			if (this.TintCP != null)
			{
				Color32 clr = ent.RenderColor.ToColor32(false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				part.SetPosition(this.TintCP.Value, new Vector3((float)clr.r, (float)clr.g, (float)clr.b));
			}
		}
	}
}
