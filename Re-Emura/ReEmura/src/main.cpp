/*
Entry point for the Re: Emura Runtime

This runtime loads the Re: Emura C# library and renders data to the screen

*/

#include <ReEmuraRuntime/Game.hpp>

#include <ERBParser/Lexer.hpp>

#define FMT_HEADER_ONLY
#include <fmt/color.h>
#include <fmt/core.h>

//gets all the data needed for a Game
static inline REEmuraRuntime::Core::Game ValidateGame(const std::string& gameDir)
{
	REEmuraRuntime::Core::Game game;

	//gets directories
	game.CsvDir = gameDir + "/csv";
	if (!std::filesystem::exists(game.CsvDir))
	{
		fmt::print(fmt::emphasis::bold | fg(fmt::color::red), "Game Directory Lacking: {}\n", "CSV");
		return game;
	}

	game.ErbDir = gameDir + "/erb";
	if (!std::filesystem::exists(game.ErbDir))
	{
		fmt::print(fmt::emphasis::bold | fg(fmt::color::red), "Game Directory Lacking: {}\n", "ERB");
		return game;
	}

	game.DatDir = gameDir + "/dat";
	if (!std::filesystem::exists(game.DatDir))
	{
		fmt::print(fmt::emphasis::bold | fg(fmt::color::red), "Game Directory Lacking: {}\n", "DAT");
		return game;
	}

	game.ContentDir = gameDir + "/resources";
	if (!std::filesystem::exists(game.ContentDir))
	{
		fmt::print(fmt::emphasis::bold | fg(fmt::color::red), "Game Directory Lacking: {}\n", "Resources");
		return game;
	}

	game.FontDir = gameDir + "/font";
	if (!std::filesystem::exists(game.FontDir))
	{
		fmt::print(fmt::emphasis::bold | fg(fmt::color::red), "Game Directory Lacking: {}\n", "Font");
		return game;
	}

	game.MusicDir = gameDir + "/sound";
	if (!std::filesystem::exists(game.MusicDir))
	{
		fmt::print(fmt::emphasis::bold | fg(fmt::color::red), "Game Directory Lacking: {}\n", "Sound");
		return game;
	}

	game.LangDir = gameDir + "/lang";
	if (!std::filesystem::exists(game.LangDir))
	{
		fmt::print(fmt::emphasis::bold | fg(fmt::color::red), "Game Directory Lacking: {}\n", "Lang");
		return game;
	}

	game.DebugDir = gameDir + "/debug";
	if (!std::filesystem::exists(game.DebugDir))
	{
		fmt::print(fmt::emphasis::bold | fg(fmt::color::red), "Game Directory Lacking: {}\n", "Debug");
		return game;
	}

	game.ExeName = "GameName"; //Path.GetFileNameWithoutExtension(Sys.ExeName);

	//load assets

	game.isValid = true;
	return game;
}

//entry point
int main()
{
	//check for flags

	//initalize the Emura C# Library in the runtime

	//initalize rendering engine

	//load games into launcher
	REEmuraRuntime::Core::Game game = ValidateGame("C:/Users/rafal/Downloads/era-updater");

	//loads the scripts

	getchar();
	return 0;
}