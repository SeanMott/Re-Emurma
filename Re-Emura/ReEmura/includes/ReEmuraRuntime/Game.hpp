#pragma once

//defines a game and all it's data

#include <string>
#include <filesystem>

namespace REEmuraRuntime::Core
{
	//defines a game
	struct Game
	{
		bool isValid = false; //is the game valid

		std::string ExeName = ""; //exe game name

		std::filesystem::path WorkingDir, //the working directory for the project

			DebugDir, //debug directory

			CsvDir, //the directory with the CSV data
			ErbDir, //erb directory
			DatDir, //dat directory

			ContentDir, //content directory

			MusicDir, //music directory
			FontDir, //font directory
			LangDir; //language directory
	};
}