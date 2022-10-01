using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200015E RID: 350
	public class BaseInventory : IBaseInventory
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0003F18C File Offset: 0x0003D38C
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x0003F194 File Offset: 0x0003D394
		public Entity Owner { get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0003F19D File Offset: 0x0003D39D
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x0003F1B8 File Offset: 0x0003D3B8
		public virtual Entity Active
		{
			get
			{
				Player player = this.Owner as Player;
				if (player == null)
				{
					return null;
				}
				return player.ActiveChild;
			}
			set
			{
				Player player = this.Owner as Player;
				if (player != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					player.ActiveChild = value;
				}
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003F1E0 File Offset: 0x0003D3E0
		public BaseInventory(Entity owner)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = owner;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0003F200 File Offset: 0x0003D400
		[Description("Return true if this item belongs in the inventory")]
		public virtual bool CanAdd(Entity ent)
		{
			BaseCarriable bc = ent as BaseCarriable;
			return bc != null && bc.CanCarry(this.Owner);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0003F228 File Offset: 0x0003D428
		[Description("Delete every entity we're carrying. Useful to call on death.")]
		public virtual void DeleteContents()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("DeleteContents");
			foreach (Entity entity in this.List.ToArray())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				entity.Delete();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.List.Clear();
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0003F27B File Offset: 0x0003D47B
		[Description("Get the item in this slot")]
		public virtual Entity GetSlot(int i)
		{
			if (this.List.Count <= i)
			{
				return null;
			}
			if (i < 0)
			{
				return null;
			}
			return this.List[i];
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0003F29F File Offset: 0x0003D49F
		[Description("Returns the number of items in the inventory")]
		public virtual int Count()
		{
			return this.List.Count;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003F2AC File Offset: 0x0003D4AC
		[Description("Returns the index of the currently active child")]
		public virtual int GetActiveSlot()
		{
			Entity ae = this.Active;
			int count = this.Count();
			for (int i = 0; i < count; i++)
			{
				if (this.List[i] == ae)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003F2E5 File Offset: 0x0003D4E5
		[Description("Try to pick this entity up")]
		public virtual void Pickup(Entity ent)
		{
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003F2E7 File Offset: 0x0003D4E7
		[Description("A child has been added to the Owner (player). Do we want this entity in our inventory? Yeah? Add it then.")]
		public virtual void OnChildAdded(Entity child)
		{
			if (!this.CanAdd(child))
			{
				return;
			}
			if (this.List.Contains(child))
			{
				throw new Exception("Trying to add to inventory multiple times. This is gated by Entity:OnChildAdded and should never happen!");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.List.Add(child);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003F31D File Offset: 0x0003D51D
		[Description("A child has been removed from our Owner. This might not even be in our inventory, if it is then we'll remove it from our list")]
		public virtual void OnChildRemoved(Entity child)
		{
			this.List.Remove(child);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003F32C File Offset: 0x0003D52C
		[Description("Set our active entity to the entity on this slot")]
		public virtual bool SetActiveSlot(int i, bool evenIfEmpty = false)
		{
			Entity ent = this.GetSlot(i);
			if (this.Active == ent)
			{
				return false;
			}
			if (!evenIfEmpty && ent == null)
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Active = ent;
			return ent.IsValid();
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003F368 File Offset: 0x0003D568
		[Description("Switch to the slot next to the slot we have active.")]
		public virtual bool SwitchActiveSlot(int idelta, bool loop)
		{
			int count = this.Count();
			if (count == 0)
			{
				return false;
			}
			int nextSlot = this.GetActiveSlot() + idelta;
			if (loop)
			{
				while (nextSlot < 0)
				{
					nextSlot += count;
				}
				while (nextSlot >= count)
				{
					nextSlot -= count;
				}
			}
			else
			{
				if (nextSlot < 0)
				{
					return false;
				}
				if (nextSlot >= count)
				{
					return false;
				}
			}
			return this.SetActiveSlot(nextSlot, false);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003F3B8 File Offset: 0x0003D5B8
		[Description("Drop the active entity. If we can't drop it, will return null")]
		public virtual Entity DropActive()
		{
			if (!Host.IsServer)
			{
				return null;
			}
			Entity ac = this.Active;
			if (ac == null)
			{
				return null;
			}
			if (this.Drop(ac))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Active = null;
				return ac;
			}
			return null;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003F3F4 File Offset: 0x0003D5F4
		[Description("Drop this entity. Will return true if successfully dropped.")]
		public virtual bool Drop(Entity ent)
		{
			if (!Host.IsServer)
			{
				return false;
			}
			if (!this.Contains(ent))
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.Parent = null;
			BaseCarriable bc = ent as BaseCarriable;
			if (bc != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bc.OnCarryDrop(this.Owner);
			}
			return true;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003F43D File Offset: 0x0003D63D
		[Description("Returns true if this inventory contains this entity")]
		public virtual bool Contains(Entity ent)
		{
			return this.List.Contains(ent);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003F44B File Offset: 0x0003D64B
		[Description("Make this entity the active one")]
		public virtual bool SetActive(Entity ent)
		{
			if (this.Active == ent)
			{
				return false;
			}
			if (!this.Contains(ent))
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Active = ent;
			return true;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003F470 File Offset: 0x0003D670
		[Description("Try to add this entity to the inventory. Will return true if the entity was added successfully.")]
		public virtual bool Add(Entity ent, bool makeActive = false)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("Add");
			if (ent.Owner != null)
			{
				return false;
			}
			if (!this.CanAdd(ent))
			{
				return false;
			}
			BaseCarriable carriable = ent as BaseCarriable;
			if (carriable == null)
			{
				return false;
			}
			if (!carriable.CanCarry(this.Owner))
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.Parent = this.Owner;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			carriable.OnCarryStart(this.Owner);
			if (makeActive)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetActive(ent);
			}
			return true;
		}

		// Token: 0x04000505 RID: 1285
		public List<Entity> List = new List<Entity>();
	}
}
