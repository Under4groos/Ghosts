using System;
using System.Runtime.CompilerServices;

namespace Sandbox.camera
{
	// Token: 0x020001B9 RID: 441
	public class HitFlinch
	{
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x000575AD File Offset: 0x000557AD
		public bool IsFinished
		{
			get
			{
				return this.timeUntilFinished < 0f;
			}
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000575C4 File Offset: 0x000557C4
		public HitFlinch(float vertMagnitude, float horizMagnitude, float fadeIn, float fadeOut)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.fadeInTime = fadeIn;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.fadeOutTime = fadeOut;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeUntilFinished = fadeIn + fadeOut;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceStarted = 0f;
			if (fadeIn < 0f)
			{
				this.timeUntilFinished = fadeOut;
			}
			float noise = Noise.Perlin(Time.Now * 60f);
			float noise2 = Noise.Perlin(0f, Time.Now * 60f) - 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.targetRotation = new Angles(-vertMagnitude * noise, horizMagnitude * noise2, 0f).ToRotation();
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0005767F File Offset: 0x0005587F
		private float EaseOutQuad(float x)
		{
			return 1f - (1f - x) * (1f - x);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00057696 File Offset: 0x00055896
		private float EaseInQuad(float x)
		{
			return x * x;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0005769C File Offset: 0x0005589C
		public Rotation UpdateRotation()
		{
			Rotation currentRotation;
			if (this.timeSinceStarted < this.fadeInTime)
			{
				currentRotation = Rotation.Slerp(Rotation.Identity, this.targetRotation, this.EaseInQuad(this.timeSinceStarted / this.fadeInTime), true);
			}
			else
			{
				currentRotation = Rotation.Slerp(this.targetRotation, Rotation.Identity, this.EaseOutQuad(1f - this.timeUntilFinished / this.fadeOutTime), true);
			}
			return currentRotation;
		}

		// Token: 0x04000735 RID: 1845
		private TimeSince timeSinceStarted;

		// Token: 0x04000736 RID: 1846
		private TimeUntil timeUntilFinished;

		// Token: 0x04000737 RID: 1847
		private Rotation targetRotation;

		// Token: 0x04000738 RID: 1848
		private float fadeInTime;

		// Token: 0x04000739 RID: 1849
		private float fadeOutTime;
	}
}
