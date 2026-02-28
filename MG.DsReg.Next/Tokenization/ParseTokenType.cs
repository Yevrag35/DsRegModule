namespace MG.DsReg.Next.Tokenization;

public enum ParseTokenType
{
	None = 0,

	Separator,

	BlockName,

	BlockStart,

	BlockEnd,

	PropertyName,

	Value,

	StartArray,

	EndArray,

	Comment,

	End,
}
