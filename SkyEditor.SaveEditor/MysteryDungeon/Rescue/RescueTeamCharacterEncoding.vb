Namespace MysteryDungeon.Rescue
    Public Class RescueTeamCharacterEncoding
        Inherits Text.Encoding

#Region "8-bit Characters"

        Private Shared ReadOnly EightBitCharacters As Char() = {vbNullChar, '0x00
                                                                "", '0x01
                                                                "", '0x02
                                                                "", '0x03
                                                                "", '0x04
                                                                "", '0x05
                                                                "", '0x06
                                                                "", '0x07
                                                                "", '0x08
                                                                "", '0x09
                                                                vbLf, '0x0A '13 pixel new line
                                                                "", '0x0B
                                                                "", '0x0C
                                                                vbCr, '0x0D '13 pixel new line
                                                                "", '0x0E
                                                                "", '0x0F
                                                                "", '0x10
                                                                "", '0x11
                                                                "", '0x12
                                                                "", '0x13
                                                                "", '0x14
                                                                "", '0x15
                                                                "", '0x16
                                                                "", '0x17
                                                                "", '0x18
                                                                "", '0x19
                                                                "", '0x1A
                                                                "", '0x1B
                                                                "", '0x1C
                                                                "", '0x1D '5 pixel new line
                                                                "", '0x1E
                                                                "", '0x1F
                                                                " ", '0x20
                                                                "!", '0x21
                                                                """", '0x22
                                                                "#", '0x23
                                                                "$", '0x24
                                                                "%", '0x25
                                                                "&", '0x26
                                                                "'", '0x27
                                                                "(", '0x28
                                                                ")", '0x29
                                                                "*", '0x2A
                                                                "+", '0x2B
                                                                ",", '0x2C
                                                                "-", '0x2D
                                                                ".", '0x2E
                                                                "/", '0x2F
                                                                "0", '0x30
                                                                "1", '0x31
                                                                "2", '0x32
                                                                "3", '0x33
                                                                "4", '0x34
                                                                "5", '0x35
                                                                "6", '0x36
                                                                "7", '0x37
                                                                "8", '0x38
                                                                "9", '0x39
                                                                ":", '0x3A
                                                                ";", '0x3B
                                                                "<", '0x3C
                                                                "=", '0x3D
                                                                ">", '0x3E
                                                                "?", '0x3F
                                                                "@", '0x40
                                                                "A", '0x41
                                                                "B", '0x42
                                                                "C", '0x43
                                                                "D", '0x44
                                                                "E", '0x45
                                                                "F", '0x46
                                                                "G", '0x47
                                                                "H", '0x48
                                                                "I", '0x49
                                                                "J", '0x4A
                                                                "K", '0x4B
                                                                "L", '0x4C
                                                                "M", '0x4D
                                                                "N", '0x4E
                                                                "O", '0x4F
                                                                "P", '0x50
                                                                "Q", '0x51
                                                                "R", '0x52
                                                                "S", '0x53
                                                                "T", '0x54
                                                                "U", '0x55
                                                                "V", '0x56
                                                                "W", '0x57
                                                                "X", '0x58
                                                                "Y", '0x59
                                                                "Z", '0x5A
                                                                "[", '0x5B
                                                                "\", '0x5C
                                                                "]", '0x5D
                                                                "^", '0x5E
                                                                "_", '0x5F
                                                                "`", '0x60
                                                                "a", '0x61
                                                                "b", '0x62
                                                                "c", '0x63
                                                                "d", '0x64
                                                                "e", '0x65
                                                                "f", '0x66
                                                                "g", '0x67
                                                                "h", '0x68
                                                                "i", '0x69
                                                                "j", '0x6A
                                                                "k", '0x6B
                                                                "l", '0x6C
                                                                "m", '0x6D
                                                                "n", '0x6E
                                                                "o", '0x6F
                                                                "p", '0x70
                                                                "q", '0x71
                                                                "r", '0x72
                                                                "s", '0x73
                                                                "t", '0x74
                                                                "u", '0x75
                                                                "v", '0x76
                                                                "w", '0x77
                                                                "x", '0x78
                                                                "y", '0x79
                                                                "z", '0x7A
                                                                "{", '0x7B
                                                                "|", '0x7C
                                                                "}", '0x7D
                                                                "\7E", '0x7E
                                                                " ", '0x7F
                                                                "€", '0x80
                                                                "", '0x81 'Escape
                                                                "", '0x82 'Escape
                                                                "", '0x83 'Escape
                                                                "", '0x84 'Escape
                                                                "⋯", '0x85
                                                                "†", '0x86
                                                                "", '0x87 'Escape
                                                                "ˆ", '0x88
                                                                "‰", '0x89
                                                                "Š", '0x8A
                                                                "‹", '0x8B
                                                                "Œ", '0x8C
                                                                "e", '0x8D
                                                                "Ž", '0x8E
                                                                "•", '0x8F
                                                                "•", '0x90
                                                                "‘", '0x91
                                                                "’", '0x92
                                                                "““", '0x93
                                                                "””", '0x94
                                                                "•", '0x95
                                                                "ᵉʳ", '0x96
                                                                "ʳᵉ", '0x97
                                                                "˜", '0x98
                                                                "™", '0x99
                                                                "š", '0x9A
                                                                "›", '0x9B
                                                                "œ", '0x9C
                                                                "•", '0x9D
                                                                "ž", '0x9E
                                                                "Ÿ", '0x9F
                                                                " ", '0xA0
                                                                "¡", '0xA1
                                                                "¢", '0xA2
                                                                "£", '0xA3
                                                                "¤", '0xA4
                                                                "¥", '0xA5
                                                                "¦", '0xA6
                                                                "§", '0xA7
                                                                "¨", '0xA8
                                                                "©", '0xA9
                                                                "ª", '0xAA
                                                                "«", '0xAB
                                                                "¬", '0xAC
                                                                "—", '0xAD
                                                                "®", '0xAE
                                                                "¯", '0xAF
                                                                "°", '0xB0
                                                                "±", '0xB1
                                                                "²", '0xB2
                                                                "³", '0xB3
                                                                "´", '0xB4
                                                                "µ", '0xB5
                                                                "¶", '0xB6
                                                                "„", '0xB7
                                                                "‚", '0xB8
                                                                "¹", '0xB9
                                                                "⁰", '0xBA
                                                                "»", '0xBB
                                                                "←", '0xBC
                                                                "♂", '0xBD
                                                                "♀", '0xBE
                                                                "¿", '0xBF
                                                                "À", '0xC0
                                                                "Á", '0xC1
                                                                "Â", '0xC2
                                                                "Ã", '0xC3
                                                                "Ä", '0xC4
                                                                "Å", '0xC5
                                                                "Æ", '0xC6
                                                                "Ç", '0xC7
                                                                "È", '0xC8
                                                                "É", '0xC9
                                                                "Ê", '0xCA
                                                                "Ë", '0xCB
                                                                "Ì", '0xCC
                                                                "Í", '0xCD
                                                                "Î", '0xCE
                                                                "Ï", '0xCF
                                                                "Ð", '0xD0
                                                                "Ñ", '0xD1
                                                                "Ò", '0xD2
                                                                "Ó", '0xD3
                                                                "Ô", '0xD4
                                                                "Õ", '0xD5
                                                                "Ö", '0xD6
                                                                "×", '0xD7
                                                                "Ø", '0xD8
                                                                "Ù", '0xD9
                                                                "Ú", '0xDA
                                                                "Û", '0xDB
                                                                "Ü", '0xDC
                                                                "Ý", '0xDD
                                                                "Þ", '0xDE
                                                                "ß", '0xDF
                                                                "à", '0xE0
                                                                "á", '0xE1
                                                                "â", '0xE2
                                                                "ã", '0xE3
                                                                "ä", '0xE4
                                                                "å", '0xE5
                                                                "æ", '0xE6
                                                                "ç", '0xE7
                                                                "è", '0xE8
                                                                "é", '0xE9
                                                                "ê", '0xEA
                                                                "ë", '0xEB
                                                                "ì", '0xEC
                                                                "í", '0xED
                                                                "î", '0xEE
                                                                "ï", '0xEF
                                                                "ð", '0xF0
                                                                "ñ", '0xF1
                                                                "ò", '0xF2
                                                                "ó", '0xF3
                                                                "ô", '0xF4
                                                                "õ", '0xF5
                                                                "ö", '0xF6
                                                                "÷", '0xF7
                                                                "ø", '0xF8
                                                                "ù", '0xF9
                                                                "ú", '0xFA
                                                                "û", '0xFB
                                                                "ü", '0xFC
                                                                "ý", '0xFD
                                                                "þ", '0xFE
                                                                "ÿ"} '0xFF

#End Region

        ''' <summary>
        ''' Calculates the number of bytes produced by encoding the characters in the specified string.
        ''' </summary>
        ''' <param name="chars"></param>
        ''' <param name="index"></param>
        ''' <param name="count"></param>
        ''' <returns></returns>
        Public Overrides Function GetByteCount(chars() As Char, index As Integer, count As Integer) As Integer
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Encodes a set of characters from the specified character array into the specified byte array.
        ''' </summary>
        ''' <param name="chars">The character array containing the set of characters to encode.</param>
        ''' <param name="charIndex">The index of the first character to encode.</param>
        ''' <param name="charCount">The number of characters to encode.</param>
        ''' <param name="bytes">The byte array to contain the resulting sequence of bytes.</param>
        ''' <param name="byteIndex">The index at which to start writing the resulting sequence of bytes.</param>
        ''' <returns>The actual number of bytes written into <paramref name="bytes"/>.</returns>
        Public Overrides Function GetBytes(chars() As Char, charIndex As Integer, charCount As Integer, bytes() As Byte, byteIndex As Integer) As Integer
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Calculates the number of characters produced by decoding a sequence of bytes from the specified byte array.
        ''' </summary>
        ''' <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
        ''' <param name="index">The index of the first byte to decode.</param>
        ''' <param name="count">The number of bytes to decode.</param>
        ''' <returns></returns>
        Public Overrides Function GetCharCount(bytes() As Byte, index As Integer, count As Integer) As Integer
            Dim charCount As Integer
            For i = index To count + index - 1
                Select Case bytes(i)
                    Case 0
                        'Null char
                        Exit For
                    Case &H81, &H82, &H83, &H84, &H87
                        'Don't increment.  These are two byte escape sequences, and as a logic shortcut, the next character will be the one counted.
                    Case Else
                        charCount += 1
                End Select
            Next
            Return charCount
        End Function

        ''' <summary>
        ''' Decodes a sequence of bytes from the specified byte array into the specified character array.
        ''' </summary>
        ''' <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
        ''' <param name="byteIndex">The index of the first byte to decode.</param>
        ''' <param name="byteCount">The number of bytes to decode.</param>
        ''' <param name="chars">The character array to contain the resulting set of characters.</param>
        ''' <param name="charIndex">The index at which to start writing the resulting set of characters.</param>
        ''' <returns>The actual number of characters written into chars.</returns>
        Public Overrides Function GetChars(bytes() As Byte, byteIndex As Integer, byteCount As Integer, chars() As Char, charIndex As Integer) As Integer
            Dim charCount As Integer
            For i = 0 To byteCount - 1
                Dim current = bytes(i)
                Select Case current
                    ''Todo: handle this, escape escape sequences, etc.
                    Case 0
                        'Null char
                        'Exit For
                        Throw New NotImplementedException
                    Case &H81, &H82, &H83, &H84, &H87
                        'Don't increment.  These are two byte escape sequences, and as a logic shortcut, the next character will be the one counted.
                        'i += 1
                        Throw New NotImplementedException
                    Case Else
                        chars(charIndex + i) = EightBitCharacters(current)
                        charCount += 1
                End Select
            Next
            Return charCount
        End Function

        ''' <summary>
        ''' Calculates the maximum number of bytes produced by encoding the specified number of characters.
        ''' </summary>
        ''' <param name="charCount">The number of characters to encode.</param>
        ''' <returns>The maximum number of bytes produced by encoding the specified number of characters.</returns>
        Public Overrides Function GetMaxByteCount(charCount As Integer) As Integer
            Return charCount
        End Function

        ''' <summary>
        ''' Calculates the maximum number of characters produced by decoding the specified number of bytes.
        ''' </summary>
        ''' <param name="byteCount">The number of bytes to decode.</param>
        ''' <returns>The maximum number of characters produced by decoding the specified number of bytes.</returns>
        Public Overrides Function GetMaxCharCount(byteCount As Integer) As Integer
            Return byteCount
        End Function
    End Class
End Namespace

