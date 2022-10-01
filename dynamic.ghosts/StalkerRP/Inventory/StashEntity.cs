using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Inventory.UI;

namespace StalkerRP.Inventory
{
	// Token: 0x02000104 RID: 260
	public class StashEntity : ModelEntity, IUse
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0002FA83 File Offset: 0x0002DC83
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x0002FA8B File Offset: 0x0002DC8B
		public Inventory Inventory { get; set; }

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002FA94 File Offset: 0x0002DC94
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.trigger = new StashTrigger();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.trigger.SetTriggerSize(this.triggerSize);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.trigger.SetParent(this, null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.trigger.SetStashParent(this);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
		public void InitialiseFromAsset(string stashAssetID)
		{
			StashResource stashAsset = StalkerResource.Get<StashResource>(stashAssetID);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Inventory = stashAsset.GenerateInventory();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel(stashAsset.WorldModel);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002FB38 File Offset: 0x0002DD38
		private void TryOpen(StalkerPlayer player)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Inventory.AddListener(player.Client);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Inventory.FullySyncWithListener(player.Client);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerMenu.ForceSetOpen(true);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002FB71 File Offset: 0x0002DD71
		public void OnPlayerLeft(StalkerPlayer player)
		{
			if (this.Inventory.RemoveListener(player.Client))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ClientInventoryManager.RemoveListenerFromInventory(To.Single(player.Client), this.Inventory.InventoryNetID);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				StalkerMenu.ForceSetOpen(false);
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002FBB1 File Offset: 0x0002DDB1
		bool IUse.IsUsable(Entity user)
		{
			return user is StalkerPlayer;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002FBBC File Offset: 0x0002DDBC
		bool IUse.OnUse(Entity user)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryOpen(user as StalkerPlayer);
			return false;
		}

		// Token: 0x040003AF RID: 943
		private float triggerSize = 90f;

		// Token: 0x040003B0 RID: 944
		private StashTrigger trigger;
	}
}
