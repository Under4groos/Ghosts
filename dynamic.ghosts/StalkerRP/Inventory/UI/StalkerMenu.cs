using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using Sandbox.UI;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x02000120 RID: 288
	public class StalkerMenu : Panel
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00034174 File Offset: 0x00032374
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0003417C File Offset: 0x0003237C
		private StalkerGearTab GearTab { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00034185 File Offset: 0x00032385
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0003418D File Offset: 0x0003238D
		private StalkerHealthTab HealthTab { get; set; }

		// Token: 0x06000CBC RID: 3260 RVA: 0x00034196 File Offset: 0x00032396
		public StalkerMenu()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerMenu.Instance = this;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InitUI();
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000341B4 File Offset: 0x000323B4
		[Event.HotloadAttribute]
		private void InitUI()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerMenu.Instance.DeleteChildren(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("inventory/ui/StalkerMenu.scss", true);
			ButtonGroup buttonGroup = base.AddChild<ButtonGroup>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			buttonGroup.AddClass("tabs");
			Panel container = base.Add.Panel("container");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GearTab = container.AddChild<StalkerGearTab>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			buttonGroup.SelectedButton = buttonGroup.AddButtonActive("Gear", delegate(bool _)
			{
				this.SetActive(this.GearTab);
			});
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HealthTab = container.AddChild<StalkerHealthTab>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			buttonGroup.AddButtonActive("Health", delegate(bool _)
			{
				this.SetActive(this.HealthTab);
			});
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00034274 File Offset: 0x00032474
		private void SetActive(Panel newActive)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			newActive.SetClass("active", true);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00034287 File Offset: 0x00032487
		public void OnMenuClosed()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GearTab.OnClosed();
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00034299 File Offset: 0x00032499
		public void OnMenuOpened()
		{
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0003429C File Offset: 0x0003249C
		public void SetOpen(bool open)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Popup.CloseAll(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Parent.SetClass("menuopen", open);
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player != null && player.InventoryComponent != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				StalkerGearTab.Instance.CreatePlayerInventory();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				StalkerGearTab.Instance.CreatePlayerGear();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.isOpen = open;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0003430A File Offset: 0x0003250A
		private void ToggleMenu()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetOpen(!this.isOpen);
			if (this.isOpen)
			{
				this.OnMenuOpened();
				return;
			}
			this.OnMenuClosed();
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00034338 File Offset: 0x00032538
		[ClientRpc]
		public static void ForceSetOpen(bool open)
		{
			if (!StalkerMenu.ForceSetOpen__RpcProxy(open, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerMenu.Instance.SetOpen(open);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00034367 File Offset: 0x00032567
		[Event.BuildInputAttribute]
		private void ProcessClientInput(InputBuilder input)
		{
			if (input.Pressed(InputButton.Drop))
			{
				this.ToggleMenu();
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00034380 File Offset: 0x00032580
		private static bool ForceSetOpen__RpcProxy(bool open, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ForceSetOpen", new object[]
				{
					open
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1960780537, null))
			{
				if (!NetRead.IsSupported(open))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] ForceSetOpen is not allowed to use Boolean for the parameter 'open'!");
					return false;
				}
				writer.Write<bool>(open);
				writer.SendRpc(toTarget, null);
			}
			return false;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00034414 File Offset: 0x00032614
		public static void ForceSetOpen(To toTarget, bool open)
		{
			StalkerMenu.ForceSetOpen__RpcProxy(open, new To?(toTarget));
		}

		// Token: 0x04000410 RID: 1040
		public static StalkerMenu Instance;

		// Token: 0x04000413 RID: 1043
		private bool isOpen;
	}
}
