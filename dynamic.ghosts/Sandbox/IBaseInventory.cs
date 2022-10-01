using System;

namespace Sandbox
{
	// Token: 0x02000161 RID: 353
	public interface IBaseInventory
	{
		// Token: 0x06001006 RID: 4102
		void OnChildAdded(Entity child);

		// Token: 0x06001007 RID: 4103
		void OnChildRemoved(Entity child);

		// Token: 0x06001008 RID: 4104
		void DeleteContents();

		// Token: 0x06001009 RID: 4105
		int Count();

		// Token: 0x0600100A RID: 4106
		Entity GetSlot(int i);

		// Token: 0x0600100B RID: 4107
		int GetActiveSlot();

		// Token: 0x0600100C RID: 4108
		bool SetActiveSlot(int i, bool allowempty);

		// Token: 0x0600100D RID: 4109
		bool SwitchActiveSlot(int idelta, bool loop);

		// Token: 0x0600100E RID: 4110
		Entity DropActive();

		// Token: 0x0600100F RID: 4111
		bool Drop(Entity ent);

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001010 RID: 4112
		Entity Active { get; }

		// Token: 0x06001011 RID: 4113
		bool SetActive(Entity ent);

		// Token: 0x06001012 RID: 4114
		bool Add(Entity ent, bool makeactive = false);

		// Token: 0x06001013 RID: 4115
		bool Contains(Entity ent);
	}
}
