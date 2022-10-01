using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000045 RID: 69
	public static class EntityExtensions
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00011B84 File Offset: 0x0000FD84
		public static bool CanSee(this Entity self, Vector3 point)
		{
			Vector3 center = self.WorldSpaceBounds.Center;
			return Trace.Ray(center, point).WorldOnly().Radius(1f).Run().EndPosition.AlmostEqual(point, 5f);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011BDC File Offset: 0x0000FDDC
		public static bool CanSee(this Entity self, Entity target)
		{
			return self.CanSee(target.WorldSpaceBounds.Center);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00011C00 File Offset: 0x0000FE00
		public static List<Entity> GetEntitiesInCone(Vector3 origin, Vector3 direction, float length, float angle)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			direction = direction.Normal;
			List<Entity> list = new List<Entity>();
			float halfLen = length / 2f;
			foreach (Entity entity in Entity.FindInSphere(origin + direction * halfLen, halfLen))
			{
				if (MathF.Acos((entity.Position - origin).Normal.Dot(direction)) <= angle)
				{
					list.Add(entity);
				}
			}
			return list;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		public static float DistanceToSquared(this Entity self, Entity target)
		{
			return (target.Position - self.Position).LengthSquared;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		public static TraceResult GetEyeTrace(this Entity entity)
		{
			Vector3 eyePosition = entity.EyePosition;
			Vector3 vector = entity.EyePosition + entity.EyeRotation.Forward * 5000f;
			Trace trace = Trace.Ray(eyePosition, vector);
			bool flag = true;
			return trace.Ignore(entity, flag).WithoutTags(new string[]
			{
				"trigger"
			}).WorldAndEntities().Run();
		}
	}
}
