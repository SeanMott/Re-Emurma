#include <ERBParser/Lexer.hpp>


//lexes ERB tokens
std::vector<REEmuraRuntime::Scripting::ERB::Token> REEmuraRuntime::Scripting::ERB::LexTokens(const std::string code)
{
	std::vector<REEmuraRuntime::Scripting::ERB::Token> tokens;

	const size_t codeLength = code.size();

	bool skip = false; //if we have found a ;, we skip the rest till the end of the line

	size_t lineCount = 1;

	std::string line = "";
	for (size_t c = 0; c < codeLength; ++c)
	{
		//if new line
		if (code[c] == '\n')
		{
			//ends the line and parses code
			fmt::print("Line: {}, StartIndex: {} || {}", lineCount, c - line.size(), line);

			if (skip)
				skip = false;

			lineCount++;
			continue;
		}

		//if comment ; starts skipping till a new line
		if (code[c] == ';')
		{
			skip = true;
			continue;
		}

		//skips
		if (skip)
			continue;

		//otherwise add the data
		line += code[c];
	}

	return tokens;
}