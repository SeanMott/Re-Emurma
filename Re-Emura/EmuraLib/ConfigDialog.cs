﻿using EvilMask.Emuera;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using trmb = EvilMask.Emuera.Lang.MessageBox;

namespace MinorShift.Emuera.Forms
{
	[global::System.Reflection.Obfuscation(Exclude = false)]
	internal enum ConfigDialogResult
	{
		Cancel = 0,
		Save = 1,
		SaveReboot = 2,
	}
	internal partial class ConfigDialog : Form
	{
		FlowLayoutPanel[] pages;
		public ConfigDialog()
		{
			InitializeComponent();
			numericUpDown1.Minimum = 1;//PrintCPerLine
			numericUpDown1.Maximum = 100;
			numericUpDown2.Minimum = 128;//ConfigCode.WindowX(Width)
			numericUpDown2.Maximum = 5000;
			numericUpDown3.Minimum = 128;//ConfigCode.WindowY(Height)
			numericUpDown3.Maximum = 5000;
			numericUpDown4.Minimum = 500;//MaxLog
			numericUpDown4.Maximum = 1000000;
			numericUpDown5.Minimum = 8;//FontSize
			numericUpDown5.Maximum = 144;
			numericUpDown6.Minimum = 8;//LineHeight
			numericUpDown6.Maximum = 144;
			numericUpDown7.Minimum = 1;//FPS
			numericUpDown7.Maximum = 240;
			numericUpDown8.Minimum = 1;//ScrollHeight
			numericUpDown8.Maximum = 10;
			numericUpDown9.Minimum = 1;//PrintCLength
			numericUpDown9.Maximum = 100;
			numericUpDown10.Minimum = 0;//InfiniteLoopAlertTime
			numericUpDown10.Maximum = 100000;
			numericUpDown11.Minimum = 20;//SaveDataNos
			numericUpDown11.Maximum = 80;
			numericUpDownPosX.Maximum = 10000;//WindowPosX
			numericUpDownPosY.Maximum = 10000;

			#region EE_AnchorのCB機能移植
			numericUpDownCBMaxCB.Minimum = 1;//CB Length of Clipboard
			numericUpDownCBMaxCB.Maximum = 200;
			numericUpDownCBBufferSize.Minimum = 50; //CB Buffer Size
			numericUpDownCBBufferSize.Maximum = 5000;
			numericUpDownCBScrollCount.Minimum = 1; //CB Scroll lines per key press
			numericUpDownCBScrollCount.Maximum = 100;
			numericUpDownCBMinTimer.Minimum = 300; //CB Min timer between updates
			numericUpDownCBMinTimer.Maximum = 60000;
			#endregion

			pages = new FlowLayoutPanel[] {
				flowLayoutPanel13,
				flowLayoutPanel17,
				flowLayoutPanel23,
				flowLayoutPanel27,
				flowLayoutPanel29,
				flowLayoutPanel30,
				flowLayoutPanel32,
				flowLayoutPanel33,
				flowLayoutPanel35,
				rikaiFlowLayoutPanel,
			};
		}
		internal void SetupLang(string[] langs)
		{
			var fisrt = this.comboBox7.Items[0];
			int selected = 0;
			int idx = 1;
			this.comboBox7.Items.Clear();
			this.comboBox7.Items.Add(fisrt);

			ConfigItem<string> item = (ConfigItem<string>)ConfigData.Instance.GetConfigItem(ConfigCode.EmueraLang);
			foreach (var lang in langs)
			{
				this.comboBox7.Items.Add(lang);
				if (lang == item.Value) selected = idx;
				idx++;
			}
			this.comboBox7.SelectedIndex = selected;
			comboBox7.Enabled = !item.Fixed;
		}
		internal void TranslateUI()
		{
			this.Text = Lang.UI.ConfigDialog.Text;

			this.tabEnvironment.Text = Lang.UI.ConfigDialog.Environment.Text;
			this.checkBox3.Text = Lang.UI.ConfigDialog.Environment.UseMouse.Text;
			this.checkBox4.Text = Lang.UI.ConfigDialog.Environment.UseMenu.Text;
			this.checkBox5.Text = Lang.UI.ConfigDialog.Environment.UseDebugCommand.Text;
			this.checkBox6.Text = Lang.UI.ConfigDialog.Environment.AllowMultipleInstances.Text;
			this.checkBox18.Text = Lang.UI.ConfigDialog.Environment.UseKeyMacro.Text;
			this.checkBox7.Text = Lang.UI.ConfigDialog.Environment.AutoSave.Text;
			this.checkBox24.Text = Lang.UI.ConfigDialog.Environment.UseSaveFolder.Text;
			this.checkBox33.Text = Lang.UI.ConfigDialog.Environment.EnglishConfigOutput.Text;
			this.label6.Text = Lang.UI.ConfigDialog.Environment.MaxLog.Text;
			this.label17.Text = Lang.UI.ConfigDialog.Environment.InfiniteLoopAlertTime.Text;
			this.label20.Text = Lang.UI.ConfigDialog.Environment.SaveDataPerPage.Text;
			this.label22.Text = Lang.UI.ConfigDialog.Environment.TextEditor.Text;
			this.button4.Text = Lang.UI.ConfigDialog.Environment.Browse.Text;
			this.label23.Text = Lang.UI.ConfigDialog.Environment.TextEditorCommandline.Text;
			this.comboBox6.Items[3] = Lang.UI.ConfigDialog.Environment.TextEditorCommandline.UserSetting.Text;

			this.tabPageView.Text = Lang.UI.ConfigDialog.Display.Text;
			this.label18.Text = Lang.UI.ConfigDialog.Display.TextDrawingMode.Text;
			this.label9.Text = Lang.UI.ConfigDialog.Display.FPS.Text;
			this.label5.Text = Lang.UI.ConfigDialog.Display.PrintCPerLine.Text;
			this.label1.Text = Lang.UI.ConfigDialog.Display.PrintCLength.Text;
			this.checkBox14.Text = Lang.UI.ConfigDialog.Display.ButtonWrap.Text;
			this.label26.Text = Lang.UI.ConfigDialog.Display.EmueraLang.Text;

			this.tabPageWindow.Text = Lang.UI.ConfigDialog.Window.Text;
			this.label2.Text = Lang.UI.ConfigDialog.Window.WindowWidth.Text;
			this.label3.Text = Lang.UI.ConfigDialog.Window.WindowHeight.Text;
			this.button1.Text = Lang.UI.ConfigDialog.Window.GetWindowSize.Text;
			this.checkBox8.Text = Lang.UI.ConfigDialog.Window.ChangeableWindowHeight.Text;
			this.checkBox21.Text = Lang.UI.ConfigDialog.Window.WindowMaximixed.Text;
			this.checkBox17.Text = Lang.UI.ConfigDialog.Window.SetWindowPos.Text;
			this.label19.Text = Lang.UI.ConfigDialog.Window.WindowX.Text;
			this.label10.Text = Lang.UI.ConfigDialog.Window.WindowY.Text;
			this.button3.Text = Lang.UI.ConfigDialog.Window.GetWindowPos.Text;
			this.ScrollRange.Text = Lang.UI.ConfigDialog.Window.LinesPerScroll.Text;

			this.tabPageFont.Text = Lang.UI.ConfigDialog.Font.Text;
			this.colorBoxBG.ButtonText = Lang.UI.ConfigDialog.Font.BackgroundColor.Text;
			this.colorBoxFG.ButtonText = Lang.UI.ConfigDialog.Font.TextColor.Text;
			this.colorBoxSelecting.ButtonText = Lang.UI.ConfigDialog.Font.HighlightColor.Text;
			this.colorBoxBacklog.ButtonText = Lang.UI.ConfigDialog.Font.LogHistoryColor.Text;
			this.label4.Text = Lang.UI.ConfigDialog.Font.FontName.Text;
			this.button2.Text = Lang.UI.ConfigDialog.Font.GetFontNames.Text;
			this.label8.Text = Lang.UI.ConfigDialog.Font.FontSize.Text;
			this.label7.Text = Lang.UI.ConfigDialog.Font.LineHeight.Text;

			this.tabPageSystem.Text = Lang.UI.ConfigDialog.System.Text;
			this.label21.Text = Lang.UI.ConfigDialog.System.Warning.Text;
			this.checkBox1.Text = Lang.UI.ConfigDialog.System.IgnoreCase.Text;
			this.checkBox2.Text = Lang.UI.ConfigDialog.System.UseRename.Text;
			this.checkBox10.Text = Lang.UI.ConfigDialog.System.UseReplace.Text;
			this.checkBox15.Text = Lang.UI.ConfigDialog.System.SearchSubfolder.Text;
			this.checkBox16.Text = Lang.UI.ConfigDialog.System.SortFileNames.Text;
			this.checkBox20.Text = Lang.UI.ConfigDialog.System.SystemFuncOverride.Text;
			this.checkBox19.Text = Lang.UI.ConfigDialog.System.SystemFuncOverrideWarn.Text;
			this.checkBox22.Text = Lang.UI.ConfigDialog.System.DuplicateFuncWarn.Text;
			this.checkBoxSystemFullSpace.Text = Lang.UI.ConfigDialog.System.WSIncludesFullWidth.Text;
			this.label11.Text = Lang.UI.ConfigDialog.System.ANSI.Text;

			this.tabPageSystem2.Text = Lang.UI.ConfigDialog.System2.Text;
			this.label24.Text = Lang.UI.ConfigDialog.System.Warning.Text;
			this.checkBoxSystemTripleSymbol.Text = Lang.UI.ConfigDialog.System2.IgnoreTripleSymbol.Text;
			this.checkBox26.Text = Lang.UI.ConfigDialog.System2.SaveInBinary.Text;
			this.checkBox32.Text = Lang.UI.ConfigDialog.System2.CompressSave.Text;
			this.checkBox29.Text = Lang.UI.ConfigDialog.System2.NoAutoCompleteCVar.Text;
			this.checkBox30.Text = Lang.UI.ConfigDialog.System2.DisallowUpdateCheck.Text;
			this.checkBox31.Text = Lang.UI.ConfigDialog.System2.UseERD.Text;
			this.checkBox34.Text = Lang.UI.ConfigDialog.System2.VarsizeDimConfig.Text;
			this.label25.Text = Lang.UI.ConfigDialog.System2.SaveLoadExt.Text;

			this.tabPageCompati.Text = Lang.UI.ConfigDialog.Compatibility.Text;
			this.label30.Text = Lang.UI.ConfigDialog.Compatibility.Warning.Text;
			this.checkBoxCompatiErrorLine.Text = Lang.UI.ConfigDialog.Compatibility.ExecuteErrorLine.Text;
			this.checkBoxCompatiCALLNAME.Text = Lang.UI.ConfigDialog.Compatibility.NameForCallname.Text;
			this.checkBoxCompatiRAND.Text = Lang.UI.ConfigDialog.Compatibility.EramakerRAND.Text;
			this.checkBox9.Text = Lang.UI.ConfigDialog.Compatibility.EramakerTIMES.Text;
			this.checkBoxFuncNoIgnoreCase.Text = Lang.UI.ConfigDialog.Compatibility.NoIgnoreCase.Text;
			this.checkBox28.Text = Lang.UI.ConfigDialog.Compatibility.CallEvent.Text;
			this.checkBoxCompatiSP.Text = Lang.UI.ConfigDialog.Compatibility.UseSPCharacters.Text;
			this.checkBoxCompatiLinefeedAs1739.Text = Lang.UI.ConfigDialog.Compatibility.ButtonWarp.Text;
			this.checkBox12.Text = Lang.UI.ConfigDialog.Compatibility.OmitArgs.Text;
			this.checkBox25.Text = Lang.UI.ConfigDialog.Compatibility.AutoTOSTR.Text;
			this.button7.Text = Lang.UI.ConfigDialog.Compatibility.EramakerStandard.Text;
			this.button8.Text = Lang.UI.ConfigDialog.Compatibility.EmueraStandard.Text;

			this.tabPageDebug.Text = Lang.UI.ConfigDialog.Debug.Text;
			this.checkBox23.Text = Lang.UI.ConfigDialog.Debug.CompatibilityWarn.Text;
			this.checkBox13.Text = Lang.UI.ConfigDialog.Debug.LoadingReport.Text;
			this.label12.Text = Lang.UI.ConfigDialog.Debug.ReduceArgs.Text;
			this.comboBoxReduceArgumentOnLoad.Items[0] = Lang.UI.ConfigDialog.Debug.ReduceArgs.Never.Text;
			this.comboBoxReduceArgumentOnLoad.Items[1] = Lang.UI.ConfigDialog.Debug.ReduceArgs.OnUpdate.Text;
			this.comboBoxReduceArgumentOnLoad.Items[2] = Lang.UI.ConfigDialog.Debug.ReduceArgs.Always.Text;
			this.label15.Text = Lang.UI.ConfigDialog.Debug.WarnLevel.Text;
			this.comboBox5.Items[0] = Lang.UI.ConfigDialog.Debug.WarnLevel.Level0.Text;
			this.comboBox5.Items[1] = Lang.UI.ConfigDialog.Debug.WarnLevel.Level1.Text;
			this.comboBox5.Items[2] = Lang.UI.ConfigDialog.Debug.WarnLevel.Level2.Text;
			this.comboBox5.Items[3] = Lang.UI.ConfigDialog.Debug.WarnLevel.Level3.Text;
			this.checkBox11.Text = Lang.UI.ConfigDialog.Debug.IgnoreUnusedFuncs.Text;
			this.label13.Text = Lang.UI.ConfigDialog.Debug.FuncNotFoundWarn.Text;
			this.comboBox3.Items[0] = Lang.UI.ConfigDialog.Debug.WarnSetting.Ignore.Text;
			this.comboBox3.Items[1] = Lang.UI.ConfigDialog.Debug.WarnSetting.TotalNumber.Text;
			this.comboBox3.Items[2] = Lang.UI.ConfigDialog.Debug.WarnSetting.OncePerFile.Text;
			this.comboBox3.Items[3] = Lang.UI.ConfigDialog.Debug.WarnSetting.Always.Text;
			this.label14.Text = Lang.UI.ConfigDialog.Debug.UnusedFuncWarn.Text;
			this.comboBox4.Items[0] = Lang.UI.ConfigDialog.Debug.WarnSetting.Ignore.Text;
			this.comboBox4.Items[1] = Lang.UI.ConfigDialog.Debug.WarnSetting.TotalNumber.Text;
			this.comboBox4.Items[2] = Lang.UI.ConfigDialog.Debug.WarnSetting.OncePerFile.Text;
			this.comboBox4.Items[3] = Lang.UI.ConfigDialog.Debug.WarnSetting.Always.Text;
			this.button5.Text = Lang.UI.ConfigDialog.Debug.PlayerStandard.Text;
			this.button6.Text = Lang.UI.ConfigDialog.Debug.DeveloperStandard.Text;

			this.tabPageClipboard.Text = Lang.UI.ConfigDialog.Clipboard.Text;
			this.checkBoxCBuseCB.Text = Lang.UI.ConfigDialog.Clipboard.CopyToClipboard.Text;
			this.checkBoxCBIgnoreTags.Text = Lang.UI.ConfigDialog.Clipboard.IgnoreTags.Text;
			this.label29.Text = Lang.UI.ConfigDialog.Clipboard.ReplaceTags.Text;
			this.checkBoxCBNewLinesOnly.Text = Lang.UI.ConfigDialog.Clipboard.NewLineOnly.Text;
			this.checkBoxCBClearBuffer.Text = Lang.UI.ConfigDialog.Clipboard.ClearClipboard.Text;
			this.label27.Text = Lang.UI.ConfigDialog.Clipboard.TriggerToUse.Text;
			this.checkBoxCBTriggerLeftClick.Text = Lang.UI.ConfigDialog.Clipboard.LClick.Text;
			this.checkBoxCBTriggerMiddleClick.Text = Lang.UI.ConfigDialog.Clipboard.MClick.Text;
			this.checkBoxCBTriggerDoubleLeftClick.Text = Lang.UI.ConfigDialog.Clipboard.DoubleClick.Text;
			this.checkBoxCBTriggerAnyKeyWait.Text = Lang.UI.ConfigDialog.Clipboard.AnyKeyWait.Text;
			this.checkBoxCBTriggerInputWait.Text = Lang.UI.ConfigDialog.Clipboard.InputWait.Text;
			this.label28.Text = Lang.UI.ConfigDialog.Clipboard.LinesToClipboard.Text;
			this.label31.Text = Lang.UI.ConfigDialog.Clipboard.TotalBuffer.Text;
			this.label32.Text = Lang.UI.ConfigDialog.Clipboard.LinesToScroll.Text;
			this.label33.Text = Lang.UI.ConfigDialog.Clipboard.UpdateTime.Text;
			this.label34.Text = Lang.UI.ConfigDialog.Clipboard.ScrollThrough.Text;

			this.tabPageRikai.Text = Lang.UI.ConfigDialog.Rikai.Text;
			this.rikaiCheckBoxEnable.Text = Lang.UI.ConfigDialog.Rikai.RikaiEnable.Text;
			this.rikaiDictFilenameLabel.Text = Lang.UI.ConfigDialog.Rikai.RikaiFilename.Text;
			this.rikaiColorBoxBG.Text = Lang.UI.ConfigDialog.Font.BackgroundColor.Text;
			this.rikaiColorBoxText.Text = Lang.UI.ConfigDialog.Font.TextColor.Text;
			this.rikaiCheckBoxSeparateBoxes.Text = Lang.UI.ConfigDialog.Rikai.RikaiSeparateBox.Text;
			this.rikaiNote1.Text = Lang.UI.ConfigDialog.Rikai.RikaiLink.Text;
			this.rikaiNote3.Text = Lang.UI.ConfigDialog.Rikai.OtherEDICT1.Text;


			this.buttonSave.Text = Lang.UI.ConfigDialog.Save.Text;
			this.buttonReboot.Text = Lang.UI.ConfigDialog.SaveAndRestart.Text;
			this.buttonCancel.Text = Lang.UI.ConfigDialog.Cancel.Text;
			this.label16.Text = Lang.UI.ConfigDialog.ChangeWontTakeEffectUntilRestart.Text;

			var diff = tabControl.Size - tabControl.DisplayRectangle.Size + ((Size)tabControl.Padding);
			var size = new Size(0, 0);
			foreach (var page in pages)
			{
				if (page.Size.Width + page.Margin.Size.Width > size.Width) size.Width = page.Size.Width + page.Margin.Size.Width;
				if (page.Size.Height + page.Margin.Size.Height > size.Height) size.Height = page.Size.Height + page.Margin.Size.Height;
			}
			this.tabControl.Size = new Size(size.Width + diff.Width, tabControl.Size.Height);
			diff = tabControl.Size - tabControl.DisplayRectangle.Size + ((Size)tabControl.Padding);
			tabControl.Size = size + diff;

			foreach (var page in pages)
			{
				diff = tabControl.DisplayRectangle.Size - page.Size;
				page.Location = new Point(diff.Width / 2, diff.Height / 2);
			}

		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveConfig();
			Result = ConfigDialogResult.Save;
			this.Close();
		}

