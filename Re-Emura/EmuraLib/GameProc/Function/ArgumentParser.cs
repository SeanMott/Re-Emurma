﻿using MinorShift.Emuera.Sub;

namespace MinorShift.Emuera.GameProc.Function
{
	//1756 LogicalLineParserから分離。処理をArgumentBuilderに分割
	internal static partial class ArgumentParser
	{
		public static bool SetArgumentTo(InstructionLine line)
		{
			if (line == null)
				return false;
			if (line.Argument != null)
				return true;
			if (line.IsError)
				return false;
			if (!Program.DebugMode && line.Function.IsDebug())
			{//非DebugモードでのDebug系命令。何もしないので引数解析も不要
				line.Argument = null;
				return true;
			}

			Argument arg;
			string errmes;
			try
			{
				if (line.Function.ArgBuilder != null)
					arg = line.Function.ArgBuilder.CreateArgument(line, GlobalStatic.EMediator);
				else
					arg = line.Function.Instruction.CreateArgument(line, GlobalStatic.EMediator);
			}
			catch (EmueraException e)
			{
				errmes = e.Message;
				goto error;
			}
			if (arg == null)
			{
				if (!line.IsError)
				{
					errmes = "命令の引数解析中に特定できないエラーが発生";
					goto error;
				}
				return false;
			}
			line.Argument = arg;
			if (arg == null)
				line.IsError = true;
			return true;
		error:
			System.Media.SystemSounds.Hand.Play();

			line.IsError = true;
			line.ErrMes = errmes;
			ParserMediator.Warn(errmes, line, 2, true, false);
			return false;
		}
	}
}
