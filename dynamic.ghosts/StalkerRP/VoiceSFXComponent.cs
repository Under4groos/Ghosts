using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000041 RID: 65
	public class VoiceSFXComponent : EntityComponent<StalkerPlayer>
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000F3B6 File Offset: 0x0000D5B6
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000F3BE File Offset: 0x0000D5BE
		private Sound CurrentSound { get; set; }

		// Token: 0x06000285 RID: 645 RVA: 0x0000F3C7 File Offset: 0x0000D5C7
		public void OnTakeDamage()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlaySound("human.pain");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeUntilSoundCanChange = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastPain = 0f;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000F403 File Offset: 0x0000D603
		public void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StopSound();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000F410 File Offset: 0x0000D610
		private void PlaySound(string sfx)
		{
			if (this.TimeUntilSoundCanChange > 0f)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentSound.Stop();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentSound = Sound.FromEntity(sfx, base.Entity);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000F45C File Offset: 0x0000D65C
		private void StopSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentSound.Stop();
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000F47D File Offset: 0x0000D67D
		private float breathingVolume
		{
			get
			{
				return 0.01f;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000F484 File Offset: 0x0000D684
		private float addedBreathingVolume
		{
			get
			{
				return 0.35f;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000F48B File Offset: 0x0000D68B
		private float timeBetweenRunningBreaths
		{
			get
			{
				return 1.25f;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000F492 File Offset: 0x0000D692
		private float timeUntilRunningBreathing
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000F499 File Offset: 0x0000D699
		private float staminaFractionForBreathing
		{
			get
			{
				return 0.35f;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		private void PlayRunningBreath()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlaySound("human.run");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentSound.SetVolume(this.breathingVolume + this.addedBreathingVolume * (1f - base.Entity.StaminaComponent.Fraction));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeUntilNextBreath = this.timeBetweenRunningBreaths;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeUntilNextLowHealthSound += 2f;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000F52C File Offset: 0x0000D72C
		private bool CanDoRunningBreath()
		{
			float staminaFraction = base.Entity.StaminaComponent.Fraction;
			return (base.Entity.StaminaComponent.TimeSinceStartedSprinting >= this.timeUntilRunningBreathing || staminaFraction <= this.staminaFractionForBreathing) && (this.timeUntilNextBreath < 0f && this.TimeUntilSoundCanChange < 0f) && (base.Entity.StaminaComponent.IsRunning || staminaFraction < this.staminaFractionForBreathing);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F5B5 File Offset: 0x0000D7B5
		private void UpdateRunningBreathing()
		{
			if (!this.CanDoRunningBreath())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayRunningBreath();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000F5CB File Offset: 0x0000D7CB
		private void PlayLowHealthSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlaySound("human.breathing_low_health");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeUntilSoundCanChange = 2f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeUntilNextLowHealthSound = 7f;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000F608 File Offset: 0x0000D808
		private bool CanDoLowHealthSounds()
		{
			return base.Entity.HealthComponent.GetEffectiveHealthFraction() <= 0.3f && this.timeSinceLastPain >= 2f && (this.timeUntilNextLowHealthSound < 0f && this.TimeUntilSoundCanChange < 0f) && !base.Entity.StaminaComponent.IsRunning;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000F67B File Offset: 0x0000D87B
		private void UpdateLowHealthSounds()
		{
			if (!this.CanDoLowHealthSounds())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayLowHealthSound();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F694 File Offset: 0x0000D894
		public void Simulate(Client client)
		{
			if (base.Entity.LifeState != LifeState.Alive)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CurrentSound.Stop();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateRunningBreathing();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateLowHealthSounds();
		}

		// Token: 0x040000D8 RID: 216
		private TimeUntil TimeUntilSoundCanChange;

		// Token: 0x040000D9 RID: 217
		private TimeSince timeSinceLastPain;

		// Token: 0x040000DA RID: 218
		private TimeUntil timeUntilNextBreath;

		// Token: 0x040000DB RID: 219
		private TimeUntil timeUntilNextLowHealthSound;
	}
}