		private void buttonReboot_Click(object sender, EventArgs e)
		{
			SaveConfig();
			Result = ConfigDialogResult.SaveReboot;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Result = ConfigDialogResult.Cancel;
			this.Close();
		}
		public ConfigDialogResult Result = ConfigDialogResult.Cancel;

		void setCheckBox(CheckBox checkbox, ConfigCode code)
		{
			ConfigItem<bool> item = (ConfigItem<bool>)ConfigData.Instance.GetConfigItem(code);
			checkbox.Checked = item.Value;
			checkbox.Enabled = !item.Fixed;
		}
		void setNumericUpDown(NumericUpDown updown, ConfigCode code)
		{
			ConfigItem<int> item = (ConfigItem<int>)ConfigData.Instance.GetConfigItem(code);
			decimal value = item.Value;
			if (updown.Maximum < value)
				updown.Maximum = value;
			if (updown.Minimum > value)
				updown.Minimum = value;
			updown.Value = value;
			updown.Enabled = !item.Fixed;
		}

		void setColorBox(ColorBox colorBox, ConfigCode code)
		{
			ConfigItem<Color> item = (ConfigItem<Color>)ConfigData.Instance.GetConfigItem(code);
			colorBox.SelectingColor = item.Value;
			colorBox.Enabled = !item.Fixed;
		}
		/*		void setTextBox(TextBox textBox, ConfigCode code)
				{
					ConfigItem<string> item = (ConfigItem<string>)ConfigData.Instance.GetConfigItem(code);
					textBox.Text = item.Value;
					textBox.Enabled = !item.Fixed;
				}
		*/
		MainWindow parent = null;
		public void SetConfig(MainWindow mainWindow)
		{
			parent = mainWindow;
			//ConfigData config = ConfigData.Instance;
			setCheckBox(checkBox1, ConfigCode.IgnoreCase);
			setCheckBox(checkBox2, ConfigCode.UseRenameFile);
			setCheckBox(checkBox3, ConfigCode.UseMouse);
			setCheckBox(checkBox4, ConfigCode.UseMenu);
			setCheckBox(checkBox5, ConfigCode.UseDebugCommand);
			setCheckBox(checkBox6, ConfigCode.AllowMultipleInstances);
			setCheckBox(checkBox7, ConfigCode.AutoSave);
			setCheckBox(checkBox8, ConfigCode.SizableWindow);
			setCheckBox(checkBox10, ConfigCode.UseReplaceFile);
			setCheckBox(checkBox11, ConfigCode.IgnoreUncalledFunction);
			//setCheckBox(checkBox12, ConfigCode.ReduceFormattedStringOnLoad);
			setCheckBox(checkBox13, ConfigCode.DisplayReport);
			setCheckBox(checkBox14, ConfigCode.ButtonWrap);
			setCheckBox(checkBox15, ConfigCode.SearchSubdirectory);
			setCheckBox(checkBox16, ConfigCode.SortWithFilename);
			setCheckBox(checkBox17, ConfigCode.SetWindowPos);
			setCheckBox(checkBox18, ConfigCode.UseKeyMacro);
			setCheckBox(checkBox20, ConfigCode.AllowFunctionOverloading);
			setCheckBox(checkBox19, ConfigCode.WarnFunctionOverloading);
			setCheckBox(checkBox21, ConfigCode.WindowMaximixed);
			setCheckBox(checkBox22, ConfigCode.WarnNormalFunctionOverloading);
			setCheckBox(checkBox23, ConfigCode.WarnBackCompatibility);
			setCheckBox(checkBoxCompatiErrorLine, ConfigCode.CompatiErrorLine);
			setCheckBox(checkBoxCompatiCALLNAME, ConfigCode.CompatiCALLNAME);
			setCheckBox(checkBox24, ConfigCode.UseSaveFolder);
			#region EM_私家版_Emuera多言語化改造
			setCheckBox(checkBox33, ConfigCode.EnglishConfigOutput);
			#endregion
			setCheckBox(checkBoxCompatiRAND, ConfigCode.CompatiRAND);
			setCheckBox(checkBoxCompatiLinefeedAs1739, ConfigCode.CompatiLinefeedAs1739);
			setCheckBox(checkBox28, ConfigCode.CompatiCallEvent);
			setCheckBox(checkBoxFuncNoIgnoreCase, ConfigCode.CompatiFunctionNoignoreCase);
			setCheckBox(checkBoxSystemFullSpace, ConfigCode.SystemAllowFullSpace);
			setCheckBox(checkBox12, ConfigCode.CompatiFuncArgOptional);
			setCheckBox(checkBox25, ConfigCode.CompatiFuncArgAutoConvert);
			setCheckBox(checkBox26, ConfigCode.SystemSaveInBinary);
			setCheckBox(checkBoxSystemTripleSymbol, ConfigCode.SystemIgnoreTripleSymbol);
			setCheckBox(checkBoxCompatiSP, ConfigCode.CompatiSPChara);
			setCheckBox(checkBox9, ConfigCode.TimesNotRigorousCalculation);
			setCheckBox(checkBox29, ConfigCode.SystemNoTarget);
			setCheckBox(checkBox30, ConfigCode.ForbidUpdateCheck);
			setCheckBox(checkBox31, ConfigCode.UseERD);
			setCheckBox(checkBox34, ConfigCode.VarsizeDimConfig);
			setCheckBox(checkBox35, ConfigCode.CheckDuplicateIdentifier);
			#region EM_私家版_セーブ圧縮
			setCheckBox(checkBox32, ConfigCode.ZipSaveData);
			#endregion
			setNumericUpDown(numericUpDown2, ConfigCode.WindowX);
			setNumericUpDown(numericUpDown3, ConfigCode.WindowY);
			setNumericUpDown(numericUpDown4, ConfigCode.MaxLog);
			setNumericUpDown(numericUpDown1, ConfigCode.PrintCPerLine);
			setNumericUpDown(numericUpDown9, ConfigCode.PrintCLength);
			setNumericUpDown(numericUpDown6, ConfigCode.LineHeight);
			setNumericUpDown(numericUpDown7, ConfigCode.FPS);
			setNumericUpDown(numericUpDown8, ConfigCode.ScrollHeight);
			setNumericUpDown(numericUpDown5, ConfigCode.FontSize);
			setNumericUpDown(numericUpDown10, ConfigCode.InfiniteLoopAlertTime);
			setNumericUpDown(numericUpDown11, ConfigCode.SaveDataNos);

			setNumericUpDown(numericUpDownPosX, ConfigCode.WindowPosX);
			setNumericUpDown(numericUpDownPosY, ConfigCode.WindowPosY);

			setColorBox(colorBoxFG, ConfigCode.ForeColor);
			setColorBox(colorBoxBG, ConfigCode.BackColor);
			setColorBox(colorBoxSelecting, ConfigCode.FocusColor);
			setColorBox(colorBoxBacklog, ConfigCode.LogColor);


			ConfigItem<TextDrawingMode> itemTDM = (ConfigItem<TextDrawingMode>)ConfigData.Instance.GetConfigItem(ConfigCode.TextDrawingMode);
			switch (itemTDM.Value)
			{
				case TextDrawingMode.WINAPI:
					comboBoxTextDrawingMode.SelectedIndex = 0; break;
				case TextDrawingMode.TEXTRENDERER:
					comboBoxTextDrawingMode.SelectedIndex = 1; break;
				case TextDrawingMode.GRAPHICS:
					comboBoxTextDrawingMode.SelectedIndex = 2; break;
			}
			comboBoxTextDrawingMode.Enabled = !itemTDM.Fixed;

			ConfigItem<string> itemStr = (ConfigItem<string>)ConfigData.Instance.GetConfigItem(ConfigCode.FontName);
			string fontname = itemStr.Value;
			int nameIndex = comboBox2.Items.IndexOf(fontname);
			if (nameIndex >= 0)
				comboBox2.SelectedIndex = nameIndex;
			else
			{
				comboBox2.Text = fontname;
				//nameIndex = comboBox2.Items.IndexOf("ＭＳ ゴシック");
				//if (nameIndex >= 0)
				//    comboBox2.SelectedIndex = nameIndex;
			}
			comboBox2.Enabled = !itemStr.Fixed;


			ConfigItem<ReduceArgumentOnLoadFlag> itemRA = (ConfigItem<ReduceArgumentOnLoadFlag>)ConfigData.Instance.GetConfigItem(ConfigCode.ReduceArgumentOnLoad);
			switch (itemRA.Value)
			{
				case ReduceArgumentOnLoadFlag.NO:
					comboBoxReduceArgumentOnLoad.SelectedIndex = 0; break;
				case ReduceArgumentOnLoadFlag.ONCE:
					comboBoxReduceArgumentOnLoad.SelectedIndex = 1; break;
				case ReduceArgumentOnLoadFlag.YES:
					comboBoxReduceArgumentOnLoad.SelectedIndex = 2; break;
			}
			comboBoxReduceArgumentOnLoad.Enabled = !itemRA.Fixed;


			ConfigItem<int> itemInt = (ConfigItem<int>)ConfigData.Instance.GetConfigItem(ConfigCode.DisplayWarningLevel);
			if (itemInt.Value <= 0)
				comboBox5.SelectedIndex = 0;
			else if (itemInt.Value >= 3)
				comboBox5.SelectedIndex = 3;
			else
				comboBox5.SelectedIndex = itemInt.Value;
			comboBox5.Enabled = !itemInt.Fixed;


			ConfigItem<DisplayWarningFlag> itemDWF = (ConfigItem<DisplayWarningFlag>)ConfigData.Instance.GetConfigItem(ConfigCode.FunctionNotFoundWarning);
			switch (itemDWF.Value)
			{
				case DisplayWarningFlag.IGNORE:
					comboBox3.SelectedIndex = 0; break;
				case DisplayWarningFlag.LATER:
					comboBox3.SelectedIndex = 1; break;
				case DisplayWarningFlag.ONCE:
					comboBox3.SelectedIndex = 2; break;
				case DisplayWarningFlag.DISPLAY:
					comboBox3.SelectedIndex = 3; break;
			}
			comboBox3.Enabled = !itemDWF.Fixed;

			itemDWF = (ConfigItem<DisplayWarningFlag>)ConfigData.Instance.GetConfigItem(ConfigCode.FunctionNotCalledWarning);
			switch (itemDWF.Value)
			{
				case DisplayWarningFlag.IGNORE:
					comboBox4.SelectedIndex = 0; break;
				case DisplayWarningFlag.LATER:
					comboBox4.SelectedIndex = 1; break;
				case DisplayWarningFlag.ONCE:
					comboBox4.SelectedIndex = 2; break;
				case DisplayWarningFlag.DISPLAY:
					comboBox4.SelectedIndex = 3; break;
			}
			comboBox4.Enabled = !itemDWF.Fixed;

			ConfigItem<UseLanguage> itemLang = (ConfigItem<UseLanguage>)ConfigData.Instance.GetConfigItem(ConfigCode.useLanguage);
			switch (itemLang.Value)
			{
				case UseLanguage.JAPANESE:
					comboBox1.SelectedIndex = 0; break;
				case UseLanguage.KOREAN:
					comboBox1.SelectedIndex = 1; break;
				case UseLanguage.CHINESE_HANS:
					comboBox1.SelectedIndex = 2; break;
				case UseLanguage.CHINESE_HANT:
					comboBox1.SelectedIndex = 3; break;
			}

			ConfigItem<TextEditorType> itemET = (ConfigItem<TextEditorType>)ConfigData.Instance.GetConfigItem(ConfigCode.EditorType);
			switch (itemET.Value)
			{
				case TextEditorType.SAKURA:
					comboBox6.SelectedIndex = 0; break;
				case TextEditorType.TERAPAD:
					comboBox6.SelectedIndex = 1; break;
				case TextEditorType.EMEDITOR:
					comboBox6.SelectedIndex = 2; break;
				case TextEditorType.USER_SETTING:
					comboBox6.SelectedIndex = 3; break;
			}
			comboBox6.Enabled = !itemET.Fixed;


			textBox1.Text = Config.TextEditor;
			textBox2.Text = Config.EditorArg;
			textBox2.Enabled = itemET.Value == TextEditorType.USER_SETTING;

			#region EM_私家版_LoadText＆SaveText機能拡張
			{
				ConfigItem<List<string>> item = (ConfigItem<List<string>>)ConfigData.Instance.GetConfigItem(ConfigCode.ValidExtension);
				textBox3.Text = item.ValueToString();
				textBox3.Enabled = !item.Fixed;
			}
			#endregion
			#region EE_AnchorのCB機能移植
			setCheckBox(checkBoxCBuseCB, ConfigCode.CBUseClipboard);
			setCheckBox(checkBoxCBIgnoreTags, ConfigCode.CBIgnoreTags);
			textBoxCBReplaceTags.Text = Config.CBReplaceTags;
			setCheckBox(checkBoxCBNewLinesOnly, ConfigCode.CBNewLinesOnly);
			setCheckBox(checkBoxCBClearBuffer, ConfigCode.CBClearBuffer);
			setCheckBox(checkBoxCBTriggerLeftClick, ConfigCode.CBTriggerLeftClick);
			setCheckBox(checkBoxCBTriggerMiddleClick, ConfigCode.CBTriggerMiddleClick);
			setCheckBox(checkBoxCBTriggerDoubleLeftClick, ConfigCode.CBTriggerDoubleLeftClick);
			setCheckBox(checkBoxCBTriggerAnyKeyWait, ConfigCode.CBTriggerAnyKeyWait);
			setCheckBox(checkBoxCBTriggerInputWait, ConfigCode.CBTriggerInputWait);
			setNumericUpDown(numericUpDownCBMaxCB, ConfigCode.CBMaxCB);
			setNumericUpDown(numericUpDownCBBufferSize, ConfigCode.CBBufferSize);
			setNumericUpDown(numericUpDownCBScrollCount, ConfigCode.CBScrollCount);
			setNumericUpDown(numericUpDownCBMinTimer, ConfigCode.CBMinTimer);
			#endregion

			setCheckBox(rikaiCheckBoxEnable, ConfigCode.RikaiEnabled);
			setCheckBox(rikaiCheckBoxSeparateBoxes, ConfigCode.RikaiUseSeparateBoxes);
			setColorBox(rikaiColorBoxBG, ConfigCode.RikaiColorBack);
			setColorBox(rikaiColorBoxText, ConfigCode.RikaiColorText);
			rikaiDictFilenameTextBox.Text = Config.RikaiFilename;


		}

