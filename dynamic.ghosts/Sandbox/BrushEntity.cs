using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000179 RID: 377
	[Library("func_brush")]
	[Solid]
	[HammerEntity]
	[RenderFields]
	[VisGroup(VisGroup.Dynamic)]
	[Title("Brush (Static)")]
	[Category("Gameplay")]
	[Icon("brush")]
	[Description("A generic brush/mesh that can toggle its visibility and collisions, and can be broken.")]
	public class BrushEntity : ModelEntity
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00044ADE File Offset: 0x00042CDE
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x00044AE6 File Offset: 0x00042CE6
		[global::DefaultValue(true)]
		[Property]
		[Description("Whether this func_brush is visible/active at all")]
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._enabled = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CheckSolidityAndEnabled();
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00044AFF File Offset: 0x00042CFF
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x00044B07 File Offset: 0x00042D07
		[global::DefaultValue(true)]
		[Property]
		[Description("Whether this func_brush has collisions")]
		public bool Solid
		{
			get
			{
				return this._solid;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._solid = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CheckSolidityAndEnabled();
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00044B20 File Offset: 0x00042D20
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00044B28 File Offset: 0x00042D28
		[Property("health", null)]
		[Title("Health")]
		[global::DefaultValue(0)]
		[Description("If set to above 0, the entity will have this much health on spawn and will be breakable.")]
		protected float healthOverride { get; set; }

		// Token: 0x06001160 RID: 4448 RVA: 0x00044B31 File Offset: 0x00042D31
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Static, false);
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x00044B4C File Offset: 0x00042D4C
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x00044B54 File Offset: 0x00042D54
		[Description("Fired when the entity gets damaged, even if it is unbreakable.")]
		protected Entity.Output OnDamaged { get; set; }

		// Token: 0x06001163 RID: 4451 RVA: 0x00044B60 File Offset: 0x00042D60
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnDamaged.Fire(this, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LastDamage = info;
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00044B9F File Offset: 0x00042D9F
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x00044BA7 File Offset: 0x00042DA7
		[Description("Fired when the entity gets destroyed.")]
		protected Entity.Output OnBreak { get; set; }

		// Token: 0x06001166 RID: 4454 RVA: 0x00044BB0 File Offset: 0x00042DB0
		public override void OnKilled()
		{
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			Breakables.Result result = new Breakables.Result();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			result.CopyParamsFrom(this.LastDamage);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Breakables.Break(this, result);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnBreak.Fire(this.LastDamage.Attacker, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00044C18 File Offset: 0x00042E18
		[Input]
		[Description("Causes this prop to break, regardless if it is actually breakable or not. (i.e. ignores health and whether the model has gibs)")]
		public void Break()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnKilled();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LifeState = LifeState.Dead;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00044C3C File Offset: 0x00042E3C
		private void CheckSolidityAndEnabled()
		{
			if (this.Solid && this.Enabled)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableAllCollisions = true;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableAllCollisions = false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = this.Enabled;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00044C79 File Offset: 0x00042E79
		[Input]
		[Description("Make this func_brush non solid")]
		protected void DisableSolid()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Solid = false;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00044C87 File Offset: 0x00042E87
		[Input]
		[Description("Make this func_brush solid")]
		protected void EnableSolid()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Solid = true;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00044C95 File Offset: 0x00042E95
		[Input]
		[Description("Toggle solidity of this func_brush")]
		protected void ToggleSolid()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Solid = !this.Solid;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00044CAB File Offset: 0x00042EAB
		[Input]
		[Description("Enable this func_brush, making it visible")]
		protected void Enable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = true;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00044CB9 File Offset: 0x00042EB9
		[Input]
		[Description("Disable this func_brush, making it invisible and non solid")]
		protected void Disable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = false;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00044CC7 File Offset: 0x00042EC7
		[Input]
		[Description("Toggle this func_brush")]
		protected void Toggle()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = !this.Enabled;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00044CDD File Offset: 0x00042EDD
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnDamaged = new Entity.Output(this, "OnDamaged");
			this.OnBreak = new Entity.Output(this, "OnBreak");
			base.CreateHammerOutputs();
		}

		// Token: 0x04000560 RID: 1376
		private bool _enabled;

		// Token: 0x04000561 RID: 1377
		private bool _solid;

		// Token: 0x04000564 RID: 1380
		private DamageInfo LastDamage;
	}
}
