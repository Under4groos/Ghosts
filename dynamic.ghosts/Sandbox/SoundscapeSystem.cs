using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000186 RID: 390
	public static class SoundscapeSystem
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x00049060 File Offset: 0x00047260
		[Event.FrameAttribute]
		public static void SoundscapeFrame()
		{
			Transform head = new Transform(CurrentView.Position, CurrentView.Rotation, 1f);
			string ss = SoundscapeSystem.FindSoundscapeAt(CurrentView.Position) ?? SoundscapeSystem.currentSoundScape;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SoundscapeSystem.snapTo = (SoundscapeSystem.snapTo || SoundscapeSystem.lastPosition.Distance(CurrentView.Position) > 100f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SoundscapeSystem.lastPosition = CurrentView.Position;
			if (SoundscapeSystem.currentSoundScape != ss)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				SoundscapeSystem.currentSoundScape = ss;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				SoundscapeSystem.StartSoundscape(SoundscapeSystem.currentSoundScape);
			}
			foreach (SoundscapeSystem.BaseEntry e in SoundscapeSystem.activeEntries)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				e.Frame(head);
				if (e.IsDead)
				{
					SoundscapeSystem.removalList.Add(e);
				}
			}
			foreach (SoundscapeSystem.BaseEntry e2 in SoundscapeSystem.removalList)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				e2.Dispose();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				SoundscapeSystem.activeEntries.Remove(e2);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SoundscapeSystem.removalList.Clear();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SoundscapeSystem.snapTo = false;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x000491D4 File Offset: 0x000473D4
		[Description("Load and start this soundscape..")]
		private static void StartSoundscape(string resourceName)
		{
			Soundscape rc = GlobalGameNamespace.ResourceLibrary.Get<Soundscape>(resourceName);
			foreach (SoundscapeSystem.BaseEntry baseEntry in SoundscapeSystem.activeEntries)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				baseEntry.Finished = true;
			}
			if (rc != null)
			{
				foreach (Soundscape.LoopedSound sound in rc.LoopedSounds)
				{
					SoundscapeSystem.StartLoopedSound(sound);
				}
				foreach (Soundscape.StingSound sound2 in rc.StingSounds)
				{
					SoundscapeSystem.StartStingSound(sound2);
				}
			}
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x000492B8 File Offset: 0x000474B8
		private static void StartLoopedSound(Soundscape.LoopedSound sound)
		{
			if (sound.SoundFile == null)
			{
				return;
			}
			using (IEnumerator<SoundscapeSystem.LoopedSoundEntry> enumerator = SoundscapeSystem.activeEntries.OfType<SoundscapeSystem.LoopedSoundEntry>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.TryUpdateFrom(sound))
					{
						return;
					}
				}
			}
			SoundscapeSystem.LoopedSoundEntry e = new SoundscapeSystem.LoopedSoundEntry(sound);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SoundscapeSystem.activeEntries.Add(e);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0004932C File Offset: 0x0004752C
		private static void StartStingSound(Soundscape.StingSound sound)
		{
			if (sound.SoundFile == null)
			{
				return;
			}
			for (int i = 0; i < sound.InstanceCount; i++)
			{
				SoundscapeSystem.StingSoundEntry e = new SoundscapeSystem.StingSoundEntry(sound);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				SoundscapeSystem.activeEntries.Add(e);
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0004936C File Offset: 0x0004756C
		private static string FindSoundscapeAt(Vector3 pos)
		{
			SoundscapeRadiusEntity bestEntity = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (SoundscapeSystem.entitySourceDebug == null)
			{
				SoundscapeSystem.entitySourceDebug = new List<string>();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SoundscapeSystem.entitySourceDebug.Clear();
			foreach (SoundscapeRadiusEntity ent in Entity.All.OfType<SoundscapeRadiusEntity>())
			{
				float distance = ent.Position.Distance(pos);
				if (!ent.TestPosition(pos))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					List<string> list = SoundscapeSystem.entitySourceDebug;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
					defaultInterpolatedStringHandler.AppendFormatted<SoundscapeRadiusEntity>(ent);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<float>(distance, "n0");
					defaultInterpolatedStringHandler.AppendLiteral(") - TestPosition == false");
					list.Add(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				else if (bestEntity != null && bestEntity.Position.Distance(pos) < ent.Position.Distance(pos))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					List<string> list2 = SoundscapeSystem.entitySourceDebug;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendFormatted<SoundscapeRadiusEntity>(ent);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<float>(distance, "n0");
					defaultInterpolatedStringHandler.AppendLiteral(")");
					list2.Add(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					List<string> list3 = SoundscapeSystem.entitySourceDebug;
					int index = 0;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendFormatted<SoundscapeRadiusEntity>(ent);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<float>(distance, "n0");
					defaultInterpolatedStringHandler.AppendLiteral(")");
					list3.Insert(index, defaultInterpolatedStringHandler.ToStringAndClear());
					RuntimeHelpers.EnsureSufficientExecutionStack();
					bestEntity = ent;
				}
			}
			if (bestEntity == null)
			{
				return null;
			}
			return bestEntity.Soundscape;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00049530 File Offset: 0x00047730
		[Event("asset.reload")]
		[Description("If a soundscape we're using has changed then try to reflect that immediately")]
		internal static void OnAssetReloaded(string name)
		{
			if (!Host.IsClient)
			{
				return;
			}
			if (SoundscapeSystem.currentSoundScape != name)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SoundscapeSystem.snapTo = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SoundscapeSystem.currentSoundScape = null;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00049560 File Offset: 0x00047760
		[DebugOverlay("soundscape", "Soundscape", "spatial_audio")]
		internal static void DrawDebugOverlay()
		{
			if (!Host.IsClient)
			{
				return;
			}
			Rect rect = new Rect(100.0, new Vector2(500f, 64f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.Draw2D.Color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.Draw2D.Box(rect, default(Vector4));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.Draw2D.Color = Color.White.WithAlpha(0.6f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.Draw2D.SetFont("Consolas", 10f, 400, false, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.Render2D draw2D = Render.Draw2D;
			Rect rect2 = rect.Shrink(8f);
			draw2D.DrawText(rect2, "Current Soundscape", TextFlag.LeftTop);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.Draw2D.Color = Color.White.WithAlpha(0.9f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.Draw2D.SetFont("Consolas", 14f, 500, false, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.Render2D draw2D2 = Render.Draw2D;
			rect2 = rect.Shrink(16f);
			draw2D2.DrawText(rect2, SoundscapeSystem.currentSoundScape ?? "none", TextFlag.LeftBottom);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			rect.Position += new Vector2(0f, rect.Height + 10f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			rect.Height = 32f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.Draw2D.FontSize = 13f;
			foreach (SoundscapeSystem.BaseEntry baseEntry in SoundscapeSystem.activeEntries)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				baseEntry.DrawDebugOverlay(rect);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				rect.Position += new Vector2(0f, rect.Height + 1f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			rect = new Rect(650f, 100f, 500f, 32f);
			if (SoundscapeSystem.entitySourceDebug != null)
			{
				foreach (string e in SoundscapeSystem.entitySourceDebug.Take(20))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Render.Draw2D.Color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Render.Draw2D.Box(rect, default(Vector4));
					Rect innerRect = rect.Shrink(8f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Render.Draw2D.Color = Color.White;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Render.Draw2D.DrawText(innerRect, e, TextFlag.LeftCenter);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					rect.Position += new Vector2(0f, rect.Height + 1f);
				}
			}
		}

		// Token: 0x04000604 RID: 1540
		private static string currentSoundScape;

		// Token: 0x04000605 RID: 1541
		private static bool snapTo;

		// Token: 0x04000606 RID: 1542
		private static Vector2 lastPosition;

		// Token: 0x04000607 RID: 1543
		private static List<string> entitySourceDebug;

		// Token: 0x04000608 RID: 1544
		private static List<SoundscapeSystem.BaseEntry> activeEntries = new List<SoundscapeSystem.BaseEntry>();

		// Token: 0x04000609 RID: 1545
		private static List<SoundscapeSystem.BaseEntry> removalList = new List<SoundscapeSystem.BaseEntry>();

		// Token: 0x0200024B RID: 587
		protected class BaseEntry : IDisposable
		{
			// Token: 0x170006F6 RID: 1782
			// (get) Token: 0x0600197B RID: 6523 RVA: 0x00069E1E File Offset: 0x0006801E
			[Description("True if this sound has finished, can be removed")]
			internal virtual bool IsDead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170006F7 RID: 1783
			// (get) Token: 0x0600197C RID: 6524 RVA: 0x00069E21 File Offset: 0x00068021
			// (set) Token: 0x0600197D RID: 6525 RVA: 0x00069E29 File Offset: 0x00068029
			[Description("Gets set when it's time to fade this out")]
			public bool Finished { get; set; }

			// Token: 0x0600197E RID: 6526 RVA: 0x00069E32 File Offset: 0x00068032
			public virtual void Frame(in Transform head)
			{
			}

			// Token: 0x0600197F RID: 6527 RVA: 0x00069E34 File Offset: 0x00068034
			public virtual void Dispose()
			{
			}

			// Token: 0x06001980 RID: 6528 RVA: 0x00069E36 File Offset: 0x00068036
			public virtual void DrawDebugOverlay(Rect rect)
			{
			}

			// Token: 0x040009AA RID: 2474
			protected SoundHandle handle;
		}

		// Token: 0x0200024C RID: 588
		protected sealed class LoopedSoundEntry : SoundscapeSystem.BaseEntry
		{
			// Token: 0x170006F8 RID: 1784
			// (get) Token: 0x06001982 RID: 6530 RVA: 0x00069E40 File Offset: 0x00068040
			[Description("Consider us dead if the soundscape system thinks we're finished and our volume is low")]
			internal override bool IsDead
			{
				get
				{
					return this.currentVolume <= 0f && base.Finished;
				}
			}

			// Token: 0x06001983 RID: 6531 RVA: 0x00069E58 File Offset: 0x00068058
			public LoopedSoundEntry(Soundscape.LoopedSound sound)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.currentVolume = 0f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.handle = GlobalGameNamespace.Audio.Play("core.ambient");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.handle.SetSoundFile(sound.SoundFile);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateFrom(sound);
			}

			// Token: 0x06001984 RID: 6532 RVA: 0x00069EB8 File Offset: 0x000680B8
			public override void Frame(in Transform head)
			{
				float targetVolume = this.sourceVolume;
				if (base.Finished)
				{
					targetVolume = 0f;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.currentVolume = this.currentVolume.Approach(targetVolume, Time.Delta / 5f);
				if (SoundscapeSystem.snapTo)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.currentVolume = targetVolume;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.handle.Volume = this.currentVolume;
			}

			// Token: 0x06001985 RID: 6533 RVA: 0x00069F25 File Offset: 0x00068125
			public override void Dispose()
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.handle.Stop(true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.handle = default(SoundHandle);
			}

			// Token: 0x06001986 RID: 6534 RVA: 0x00069F4C File Offset: 0x0006814C
			public override string ToString()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Looped - Finished:");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(base.Finished);
				defaultInterpolatedStringHandler.AppendLiteral(" volume:");
				defaultInterpolatedStringHandler.AppendFormatted<float>(this.currentVolume, "n0.00");
				defaultInterpolatedStringHandler.AppendLiteral(" - ");
				defaultInterpolatedStringHandler.AppendFormatted<Soundscape.LoopedSound>(this.source);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}

			// Token: 0x06001987 RID: 6535 RVA: 0x00069FBA File Offset: 0x000681BA
			[Description("If we're using the same sound file as this incoming sound, and we're on our way out.. then let it replace us instead. This is much nicer.")]
			public bool TryUpdateFrom(Soundscape.LoopedSound sound)
			{
				if (!base.Finished)
				{
					return false;
				}
				if (sound.SoundFile != this.source.SoundFile)
				{
					return false;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateFrom(sound);
				return true;
			}

			// Token: 0x06001988 RID: 6536 RVA: 0x00069FE8 File Offset: 0x000681E8
			private void UpdateFrom(Soundscape.LoopedSound sound)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.source = sound;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.sourceVolume = sound.Volume.GetValue();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Finished = false;
			}

			// Token: 0x06001989 RID: 6537 RVA: 0x0006A028 File Offset: 0x00068228
			public override void DrawDebugOverlay(Rect rect)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.Color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.Box(rect, default(Vector4));
				Rect innerRect = rect.Shrink(16f, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.Color = Color.White;
				if (base.Finished)
				{
					Render.Draw2D.Color = Color.White.WithAlpha(0.5f);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.DrawIcon(innerRect, "loop", 24f, TextFlag.LeftCenter);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				innerRect.left += 32f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Render2D draw2D = Render.Draw2D;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<Soundscape.LoopedSound>(this.source);
				draw2D.DrawText(innerRect, defaultInterpolatedStringHandler.ToStringAndClear(), TextFlag.LeftCenter);
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
				defaultInterpolatedStringHandler.AppendFormatted<float>(this.currentVolume * 100f, "n0");
				defaultInterpolatedStringHandler.AppendLiteral("%");
				string volume = defaultInterpolatedStringHandler.ToStringAndClear();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.DrawText(innerRect, volume, TextFlag.RightCenter);
			}

			// Token: 0x040009AC RID: 2476
			public float currentVolume;

			// Token: 0x040009AD RID: 2477
			private Soundscape.LoopedSound source;

			// Token: 0x040009AE RID: 2478
			private float sourceVolume;
		}

		// Token: 0x0200024D RID: 589
		protected sealed class StingSoundEntry : SoundscapeSystem.BaseEntry
		{
			// Token: 0x170006F9 RID: 1785
			// (get) Token: 0x0600198A RID: 6538 RVA: 0x0006A176 File Offset: 0x00068376
			[Description("Consider us dead if the soundscape system thinks we're finished and our volume is low")]
			internal override bool IsDead
			{
				get
				{
					return !this.handle.IsPlaying && base.Finished;
				}
			}

			// Token: 0x0600198B RID: 6539 RVA: 0x0006A190 File Offset: 0x00068390
			public StingSoundEntry(Soundscape.StingSound sound)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.source = sound;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeUntilNextShot = sound.RepeatTime.GetValue();
			}

			// Token: 0x0600198C RID: 6540 RVA: 0x0006A1D0 File Offset: 0x000683D0
			public override void Frame(in Transform head)
			{
				if (base.Finished)
				{
					return;
				}
				if (this.timeUntilNextShot <= 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.timeUntilNextShot = this.source.RepeatTime.GetValue();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.handle.Stop(false);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.handle = GlobalGameNamespace.Audio.Play(this.source.SoundFile.ResourcePath);
					Vector3 randomOffset = new Vector3(Rand.Float(-10f, 10f), Rand.Float(-10f, 10f), Rand.Float(-1f, 1f));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					randomOffset = randomOffset.Normal * this.source.Distance.GetValue();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.handle.Position = head.Position + randomOffset;
				}
			}

			// Token: 0x0600198D RID: 6541 RVA: 0x0006A2C9 File Offset: 0x000684C9
			public override void Dispose()
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.handle.Stop(true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.handle = default(SoundHandle);
			}

			// Token: 0x0600198E RID: 6542 RVA: 0x0006A2F0 File Offset: 0x000684F0
			public override void DrawDebugOverlay(Rect rect)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.Color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.Box(rect, default(Vector4));
				Rect innerRect = rect.Shrink(16f, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.Color = Color.White;
				if (this.handle.IsPlaying)
				{
					Render.Draw2D.Color = Color.Green;
				}
				if (base.Finished)
				{
					Render.Draw2D.Color = Render.Draw2D.Color.WithAlpha(0.5f);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.DrawIcon(innerRect, "volume_up", 24f, TextFlag.LeftCenter);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				innerRect.left += 32f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Render2D draw2D = Render.Draw2D;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<Soundscape.StingSound>(this.source);
				draw2D.DrawText(innerRect, defaultInterpolatedStringHandler.ToStringAndClear(), TextFlag.LeftCenter);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Render2D draw2D2 = Render.Draw2D;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
				defaultInterpolatedStringHandler.AppendFormatted<float>(this.timeUntilNextShot.Relative, "0.0");
				defaultInterpolatedStringHandler.AppendLiteral("s");
				draw2D2.DrawText(innerRect, defaultInterpolatedStringHandler.ToStringAndClear(), TextFlag.RightCenter);
			}

			// Token: 0x040009AF RID: 2479
			private Soundscape.StingSound source;

			// Token: 0x040009B0 RID: 2480
			private TimeUntil timeUntilNextShot;
		}
	}
}
