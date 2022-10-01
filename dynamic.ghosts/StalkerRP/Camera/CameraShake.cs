using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Camera
{
	// Token: 0x02000132 RID: 306
	public class CameraShake
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x000362EB File Offset: 0x000344EB
		public bool IsFinished
		{
			get
			{
				return this.timeUntilFinished < 0f;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00036300 File Offset: 0x00034500
		public CameraShake(float magnitude, float roughness, float fadeInDuration = 0.1f, float fadeOutDuration = 0.5f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceStarted = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeUntilFinished = fadeInDuration + fadeOutDuration;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.tick = Time.Now * roughness;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.magnitude = magnitude;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.roughness = roughness;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.fadeInDuration = fadeInDuration;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.fadeOutDuration = fadeOutDuration;
			if (fadeInDuration < 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeUntilFinished = fadeOutDuration;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.sustain = false;
			}
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000363AC File Offset: 0x000345AC
		public Vector3 UpdateShake()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.shake.x = Noise.Perlin(this.tick, 0f) - 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.shake.y = Noise.Perlin(0f, this.tick) - 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.shake.z = Noise.Perlin(this.tick, this.tick) - 0.5f;
			if (this.fadeInDuration > 0f && this.sustain)
			{
				if (this.timeSinceStarted < this.fadeInDuration)
				{
					this.currentFadeTime = this.timeSinceStarted / this.fadeInDuration;
				}
				else if (this.fadeOutDuration > 0f)
				{
					this.sustain = false;
				}
			}
			if (!this.sustain)
			{
				this.currentFadeTime = this.timeUntilFinished / this.fadeOutDuration;
			}
			if (this.sustain)
			{
				this.tick = Time.Now * this.roughness;
			}
			else
			{
				this.tick = Time.Now * this.roughness * this.currentFadeTime;
			}
			return this.shake * this.magnitude * this.currentFadeTime;
		}

		// Token: 0x0400045E RID: 1118
		private Vector3 shake;

		// Token: 0x0400045F RID: 1119
		private float fadeInDuration;

		// Token: 0x04000460 RID: 1120
		private float currentFadeTime;

		// Token: 0x04000461 RID: 1121
		private float fadeOutDuration;

		// Token: 0x04000462 RID: 1122
		private float magnitude;

		// Token: 0x04000463 RID: 1123
		private float roughness;

		// Token: 0x04000464 RID: 1124
		private float tick;

		// Token: 0x04000465 RID: 1125
		private bool sustain = true;

		// Token: 0x04000466 RID: 1126
		private TimeUntil timeUntilFinished;

		// Token: 0x04000467 RID: 1127
		private TimeSince timeSinceStarted;
	}
}
