using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;

namespace StalkerRP
{
	// Token: 0x02000047 RID: 71
	public static class FortuneUtil
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00011E80 File Offset: 0x00010080
		public static bool IsMouseInside(this Panel panel)
		{
			Vector2 deltaPos = panel.ScreenPositionToPanelPosition(Mouse.Position);
			float width = panel.Box.Rect.Width;
			float height = panel.Box.Rect.Height;
			return deltaPos.x >= 0f && deltaPos.y >= 0f && deltaPos.x <= width && deltaPos.y <= height;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00011EF0 File Offset: 0x000100F0
		public static T GetRandom<T>(this List<T> list)
		{
			return list[Rand.Int(list.Count - 1)];
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00011F05 File Offset: 0x00010105
		public static T GetRandom<T>(this IList<T> list)
		{
			return list[Rand.Int(list.Count - 1)];
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00011F1A File Offset: 0x0001011A
		public static bool WasHeadshot(this ModelEntity entity, DamageInfo info)
		{
			return entity.GetHitboxGroup(info.HitboxIndex) == 1;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011F2C File Offset: 0x0001012C
		public static void ShrinkHead(this ModelEntity entity)
		{
			Transform bone = entity.GetBoneTransform("head", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			entity.SetBone("head", new Transform(bone.Position, bone.Rotation, 0f));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00011F6C File Offset: 0x0001016C
		public static void ResetHead(this ModelEntity entity)
		{
			Transform bone = entity.GetBoneTransform("head", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			entity.SetBone("head", new Transform(bone.Position, bone.Rotation, 1f));
		}
	}
}
