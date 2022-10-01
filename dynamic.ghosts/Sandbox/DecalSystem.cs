using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000167 RID: 359
	[Obsolete]
	public static class DecalSystem
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x0004159E File Offset: 0x0003F79E
		[Obsolete("Use Sandbox.Decal.Place")]
		[Description("Place this decal somewhere")]
		public static void PlaceUsingTrace(DecalDefinition decal, TraceResult tr)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Decal.Place(decal, tr);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000415AC File Offset: 0x0003F7AC
		[Obsolete("Use Sandbox.Decal.Place")]
		public static void Place(Material material, Entity ent, int bone, Vector3 localpos, Rotation localrot, Vector3 scale)
		{
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000415AE File Offset: 0x0003F7AE
		[Obsolete("Use Sandbox.Decal.Place")]
		public static void Place(To to, Material material, Entity ent, int bone, Vector3 localpos, Rotation localrot, Vector3 scale)
		{
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x000415B0 File Offset: 0x0003F7B0
		[Obsolete("Use Sandbox.Decal.PlaceMaterial")]
		[Description("Place a decal on an entity")]
		public static void PlaceOnEntity(Material material, Entity entity, int bone, Vector3 position, Rotation rotation, Vector3 scale)
		{
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x000415B2 File Offset: 0x0003F7B2
		[Obsolete("Use Sandbox.Decal.PlaceMaterial")]
		[Description("Place a decal on the world")]
		public static void PlaceOnWorld(Material material, Vector3 position, Rotation rotation, Vector3 scale)
		{
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000415B4 File Offset: 0x0003F7B4
		[Obsolete("Use Sandbox.Decal.PlaceMaterial")]
		[Description("Place a decal on the world")]
		public static void PlaceOnWorld(To to, Material material, Vector3 position, Rotation rotation, Vector3 scale)
		{
		}
	}
}
