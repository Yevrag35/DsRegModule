namespace MG.DsReg.Next.Tokenization;

public ref struct ParseReader
{
	private const string BLOCK_SEPARATOR = "+--";
	private static readonly string s_lineEnd = Environment.NewLine;

	private int _bufferSize;
	private ParseToken _current;

	private ReadOnlySpan<char> _input;

	private bool _hasNext;

	private int _position;

	/// <summary>
	/// Gets the size of the buffer used for parsing.
	/// </summary>
	public readonly int BufferSize => _bufferSize;

	/// <summary>
	/// Gets the current <see cref="ArgToken"/> being processed.
	/// </summary>
	public readonly ParseToken Current => _current;

	/// <summary>
	/// Indicates whether there is any input (beyond whitespace) left to be parsed.
	/// </summary>
	public readonly bool HasNext => _hasNext;
	public readonly ReadOnlySpan<char> UnparsedInput => _input;

	internal ParseReader(ReadOnlySpan<char> input)
	{
		_input = input;
		_position = 0;
		_current = default;
		_hasNext = false;
	}

	public bool ReadNext()
	{
		ref ReadOnlySpan<char> input = ref _input;
		if (input.IsEmpty)
		{
			_current = default;
			_hasNext = false;
			return false;
		}

		input = SkipDelimiters(input);
		if (IsBlockSeparator(input))
		{
			if (_current.TokenType == ParseTokenType.BlockName)
			{
				// End of the current block
				_current = new(ParseTokenType.BlockEnd, _current.BlockName, default, default);
			}
			else
			{
				// Start of a new block
				_current = new(ParseTokenType.BlockStart, default, default, default);
			}

			return true;
		}


	}

	/// <summary>
	/// Removes leading delimiters (e.g., whitespace) from the given span.
	/// </summary>
	private static ReadOnlySpan<char> SkipDelimiters(ReadOnlySpan<char> span)
	{
		int pos = 0;
		while (pos < span.Length && char.IsWhiteSpace(span[pos]))
		{
			pos++;
		}

		return pos == 0 ? span : span.Slice(pos);
	}

	private static bool IsBlockSeparator(ReadOnlySpan<char> chars)
	{
		return chars.Length >= 3 && chars.Slice(0, 3).Equals(BLOCK_SEPARATOR, StringComparison.Ordinal);
	}
}
