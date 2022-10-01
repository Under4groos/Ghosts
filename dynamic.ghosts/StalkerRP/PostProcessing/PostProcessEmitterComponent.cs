using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.NPC;

namespace StalkerRP.PostProcessing
{
	// Token: 0x02000063 RID: 99
	public class PostProcessEmitterComponent : EntityComponent
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00016B4E File Offset: 0x00014D4E
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00016B56 File Offset: 0x00014D56
		private Sound sound { get; set; }

		// Token: 0x0600045B RID: 1115 RVA: 0x00016B60 File Offset: 0x00014D60
		protected override void OnDeactivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.sound.Stop();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			EffectsPostProcessManager.Instance.SetFilmGrain(0f);
			if (this.saturationEnabled)
			{
				EffectsPostProcessManager.Instance.SetSaturation(1f);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00016BAC File Offset: 0x00014DAC
		protected override void OnActivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateSound();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00016BBC File Offset: 0x00014DBC
		private void CreateSound()
		{
			if (string.IsNullOrWhiteSpace(this.soundName))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.sound = base.Entity.PlaySound(this.soundName);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.sound.SetVolume(0f);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00016C0C File Offset: 0x00014E0C
		public void SetUpEmitter(Color _screenColor, float _maxRange, float _maxColorMultIncrement, string _soundName, float _soundMaxVolume = 1f, float _maxFilmGrain = 0f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.screenColor = _screenColor;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.maxRange = _maxRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.maxColorMultIncrement = _maxColorMultIncrement;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.soundName = _soundName;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.soundMaxVolume = _soundMaxVolume;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.maxFilmGrian = _maxFilmGrain;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateSound();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.initialized = true;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00016C7B File Offset: 0x00014E7B
		public void AddSaturation(float _minSaturation)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.minSaturation = _minSaturation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.saturationEnabled = true;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00016C98 File Offset: 0x00014E98
		[Event.Tick.ClientAttribute]
		private void UpdatePsyFX()
		{
			if (!this.initialized)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.sound.SetVolume(0f);
				return;
			}
			NPCBase npcbase = base.Entity as NPCBase;
			if (npcbase != null && !npcbase.Active)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.sound.SetVolume(0f);
				return;
			}
			float dist = base.Entity.Position.Distance(CurrentView.Position);
			if (dist > this.maxRange)
			{
				return;
			}
			float delta = Time.Delta;
			float weight = 1f - dist / this.maxRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			weight = weight.Clamp(0f, 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.sound.SetVolume(this.soundMaxVolume * weight);
			if (this.maxColorMultIncrement > 0f)
			{
				EffectsPostProcessManager.Instance.AddColorMultiplier(this.screenColor, weight * this.maxColorMultIncrement * delta);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			EffectsPostProcessManager.Instance.SetFilmGrain(weight * this.maxFilmGrian);
			if (this.saturationEnabled)
			{
				EffectsPostProcessManager.Instance.SetSaturation((1f - weight).Clamp(this.minSaturation, 1f));
			}
		}

		// Token: 0x0400015C RID: 348
		private Color screenColor;

		// Token: 0x0400015D RID: 349
		private float maxRange;

		// Token: 0x0400015E RID: 350
		private float maxColorMultIncrement;

		// Token: 0x0400015F RID: 351
		private string soundName;

		// Token: 0x04000160 RID: 352
		private float soundMaxVolume;

		// Token: 0x04000161 RID: 353
		private float maxFilmGrian;

		// Token: 0x04000162 RID: 354
		private float minSaturation;

		// Token: 0x04000164 RID: 356
		private bool initialized;

		// Token: 0x04000165 RID: 357
		private bool saturationEnabled;
	}
}
