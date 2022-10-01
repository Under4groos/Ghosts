using System;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000048 RID: 72
	public static class NetUtil
	{
		// Token: 0x06000306 RID: 774 RVA: 0x00011FAC File Offset: 0x000101AC
		public static Client GetClientFromNetID(int netID)
		{
			foreach (Client client in Client.All)
			{
				if (client.NetworkIdent == netID)
				{
					return client;
				}
			}
			return null;
		}
	}
}
