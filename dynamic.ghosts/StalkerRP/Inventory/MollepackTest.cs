using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x02000103 RID: 259
	[Library("item_entity_mollepack", Title = "Big Backpack")]
	[Spawnable]
	public class MollepackTest : ItemEntity
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0002FA4C File Offset: 0x0002DC4C
		public override void Spawn()
		{
			Item item = new Item("item_storage_mollepack");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.InitializeFromAsset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetItem(item);
		}
	}
}
