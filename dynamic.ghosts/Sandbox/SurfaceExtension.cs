using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x02000159 RID: 345
	[Description("Extensions for Surfaces")]
	public static class SurfaceExtension
	{
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003DEA8 File Offset: 0x0003C0A8
		[Description("Create a particle effect and play an impact sound for this surface being hit by a bullet")]
		public static Particles DoBulletImpact(this Surface self, TraceResult tr)
		{
			if (!Prediction.FirstTime)
			{
				return null;
			}
			string decalPath = Rand.FromArray<string>(self.ImpactEffects.BulletDecal, null);
			Surface surf = self.GetBaseSurface();
			while (string.IsNullOrWhiteSpace(decalPath) && surf != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				decalPath = Rand.FromArray<string>(surf.ImpactEffects.BulletDecal, null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				surf = surf.GetBaseSurface();
			}
			DecalDefinition decal;
			if (!string.IsNullOrWhiteSpace(decalPath) && GlobalGameNamespace.ResourceLibrary.TryGet<DecalDefinition>(decalPath, out decal))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Decal.Place(decal, tr);
			}
			string sound = self.Sounds.Bullet;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			surf = self.GetBaseSurface();
			while (string.IsNullOrWhiteSpace(sound) && surf != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				sound = surf.Sounds.Bullet;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				surf = surf.GetBaseSurface();
			}
			if (!string.IsNullOrWhiteSpace(sound))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Sound.FromWorld(sound, tr.EndPosition);
			}
			string particleName = Rand.FromArray<string>(self.ImpactEffects.Bullet, null);
			if (string.IsNullOrWhiteSpace(particleName))
			{
				particleName = Rand.FromArray<string>(self.ImpactEffects.Regular, null);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			surf = self.GetBaseSurface();
			while (string.IsNullOrWhiteSpace(particleName) && surf != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				particleName = Rand.FromArray<string>(surf.ImpactEffects.Bullet, null);
				if (string.IsNullOrWhiteSpace(particleName))
				{
					particleName = Rand.FromArray<string>(surf.ImpactEffects.Regular, null);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				surf = surf.GetBaseSurface();
			}
			if (!string.IsNullOrWhiteSpace(particleName))
			{
				Particles particles = Particles.Create(particleName, tr.EndPosition);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				particles.SetForward(0, tr.Normal);
				return particles;
			}
			return null;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003E054 File Offset: 0x0003C254
		[Description("Create a footstep effect")]
		public static void DoFootstep(this Surface self, Entity ent, TraceResult tr, int foot, float volume)
		{
			string sound = (foot == 0) ? self.Sounds.FootLeft : self.Sounds.FootRight;
			if (!string.IsNullOrWhiteSpace(sound))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Sound.FromWorld(sound, tr.EndPosition).SetVolume(volume);
				return;
			}
			if (self.GetBaseSurface() != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.GetBaseSurface().DoFootstep(ent, tr, foot, volume);
			}
		}
	}
}
