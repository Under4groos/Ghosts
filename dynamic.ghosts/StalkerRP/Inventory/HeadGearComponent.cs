using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.Inventory
{
	// Token: 0x020000FB RID: 251
	public class HeadGearComponent : EntityComponent<StalkerPlayer>, ISingletonComponent
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0002E359 File Offset: 0x0002C559
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0002E370 File Offset: 0x0002C570
		[Net]
		[Local]
		public unsafe HeadGearEntity HeadGearEntity
		{
			get
			{
				return *this._repback__HeadGearEntity.GetValue();
			}
			set
			{
				VarUnmanaged<EntityHandle<HeadGearEntity>> repback__HeadGearEntity = this._repback__HeadGearEntity;
				EntityHandle<HeadGearEntity> entityHandle = value;
				repback__HeadGearEntity.SetValue(entityHandle);
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002E391 File Offset: 0x0002C591
		protected override void OnActivate()
		{
			if (Host.IsServer)
			{
				this.CreateEntity();
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002E3A0 File Offset: 0x0002C5A0
		public void OnPlayerDied()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.Enabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.SetLightEnabled(false);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
		private void CreateEntity()
		{
			if (this.HeadGearEntity != null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity = new HeadGearEntity();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.Initialize(base.Entity);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0002E3F8 File Offset: 0x0002C5F8
		public void OnHeadGearEquipped(HeadGearResource headGearData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.SetHeadGearData(headGearData);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.Active = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.SetLightEnabled(false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.AttachToPlayer(base.Entity);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002E45F File Offset: 0x0002C65F
		public void OnHeadGearUnequipped()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.Enabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.Active = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearEntity.SetLightEnabled(false);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002E494 File Offset: 0x0002C694
		public void Simulate(Client cl)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HeadGearEntity headGearEntity = this.HeadGearEntity;
			if (headGearEntity == null)
			{
				return;
			}
			headGearEntity.Simulate(cl);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002E4AC File Offset: 0x0002C6AC
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<EntityHandle<HeadGearEntity>>>(ref this._repback__HeadGearEntity, "HeadGearEntity", false, true);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x0400039C RID: 924
		private VarUnmanaged<EntityHandle<HeadGearEntity>> _repback__HeadGearEntity = new VarUnmanaged<EntityHandle<HeadGearEntity>>();
	}
}
