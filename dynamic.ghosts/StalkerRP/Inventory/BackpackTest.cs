using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x020000FF RID: 255
	[Library("item_entity_backpack", Title = "Backpack")]
	[Spawnable]
	public class BackpackTest : ItemEntity
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x0002F118 File Offset: 0x0002D318
		public override void Spawn()
		{
			Item item = new Item("item_storage_backpack");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.InitializeFromAsset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetItem(item);
		}
	}
}