		private void SaveConfig()
		{
			ConfigData config = ConfigData.Instance.Copy();
			config.GetConfigItem(ConfigCode.IgnoreCase).SetValue<bool>(checkBox1.Checked);
			config.GetConfigItem(ConfigCode.UseRenameFile).SetValue<bool>(checkBox2.Checked);
			config.GetConfigItem(ConfigCode.UseMouse).SetValue<bool>(checkBox3.Checked);
			config.GetConfigItem(ConfigCode.UseMenu).SetValue<bool>(checkBox4.Checked);
			config.GetConfigItem(ConfigCode.UseDebugCommand).SetValue<bool>(checkBox5.Checked);
			config.GetConfigItem(ConfigCode.AllowMultipleInstances).SetValue<bool>(checkBox6.Checked);
			config.GetConfigItem(ConfigCode.AutoSave).SetValue<bool>(checkBox7.Checked);
			config.GetConfigItem(ConfigCode.SizableWindow).SetValue<bool>(checkBox8.Checked);
			config.GetConfigItem(ConfigCode.UseReplaceFile).SetValue<bool>(checkBox10.Checked);
			config.GetConfigItem(ConfigCode.IgnoreUncalledFunction).SetValue<bool>(checkBox11.Checked);
			//config.GetConfigItem(ConfigCode.ReduceFormattedStringOnLoad).SetValue<bool>(checkBox12.Checked);
			config.GetConfigItem(ConfigCode.DisplayReport).SetValue<bool>(checkBox13.Checked);
			config.GetConfigItem(ConfigCode.ButtonWrap).SetValue<bool>(checkBox14.Checked);
			config.GetConfigItem(ConfigCode.SearchSubdirectory).SetValue<bool>(checkBox15.Checked);
			config.GetConfigItem(ConfigCode.SortWithFilename).SetValue<bool>(checkBox16.Checked);
			config.GetConfigItem(ConfigCode.SetWindowPos).SetValue<bool>(checkBox17.Checked);
			config.GetConfigItem(ConfigCode.UseKeyMacro).SetValue<bool>(checkBox18.Checked);
			config.GetConfigItem(ConfigCode.AllowFunctionOverloading).SetValue<bool>(checkBox20.Checked);
			config.GetConfigItem(ConfigCode.WarnFunctionOverloading).SetValue<bool>(checkBox19.Checked);
			config.GetConfigItem(ConfigCode.WindowMaximixed).SetValue<bool>(checkBox21.Checked);
			config.GetConfigItem(ConfigCode.WarnNormalFunctionOverloading).SetValue<bool>(checkBox22.Checked);
			config.GetConfigItem(ConfigCode.WarnBackCompatibility).SetValue<bool>(checkBox23.Checked);
			config.GetConfigItem(ConfigCode.CompatiErrorLine).SetValue<bool>(checkBoxCompatiErrorLine.Checked);
			config.GetConfigItem(ConfigCode.CompatiCALLNAME).SetValue<bool>(checkBoxCompatiCALLNAME.Checked);
			config.GetConfigItem(ConfigCode.UseSaveFolder).SetValue<bool>(checkBox24.Checked);
			config.GetConfigItem(ConfigCode.CompatiRAND).SetValue<bool>(checkBoxCompatiRAND.Checked);
			config.GetConfigItem(ConfigCode.CompatiLinefeedAs1739).SetValue<bool>(checkBoxCompatiLinefeedAs1739.Checked);
			config.GetConfigItem(ConfigCode.CompatiCallEvent).SetValue<bool>(checkBox28.Checked);

			config.GetConfigItem(ConfigCode.CompatiFuncArgOptional).SetValue<bool>(checkBox12.Checked);
			config.GetConfigItem(ConfigCode.CompatiFuncArgAutoConvert).SetValue<bool>(checkBox25.Checked);
			config.GetConfigItem(ConfigCode.SystemSaveInBinary).SetValue<bool>(checkBox26.Checked);
			config.GetConfigItem(ConfigCode.SystemIgnoreTripleSymbol).SetValue<bool>(checkBoxSystemTripleSymbol.Checked);

			config.GetConfigItem(ConfigCode.CompatiFunctionNoignoreCase).SetValue<bool>(checkBoxFuncNoIgnoreCase.Checked);
			config.GetConfigItem(ConfigCode.SystemAllowFullSpace).SetValue<bool>(checkBoxSystemFullSpace.Checked);
			config.GetConfigItem(ConfigCode.CompatiSPChara).SetValue<bool>(checkBoxCompatiSP.Checked);
			config.GetConfigItem(ConfigCode.TimesNotRigorousCalculation).SetValue<bool>(checkBox9.Checked);
			config.GetConfigItem(ConfigCode.SystemNoTarget).SetValue<bool>(checkBox29.Checked);
			config.GetConfigItem(ConfigCode.ForbidUpdateCheck).SetValue<bool>(checkBox30.Checked);
			config.GetConfigItem(ConfigCode.UseERD).SetValue<bool>(checkBox31.Checked);
			config.GetConfigItem(ConfigCode.VarsizeDimConfig).SetValue<bool>(checkBox34.Checked);
			config.GetConfigItem(ConfigCode.CheckDuplicateIdentifier).SetValue<bool>(checkBox35.Checked);

			config.GetConfigItem(ConfigCode.WindowX).SetValue<int>((int)numericUpDown2.Value);
			config.GetConfigItem(ConfigCode.WindowY).SetValue<int>((int)numericUpDown3.Value);
			config.GetConfigItem(ConfigCode.MaxLog).SetValue<int>((int)numericUpDown4.Value);
			config.GetConfigItem(ConfigCode.PrintCPerLine).SetValue<int>((int)numericUpDown1.Value);
			config.GetConfigItem(ConfigCode.PrintCLength).SetValue<int>((int)numericUpDown9.Value);
			config.GetConfigItem(ConfigCode.LineHeight).SetValue<int>((int)numericUpDown6.Value);
			config.GetConfigItem(ConfigCode.FPS).SetValue<int>((int)numericUpDown7.Value);
			config.GetConfigItem(ConfigCode.ScrollHeight).SetValue<int>((int)numericUpDown8.Value);
			config.GetConfigItem(ConfigCode.InfiniteLoopAlertTime).SetValue<int>((int)numericUpDown10.Value);
			config.GetConfigItem(ConfigCode.SaveDataNos).SetValue<int>((int)numericUpDown11.Value);

			config.GetConfigItem(ConfigCode.WindowPosX).SetValue<int>((int)numericUpDownPosX.Value);
			config.GetConfigItem(ConfigCode.WindowPosY).SetValue<int>((int)numericUpDownPosY.Value);

			config.GetConfigItem(ConfigCode.FontSize).SetValue<int>((int)numericUpDown5.Value);
			int nameIndex = comboBox2.SelectedIndex;
			if (nameIndex >= 0)
				config.GetConfigItem(ConfigCode.FontName).SetValue<string>((string)comboBox2.SelectedItem);
			else
				config.GetConfigItem(ConfigCode.FontName).SetValue<string>(comboBox2.Text);



			config.GetConfigItem(ConfigCode.ForeColor).SetValue<Color>(colorBoxFG.SelectingColor);
			config.GetConfigItem(ConfigCode.BackColor).SetValue<Color>(colorBoxBG.SelectingColor);
			config.GetConfigItem(ConfigCode.FocusColor).SetValue<Color>(colorBoxSelecting.SelectingColor);
			config.GetConfigItem(ConfigCode.LogColor).SetValue<Color>(colorBoxBacklog.SelectingColor);

			switch (comboBoxTextDrawingMode.SelectedIndex)
			{
				case 0:
					config.GetConfigItem(ConfigCode.TextDrawingMode).SetValue<TextDrawingMode>(TextDrawingMode.WINAPI); break;
				case 1:
					config.GetConfigItem(ConfigCode.TextDrawingMode).SetValue<TextDrawingMode>(TextDrawingMode.TEXTRENDERER); break;
				case 2:
					config.GetConfigItem(ConfigCode.TextDrawingMode).SetValue<TextDrawingMode>(TextDrawingMode.GRAPHICS); break;
			}

			switch (comboBoxReduceArgumentOnLoad.SelectedIndex)
			{
				case 0:
					config.GetConfigItem(ConfigCode.ReduceArgumentOnLoad).SetValue<ReduceArgumentOnLoadFlag>(ReduceArgumentOnLoadFlag.NO); break;
				case 1:
					config.GetConfigItem(ConfigCode.ReduceArgumentOnLoad).SetValue<ReduceArgumentOnLoadFlag>(ReduceArgumentOnLoadFlag.ONCE); break;
				case 2:
					config.GetConfigItem(ConfigCode.ReduceArgumentOnLoad).SetValue<ReduceArgumentOnLoadFlag>(ReduceArgumentOnLoadFlag.YES); break;
			}
			config.GetConfigItem(ConfigCode.DisplayWarningLevel).SetValue<int>(comboBox5.SelectedIndex);


			switch (comboBox3.SelectedIndex)
			{
				case 0:
					config.GetConfigItem(ConfigCode.FunctionNotFoundWarning).SetValue<DisplayWarningFlag>(DisplayWarningFlag.IGNORE); break;
				case 1:
					config.GetConfigItem(ConfigCode.FunctionNotFoundWarning).SetValue<DisplayWarningFlag>(DisplayWarningFlag.LATER); break;
				case 2:
					config.GetConfigItem(ConfigCode.FunctionNotFoundWarning).SetValue<DisplayWarningFlag>(DisplayWarningFlag.ONCE); break;
				case 3:
					config.GetConfigItem(ConfigCode.FunctionNotFoundWarning).SetValue<DisplayWarningFlag>(DisplayWarningFlag.DISPLAY); break;
			}
			switch (comboBox4.SelectedIndex)
			{
				case 0:
					config.GetConfigItem(ConfigCode.FunctionNotCalledWarning).SetValue<DisplayWarningFlag>(DisplayWarningFlag.IGNORE); break;
				case 1:
					config.GetConfigItem(ConfigCode.FunctionNotCalledWarning).SetValue<DisplayWarningFlag>(DisplayWarningFlag.LATER); break;
				case 2:
					config.GetConfigItem(ConfigCode.FunctionNotCalledWarning).SetValue<DisplayWarningFlag>(DisplayWarningFlag.ONCE); break;
				case 3:
					config.GetConfigItem(ConfigCode.FunctionNotCalledWarning).SetValue<DisplayWarningFlag>(DisplayWarningFlag.DISPLAY); break;
			}
			switch (comboBox1.SelectedIndex)
			{
				case 0:
					config.GetConfigItem(ConfigCode.useLanguage).SetValue<UseLanguage>(UseLanguage.JAPANESE); break;
				case 1:
					config.GetConfigItem(ConfigCode.useLanguage).SetValue<UseLanguage>(UseLanguage.KOREAN); break;
				case 2:
					config.GetConfigItem(ConfigCode.useLanguage).SetValue<UseLanguage>(UseLanguage.CHINESE_HANS); break;
				case 3:
					config.GetConfigItem(ConfigCode.useLanguage).SetValue<UseLanguage>(UseLanguage.CHINESE_HANT); break;
			}
			switch (comboBox6.SelectedIndex)
			{
				case 0:
					config.GetConfigItem(ConfigCode.EditorType).SetValue<TextEditorType>(TextEditorType.SAKURA); break;
				case 1:
					config.GetConfigItem(ConfigCode.EditorType).SetValue<TextEditorType>(TextEditorType.TERAPAD); break;
				case 2:
					config.GetConfigItem(ConfigCode.EditorType).SetValue<TextEditorType>(TextEditorType.EMEDITOR); break;
				case 3:
					config.GetConfigItem(ConfigCode.EditorType).SetValue<TextEditorType>(TextEditorType.USER_SETTING); break;
			}

			config.GetConfigItem(ConfigCode.TextEditor).SetValue<string>(textBox1.Text);
			config.GetConfigItem(ConfigCode.EditorArgument).SetValue<string>(textBox2.Text);

			#region EM_私家版_LoadText＆SaveText機能拡張
			config.GetConfigItem(ConfigCode.ValidExtension).TryParse(textBox3.Text);
			#endregion
			#region EM_私家版_セーブ圧縮
			config.GetConfigItem(ConfigCode.ZipSaveData).SetValue<bool>(checkBox32.Checked);
			#endregion
			#region EM_私家版_多言語化改造
			if (comboBox7.SelectedIndex == 0)
			{
				config.GetConfigItem(ConfigCode.EmueraLang).SetValue<string>(string.Empty);
				ConfigData.Instance.GetConfigItem(ConfigCode.EmueraLang).SetValue<string>(string.Empty);
			}
			else
			{
				config.GetConfigItem(ConfigCode.EmueraLang).SetValue<string>(comboBox7.SelectedItem as string);
				ConfigData.Instance.GetConfigItem(ConfigCode.EmueraLang).SetValue<string>(comboBox7.SelectedItem as string);
			}
			config.GetConfigItem(ConfigCode.EnglishConfigOutput).SetValue<bool>(checkBox33.Checked);
			Config.UpdateLangSetting(config);
			#endregion
			#region EE_AnchorのCB機能移植
			config.GetConfigItem(ConfigCode.CBUseClipboard).SetValue<bool>(checkBoxCBuseCB.Checked);
			config.GetConfigItem(ConfigCode.CBIgnoreTags).SetValue<bool>(checkBoxCBIgnoreTags.Checked);
			config.GetConfigItem(ConfigCode.CBReplaceTags).SetValue<string>(textBoxCBReplaceTags.Text);
			config.GetConfigItem(ConfigCode.CBNewLinesOnly).SetValue<bool>(checkBoxCBNewLinesOnly.Checked);
			config.GetConfigItem(ConfigCode.CBClearBuffer).SetValue<bool>(checkBoxCBClearBuffer.Checked);
			config.GetConfigItem(ConfigCode.CBTriggerLeftClick).SetValue<bool>(checkBoxCBTriggerLeftClick.Checked);
			config.GetConfigItem(ConfigCode.CBTriggerMiddleClick).SetValue<bool>(checkBoxCBTriggerMiddleClick.Checked);
			config.GetConfigItem(ConfigCode.CBTriggerDoubleLeftClick).SetValue<bool>(checkBoxCBTriggerDoubleLeftClick.Checked);
			config.GetConfigItem(ConfigCode.CBTriggerAnyKeyWait).SetValue<bool>(checkBoxCBTriggerAnyKeyWait.Checked);
			config.GetConfigItem(ConfigCode.CBTriggerInputWait).SetValue<bool>(checkBoxCBTriggerInputWait.Checked);
			config.GetConfigItem(ConfigCode.CBMaxCB).SetValue<int>((int)numericUpDownCBMaxCB.Value);
			config.GetConfigItem(ConfigCode.CBBufferSize).SetValue<int>((int)numericUpDownCBBufferSize.Value);
			config.GetConfigItem(ConfigCode.CBScrollCount).SetValue<int>((int)numericUpDownCBScrollCount.Value);
			config.GetConfigItem(ConfigCode.CBMinTimer).SetValue<int>((int)numericUpDownCBMinTimer.Value);
			#endregion

			config.GetConfigItem(ConfigCode.RikaiEnabled).SetValue<bool>(rikaiCheckBoxEnable.Checked);
			config.GetConfigItem(ConfigCode.RikaiFilename).SetValue<string>(rikaiDictFilenameTextBox.Text);
			config.GetConfigItem(ConfigCode.RikaiColorBack).SetValue<Color>(rikaiColorBoxBG.SelectingColor);
			config.GetConfigItem(ConfigCode.RikaiColorText).SetValue<Color>(rikaiColorBoxText.SelectingColor);
			config.GetConfigItem(ConfigCode.RikaiUseSeparateBoxes).SetValue<bool>(rikaiCheckBoxSeparateBoxes.Checked);


			config.SaveConfig();
		}


