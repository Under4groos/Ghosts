using System;
using System.Runtime.CompilerServices;

namespace Sandbox.Internal.Tests
{
	// Token: 0x020001E6 RID: 486
	public static class CmdTest
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x0006489B File Offset: 0x00062A9B
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x000648A2 File Offset: 0x00062AA2
		[ConVar.ClientAttribute(null)]
		[DefaultValue("Client Value")]
		public static string test_clientvar { get; set; } = "Client Value";

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x000648D6 File Offset: 0x00062AD6
		// (set) Token: 0x0600182D RID: 6189 RVA: 0x000648AA File Offset: 0x00062AAA
		[ConVar.ClientDataAttribute(null)]
		public static string test_uservar
		{
			get
			{
				Host.AssertClient("test_uservar");
				return CmdTest._repback__test_uservar;
			}
			set
			{
				Host.AssertClient("test_uservar");
				if (CmdTest._repback__test_uservar == value)
				{
					return;
				}
				CmdTest._repback__test_uservar = value;
				ConsoleSystem.UpdateUserData("test_uservar", value, false);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x000648E7 File Offset: 0x00062AE7
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x000648EE File Offset: 0x00062AEE
		[ConVar.ServerAttribute(null)]
		[DefaultValue("Server Value")]
		public static string test_servervar { get; set; } = "Server Value";

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0006491E File Offset: 0x00062B1E
		// (set) Token: 0x06001831 RID: 6193 RVA: 0x000648F6 File Offset: 0x00062AF6
		[ConVar.ReplicatedAttribute(null)]
		public static string test_replicatedvar
		{
			get
			{
				return CmdTest._repback__test_replicatedvar;
			}
			set
			{
				if (CmdTest._repback__test_replicatedvar == value)
				{
					return;
				}
				CmdTest._repback__test_replicatedvar = value;
				if (Host.IsServer)
				{
					ConsoleSystem.SetValue("test_replicatedvar", value);
				}
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00064928 File Offset: 0x00062B28
		[ConCmd.ClientAttribute(null)]
		public static void test_clientcmd()
		{
			if (Host.IsServer)
			{
				throw new Exception("Trying to call test_clientcmd serverside!");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Client Command Success");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\ttest_clientvar: {0}", new object[]
			{
				CmdTest.test_clientvar
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\ttest_servervar: {0}", new object[]
			{
				CmdTest.test_servervar
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\ttest_uservar: {0}", new object[]
			{
				Local.Client.GetClientData("test_uservar", null)
			}));
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000649DC File Offset: 0x00062BDC
		[ConCmd.ServerAttribute(null)]
		public static void test_servercmd()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("test_servercmd");
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Server Command Success");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("Caller is {0}", new object[]
			{
				ConsoleSystem.Caller
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\ttest_clientvar: {0}", new object[]
			{
				CmdTest.test_clientvar
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\ttest_servervar: {0}", new object[]
			{
				CmdTest.test_servervar
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\ttest_uservar: {0}", new object[]
			{
				ConsoleSystem.Caller.GetClientData("test_uservar", null)
			}));
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00064AB8 File Offset: 0x00062CB8
		[ConCmd.AdminAttribute(null)]
		public static void test_admincmd()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("test_admincmd");
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Admin Command Success");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("Caller is {0}", new object[]
			{
				ConsoleSystem.Caller
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\ttest_clientvar: {0}", new object[]
			{
				CmdTest.test_clientvar
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\ttest_servervar: {0}", new object[]
			{
				CmdTest.test_servervar
			}));
		}

		// Token: 0x040007D9 RID: 2009
		public static string _repback__test_uservar = "Client User Var";

		// Token: 0x040007DA RID: 2010
		public static string _repback__test_replicatedvar = "Hairy Ball Bag";
	}
}
