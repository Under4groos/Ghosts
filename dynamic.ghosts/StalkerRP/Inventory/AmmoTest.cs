using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.Inventory
{
	// Token: 0x020000FE RID: 254
	[Title("Ammo Test")]
	[Spawnable]
	public class AmmoTest : ItemEntity
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x0002F0D0 File Offset: 0x0002D2D0
		public override void Spawn()
		{
			Item item = new Item("item_ammo_9mm");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.InitializeFromAsset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetItem(item);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Item.Stacks = 5;
		}
	}
}
