using System.Runtime.InteropServices;

namespace MG.DsReg.Next.Tokenization;

[StructLayout(LayoutKind.Auto)]
public readonly ref struct ParseToken
{
	public readonly ReadOnlySpan<char> BlockName;

	public readonly ReadOnlySpan<char> PropertyName;

	public readonly ParseTokenType TokenType;

	public readonly ReadOnlySpan<char> Value;

	internal ParseToken(ParseTokenType tokenType, ReadOnlySpan<char> blockName, ReadOnlySpan<char> propertyName, ReadOnlySpan<char> value)
	{
		BlockName = blockName;
		PropertyName = propertyName;
		TokenType = tokenType;
		Value = value;
	}
}
