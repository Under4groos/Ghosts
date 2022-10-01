using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000183 RID: 387
	[Library("func_physbox")]
	[HammerEntity]
	[Solid]
	[RenderFields]
	[VisGroup(VisGroup.Physics)]
	[Title("Brush (Physics)")]
	[Category("Gameplay")]
	[Icon("desk")]
	[Description("A generic non model physics object.")]
	public class PhysicsBrushEntity : BasePhysics
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00048508 File Offset: 0x00046708
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00048510 File Offset: 0x00046710
		[Property("start_as", null, Title = "Start as")]
		public PhysicsBrushEntity.StartAsState StartAs { get; set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00048519 File Offset: 0x00046719
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00048526 File Offset: 0x00046726
		[Property("propdata", null, Title = "Physical material")]
		[Net]
		[Description("Physical surface properties of this physics mesh.")]
		public string PropData
		{
			get
			{
				return this._repback__PropData.GetValue();
			}
			set
			{
				this._repback__PropData.SetValue(value);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00048534 File Offset: 0x00046734
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x0004853C File Offset: 0x0004673C
		[Property("health", null, Title = "Health")]
		[Description("Amount of damage this entity can take before breaking")]
		protected float _health { get; set; }

		// Token: 0x06001288 RID: 4744 RVA: 0x00048548 File Offset: 0x00046748
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			if (this._health > 0f)
			{
				base.Health = this._health;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePhysics();
			if (this.StartAs.HasFlag(PhysicsBrushEntity.StartAsState.Motionless))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.MotionEnabled = false;
			}
			if (this.StartAs.HasFlag(PhysicsBrushEntity.StartAsState.Asleep))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsGroup.Sleeping = true;
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000485D6 File Offset: 0x000467D6
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePhysics();
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000485EE File Offset: 0x000467EE
		private void CreatePhysics()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsGroup.SetSurface(this.PropData);
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00048614 File Offset: 0x00046814
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x0004861C File Offset: 0x0004681C
		[Description("Fired when the entity gets damaged")]
		protected Entity.Output OnDamaged { get; set; }

		// Token: 0x0600128D RID: 4749 RVA: 0x00048628 File Offset: 0x00046828
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnDamaged.Fire(this, 0f);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0004865B File Offset: 0x0004685B
		[Input]
		[Description("Wake up this physics object, if it is sleeping.")]
		protected void Wake()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsGroup.Sleeping = false;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0004866E File Offset: 0x0004686E
		[Input]
		[Description("Wake up this physics object, if it is sleeping.")]
		protected void Sleep()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsGroup.Sleeping = true;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00048681 File Offset: 0x00046881
		[Input]
		[Description("Enable motion (gravity, etc) on this entity")]
		protected void EnableMotion()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.MotionEnabled = true;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00048694 File Offset: 0x00046894
		[Input]
		[Description("Disable motion (gravity, etc) on this entity")]
		protected void DisableMotion()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.MotionEnabled = false;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000486A7 File Offset: 0x000468A7
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarGeneric<string>>(ref this._repback__PropData, "PropData", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000486C3 File Offset: 0x000468C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnDamaged = new Entity.Output(this, "OnDamaged");
			base.CreateHammerOutputs();
		}

		// Token: 0x040005E8 RID: 1512
		private VarGeneric<string> _repback__PropData = new VarGeneric<string>();

		// Token: 0x02000248 RID: 584
		[Flags]
		public enum StartAsState
		{
			// Token: 0x04000997 RID: 2455
			Motionless = 1,
			// Token: 0x04000998 RID: 2456
			Asleep = 2
		}
	}
}
