using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x02000102 RID: 258
	public class ItemEntity : ModelEntity, IUse
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0002F9AB File Offset: 0x0002DBAB
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x0002F9B3 File Offset: 0x0002DBB3
		[Property]
		[ResourceType("weapon")]
		public string ItemAsset { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002F9BC File Offset: 0x0002DBBC
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0002F9C4 File Offset: 0x0002DBC4
		public Item Item { get; set; }

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002F9CD File Offset: 0x0002DBCD
		public void SetItem(Item item)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Item = item;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel(this.Item.Resource.WorldModel);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002FA04 File Offset: 0x0002DC04
		private void TryPickup(StalkerPlayer player)
		{
			if (player.InventoryComponent.TryPickupItem(this.Item))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Delete();
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002FA24 File Offset: 0x0002DC24
		bool IUse.IsUsable(Entity user)
		{
			return user is StalkerPlayer;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002FA2F File Offset: 0x0002DC2F
		bool IUse.OnUse(Entity user)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryPickup(user as StalkerPlayer);
			return false;
		}
	}
}
