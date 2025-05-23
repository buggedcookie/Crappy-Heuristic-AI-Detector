# C.H.A.D — Crappy Heuristic AI Detector

C.H.A.D (Crappy Heuristic AI Detector) is a basic C# tool that tries to guess if a piece of text was written by ChatGPT or another AI. It doesn't use any fancy machine learning — just simple checks based on patterns AI tends to use more than most humans.

## What It Does

- Checks for uncommon or “fancy” characters like curly quotes, em dashes, ellipses, etc.

- Looks for uncommon words not typically used in casual writing

- Flags text that mixes common and uncommon quote styles (something AI often does, but regular users rarely do) 

See `config.json` for the character and word lists it uses.

## Usage

Run the tool from the project folder:

`dotnet run`

Then just paste your text into the console and press enter.

## Commands

--warning  
Shows the warning/disclaimer message.

--exit  
Quits the program.

#### Flags

- **None (0)**  
  No suspicious patterns detected.

- **UncommonSpaces (2)**  
  Detects use of unusual space characters (e.g., non-breaking spaces or thin spaces).

- **UncommonWords (4)**  
  Flags rare or overly formal words not common in casual human writing.

- **UncommonCharacters (8)**  
  Looks for uncommon or typographically fancy characters (e.g., curly quotes, em dashes, ellipses).

- **LotsOfSentences (16)**  
  Indicates a high number of sentences, which may suggest overly structured or generated content or just a person which good grammar.

- **VeryShortSentences (32)**  
  Detects a pattern of unusually short sentences — may signal poor punctuation or oversimplified output.

- **SuspiciousQuotesCombination (64)**  
  Flags use of both straight (' or ") and fancy (“ ” or ‘ ’) quotes together — something AI often does, but humans rarely mix.
  
  > **<u>Please read the warnings / disclaimers below.</u>** 

## Warnings / Disclaimers

This tool is not accurate. It’s based on basic pattern checks and can easily flag normal human writing, especially if it's formal, well-edited, or just uses smart punctuation.

This can also be easily bypassed. A user can simply tell ChatGPT (or any AI) to avoid specific characters, which will prevent the tool from catching it.

This README was written by AI — you can use it as a sample to test the program.

> Again, the whole point of generated AI text is to replicate human writting which is why this tool is **<u>NOT</u>** accurate.

## License

MIT — use it however you want.

## Author

Built out of mild annoyance at overly clean, obviously-AI-generated text.
