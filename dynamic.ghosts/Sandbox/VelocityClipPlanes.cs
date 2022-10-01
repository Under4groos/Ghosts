using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200015C RID: 348
	public struct VelocityClipPlanes : IDisposable
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0003EBC0 File Offset: 0x0003CDC0
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0003EBC8 File Offset: 0x0003CDC8
		[Description("Maximum number of planes that can be hit")]
		public int Max { readonly get; private set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0003EBD1 File Offset: 0x0003CDD1
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x0003EBD9 File Offset: 0x0003CDD9
		[Description("Number of planes we're currently holding")]
		public int Count { readonly get; private set; }

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003EBE4 File Offset: 0x0003CDE4
		public VelocityClipPlanes(Vector3 originalVelocity, int max = 5)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Max = max;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OrginalVelocity = originalVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BumpVelocity = originalVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Planes = ArrayPool<Vector3>.Shared.Rent(max);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Count = 0;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003EC38 File Offset: 0x0003CE38
		[Description("Try to add this plane and restrain velocity to it (and its brothers)")]
		public bool TryAdd(Vector3 normal, ref Vector3 velocity, float bounce)
		{
			if (this.Count == this.Max)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				velocity = 0f;
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3[] planes = this.Planes;
			int count = this.Count;
			this.Count = count + 1;
			planes[count] = normal;
			if (this.Count == 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.BumpVelocity = this.ClipVelocity(this.BumpVelocity, normal, 1f + bounce);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				velocity = this.BumpVelocity;
				return true;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			velocity = this.BumpVelocity;
			if (this.TryClip(ref velocity))
			{
				if (this.Count != 2)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					velocity = Vector3.Zero;
					return true;
				}
				Vector3 dir = Vector3.Cross(this.Planes[0], this.Planes[1]);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				velocity = dir.Normal * dir.Dot(velocity);
			}
			if (velocity.Dot(this.OrginalVelocity) < 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				velocity = 0f;
			}
			return true;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003ED6C File Offset: 0x0003CF6C
		[Description("Try to clip our velocity to all the planes, so we're not travelling into them Returns true if we clipped properly")]
		private bool TryClip(ref Vector3 velocity)
		{
			for (int i = 0; i < this.Count; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				velocity = this.ClipVelocity(this.BumpVelocity, this.Planes[i], 1f);
				if (this.MovingTowardsAnyPlane(velocity, i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003EDC4 File Offset: 0x0003CFC4
		[Description("Returns true if we're moving towards any of our planes (except for skip)")]
		private bool MovingTowardsAnyPlane(Vector3 velocity, int iSkip)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (i != iSkip && velocity.Dot(this.Planes[i]) < 0f)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003EE03 File Offset: 0x0003D003
		[Description("Start a new bump. Clears planes and resets BumpVelocity")]
		public void StartBump(Vector3 velocity)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BumpVelocity = velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Count = 0;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003EE20 File Offset: 0x0003D020
		[Description("Clip the velocity to the normal")]
		private Vector3 ClipVelocity(Vector3 vel, Vector3 norm, float overbounce = 1f)
		{
			float backoff = Vector3.Dot(vel, norm) * overbounce;
			Vector3 o = vel - norm * backoff;
			float adjust = Vector3.Dot(o, norm);
			if (adjust >= 1f)
			{
				return o;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			adjust = MathF.Min(adjust, -1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			return o - norm * adjust;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003EE7C File Offset: 0x0003D07C
		public void Dispose()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ArrayPool<Vector3>.Shared.Return(this.Planes, false);
		}

		// Token: 0x040004FE RID: 1278
		private Vector3 OrginalVelocity;

		// Token: 0x040004FF RID: 1279
		private Vector3 BumpVelocity;

		// Token: 0x04000500 RID: 1280
		private Vector3[] Planes;
	}
}
