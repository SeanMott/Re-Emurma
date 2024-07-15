#pragma once

//parses ERB files into tokens

#include <string>
#include <vector>

#define FMT_HEADER_ONLY
#include <fmt/color.h>
#include <fmt/core.h>

namespace REEmuraRuntime::Scripting::ERB
{
	//defines a ERB Token type


	//defines a ERB token
	struct Token
	{
		size_t lineCount = 0, charIndex = 0;
		std::string data = "";
	};

	//lexes ERB tokens
	std::vector<Token> LexTokens(const std::string code);
}