		private void comboBoxReduceArgumentOnLoad_SelectedIndexChanged(object sender, EventArgs e)
		{
			//いちいち切り替えるのが面倒なのでまとめて却下
			/*if (comboBoxReduceArgumentOnLoad.SelectedIndex == 0)
			{
				comboBox3.Enabled = false;
				comboBox4.Enabled = false;
				comboBox5.Enabled = false;
				checkBox12.Enabled = false;
				checkBox11.Enabled = false;
			}
			else
			{
				comboBox3.Enabled = true;
				comboBox4.Enabled = true;
				comboBox5.Enabled = true;
				checkBox12.Enabled = true;
				checkBox11.Enabled = true;
			}*/


		}


		private void button1_Click(object sender, EventArgs e)
		{
			if (parent == null)
				return;
			if (numericUpDown2.Enabled)
				numericUpDown2.Value = parent.MainPicBox.Width;
			if (numericUpDown3.Enabled)
				numericUpDown3.Value = parent.MainPicBox.Height + (int)Config.LineHeight;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (parent == null)
				return;
			if (numericUpDownPosX.Enabled)
			{
				if (numericUpDownPosX.Maximum < parent.Location.X)
					numericUpDownPosX.Maximum = parent.Location.X;
				if (numericUpDownPosX.Minimum > parent.Location.X)
					numericUpDownPosX.Minimum = parent.Location.X;
				numericUpDownPosX.Value = parent.Location.X;
			}
			if (numericUpDownPosY.Enabled)
			{
				if (numericUpDownPosY.Maximum < parent.Location.Y)
					numericUpDownPosY.Maximum = parent.Location.Y;
				if (numericUpDownPosY.Minimum > parent.Location.Y)
					numericUpDownPosY.Minimum = parent.Location.Y;
				numericUpDownPosY.Value = parent.Location.Y;
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (!comboBox2.Enabled)
				return;
			if (!OperatingSystem.IsWindows())
				return;
			InstalledFontCollection ifc = new InstalledFontCollection();
			foreach (FontFamily ff in ifc.Families)
			{
				/**
				if (!ff.IsStyleAvailable(FontStyle.Regular))
					continue;
				if (!ff.IsStyleAvailable(FontStyle.Bold))
					continue;
				if (!ff.IsStyleAvailable(FontStyle.Italic))
					continue;
				if (!ff.IsStyleAvailable(FontStyle.Strikeout))
					continue;
				if (!ff.IsStyleAvailable(FontStyle.Underline))
					continue;
				**/
				comboBox2.Items.Add(ff.Name);
			}
			#region EE_フォントファイル対応
			PrivateFontCollection pfc = new PrivateFontCollection();
			foreach (string fontFile in Directory.GetFiles(Program.FontDir, "*.ttf", SearchOption.AllDirectories))
				pfc.AddFontFile(fontFile);

			foreach (string fontFile in Directory.GetFiles(Program.FontDir, "*.otf", SearchOption.AllDirectories))
				pfc.AddFontFile(fontFile);

			foreach (FontFamily ff in pfc.Families)
			{
				/**
				if (!ff.IsStyleAvailable(FontStyle.Regular))
					continue;
				if (!ff.IsStyleAvailable(FontStyle.Bold))
					continue;
				if (!ff.IsStyleAvailable(FontStyle.Italic))
					continue;
				if (!ff.IsStyleAvailable(FontStyle.Strikeout))
					continue;
				if (!ff.IsStyleAvailable(FontStyle.Underline))
					continue;
				**/
				comboBox2.Items.Add(ff.Name);
			}
			#endregion

			string fontname = comboBox2.Text;
			if (!string.IsNullOrEmpty(fontname))
			{
				int nameIndex = comboBox2.Items.IndexOf(fontname);
				if (nameIndex >= 0)
					comboBox2.SelectedIndex = nameIndex;
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			openFileDialog1.InitialDirectory = @"c:\Program Files";
			openFileDialog1.FileName = "";
			DialogResult res = openFileDialog1.ShowDialog();
			if (res == DialogResult.OK)
			{
				textBox1.Text = openFileDialog1.FileName;
			}
		}


		int setCheckBoxChecked(CheckBox checkbox, bool flag)
		{
			if (checkbox.Checked == flag)
				return 0;//変更不要
			if (!checkbox.Enabled)
				return -1;//変更したいけど許可されなかった
			checkbox.Checked = flag;
			return 1;//変更した
		}

		int setComboBoxChanged(ComboBox combobox, int value)
		{
			if (combobox.SelectedIndex == value)
				return 0;//変更不要
			if (!combobox.Enabled)
				return -1;//変更したいけど許可されなかった
			combobox.SelectedIndex = value;
			return 1;//変更した
		}

		private void button7_Click(object sender, EventArgs e)
		{//eramaker仕様
			bool disenabled = false;
			disenabled |= setCheckBoxChecked(checkBoxCompatiErrorLine, true) < 0;
			disenabled |= setCheckBoxChecked(checkBoxCompatiCALLNAME, true) < 0;
			disenabled |= setCheckBoxChecked(checkBoxCompatiRAND, true) < 0;
			disenabled |= setCheckBoxChecked(checkBoxFuncNoIgnoreCase, true) < 0;
			disenabled |= setCheckBoxChecked(checkBox28, true) < 0;
			disenabled |= setCheckBoxChecked(checkBoxCompatiSP, true) < 0;
			disenabled |= setCheckBoxChecked(checkBoxCompatiLinefeedAs1739, false) < 0;
			disenabled |= setCheckBoxChecked(checkBox12, false) < 0;
			disenabled |= setCheckBoxChecked(checkBox25, false) < 0;
			disenabled |= setCheckBoxChecked(checkBox9, true) < 0;
			if (disenabled)
				MessageBox.Show(trmb.NotAllowChangeSetting.Text, trmb.UnableChangeSetting.Text);
		}

		private void button8_Click(object sender, EventArgs e)
		{//最新Emuera仕様 - 全部false
			bool disenabled = false;
			disenabled |= setCheckBoxChecked(checkBoxCompatiErrorLine, false) < 0;
			disenabled |= setCheckBoxChecked(checkBoxCompatiCALLNAME, false) < 0;
			disenabled |= setCheckBoxChecked(checkBoxCompatiRAND, false) < 0;
			disenabled |= setCheckBoxChecked(checkBoxFuncNoIgnoreCase, false) < 0;
			disenabled |= setCheckBoxChecked(checkBox28, false) < 0;
			disenabled |= setCheckBoxChecked(checkBoxCompatiLinefeedAs1739, false) < 0;
			disenabled |= setCheckBoxChecked(checkBox12, false) < 0;
			disenabled |= setCheckBoxChecked(checkBox25, false) < 0;
			disenabled |= setCheckBoxChecked(checkBoxCompatiSP, false) < 0;
			disenabled |= setCheckBoxChecked(checkBox9, false) < 0;
			if (disenabled)
				MessageBox.Show(trmb.NotAllowChangeSetting.Text, trmb.UnableChangeSetting.Text);
		}

		//互換性チェックはいじらないように変更
		private void button5_Click(object sender, EventArgs e)
		{//解析のユーザー向け設定（デフォルト設定と同じ）
			bool disenabled = false;
			//disenabled |= setCheckBoxChecked(checkBox23, true) < 0;
			disenabled |= setCheckBoxChecked(checkBox13, false) < 0;
			disenabled |= setComboBoxChanged(comboBoxReduceArgumentOnLoad, 0) < 0;
			disenabled |= setComboBoxChanged(comboBox5, 1) < 0;
			disenabled |= setCheckBoxChecked(checkBox11, true) < 0;
			disenabled |= setComboBoxChanged(comboBox3, 0) < 0;
			disenabled |= setComboBoxChanged(comboBox4, 0) < 0;
			if (disenabled)
				MessageBox.Show(trmb.NotAllowChangeSetting.Text, trmb.UnableChangeSetting.Text);
		}

		private void button6_Click(object sender, EventArgs e)
		{//解析の開発者向け設定（関数名以外はしっかりチェックする）
			bool disenabled = false;
			//disenabled |= setCheckBoxChecked(checkBox23, true) < 0;
			disenabled |= setCheckBoxChecked(checkBox13, true) < 0;
			disenabled |= setComboBoxChanged(comboBoxReduceArgumentOnLoad, 2) < 0;
			disenabled |= setComboBoxChanged(comboBox5, 0) < 0;
			disenabled |= setCheckBoxChecked(checkBox11, true) < 0;
			disenabled |= setComboBoxChanged(comboBox3, 0) < 0;
			disenabled |= setComboBoxChanged(comboBox4, 0) < 0;
			if (disenabled)
				MessageBox.Show(trmb.NotAllowChangeSetting.Text, trmb.UnableChangeSetting.Text);
		}

		private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBox2.Enabled = ((ComboBox)sender).SelectedIndex == 3;
		}

		private void rikaiFlowLayoutPanel_Paint(object sender, PaintEventArgs e)
		{

		}

		private void rikaiDictFilenameLabel_Click(object sender, EventArgs e)
		{

		}

		private void rikaiNote2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var url = "https://wiki.eragames.rip/index.php/Emuera-Rikaichan";
			try
			{
				System.Diagnostics.Process.Start(url);
			}
			catch
			{
				// https://stackoverflow.com/questions/4580263/how-to-open-in-default-browser-in-c-sharp
				// linux makes life easier once again...
				// hack because of this: https://github.com/dotnet/corefx/issues/10361
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					url = url.Replace("&", "^&");
					Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					Process.Start("xdg-open", url);
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				{
					Process.Start("open", url);
				}
				else
				{
					throw;
				}
			}
		}
	}
}
