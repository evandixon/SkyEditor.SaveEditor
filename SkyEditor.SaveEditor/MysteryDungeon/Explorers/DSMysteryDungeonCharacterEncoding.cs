using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon.Explorers
{
    public class DSMysteryDungeonCharacterEncoding : Encoding
    {
        private static string[] EightBitCharacters = {"\0", //0x00
                                                    "?", //0x01
                                                    "?", //0x02
                                                    "?", //0x03
                                                    "?", //0x04
                                                    "?", //0x05
                                                    "?", //0x06
                                                    "?", //0x07
                                                    "?", //0x08
                                                    "?", //0x09
                                                    "\n", //0x0A '13 pixel new line
                                                    "?", //0x0B
                                                    "?", //0x0C
                                                    "\r", //0x0D '13 pixel new line
                                                    "?", //0x0E
                                                    "?", //0x0F
                                                    "?", //0x10
                                                    "?", //0x11
                                                    "?", //0x12
                                                    "?", //0x13
                                                    "?", //0x14
                                                    "?", //0x15
                                                    "?", //0x16
                                                    "?", //0x17
                                                    "?", //0x18
                                                    "?", //0x19
                                                    "?", //0x1A
                                                    "?", //0x1B
                                                    "?", //0x1C
                                                    "?", //0x1D '5 pixel new line
                                                    "?", //0x1E
                                                    "?", //0x1F
                                                    " ", //0x20
                                                    "!", //0x21
                                                    "\"", //0x22
                                                    "#", //0x23
                                                    "$", //0x24
                                                    "%", //0x25
                                                    "&", //0x26
                                                    "\'", //0x27
                                                    "(", //0x28
                                                    ")", //0x29
                                                    "*", //0x2A
                                                    "+", //0x2B
                                                    ",", //0x2C
                                                    "-", //0x2D
                                                    ".", //0x2E
                                                    "/", //0x2F
                                                    "0", //0x30
                                                    "1", //0x31
                                                    "2", //0x32
                                                    "3", //0x33
                                                    "4", //0x34
                                                    "5", //0x35
                                                    "6", //0x36
                                                    "7", //0x37
                                                    "8", //0x38
                                                    "9", //0x39
                                                    ":", //0x3A
                                                    ";", //0x3B
                                                    "<", //0x3C
                                                    "=", //0x3D
                                                    ">", //0x3E
                                                    "?", //0x3F
                                                    "@", //0x40
                                                    "A", //0x41
                                                    "B", //0x42
                                                    "C", //0x43
                                                    "D", //0x44
                                                    "E", //0x45
                                                    "F", //0x46
                                                    "G", //0x47
                                                    "H", //0x48
                                                    "I", //0x49
                                                    "J", //0x4A
                                                    "K", //0x4B
                                                    "L", //0x4C
                                                    "M", //0x4D
                                                    "N", //0x4E
                                                    "O", //0x4F
                                                    "P", //0x50
                                                    "Q", //0x51
                                                    "R", //0x52
                                                    "S", //0x53
                                                    "T", //0x54
                                                    "U", //0x55
                                                    "V", //0x56
                                                    "W", //0x57
                                                    "X", //0x58
                                                    "Y", //0x59
                                                    "Z", //0x5A
                                                    "[", //0x5B
                                                    "\\", //0x5C
                                                    "]", //0x5D
                                                    "^", //0x5E
                                                    "_", //0x5F
                                                    "`", //0x60
                                                    "a", //0x61
                                                    "b", //0x62
                                                    "c", //0x63
                                                    "d", //0x64
                                                    "e", //0x65
                                                    "f", //0x66
                                                    "g", //0x67
                                                    "h", //0x68
                                                    "i", //0x69
                                                    "j", //0x6A
                                                    "k", //0x6B
                                                    "l", //0x6C
                                                    "m", //0x6D
                                                    "n", //0x6E
                                                    "o", //0x6F
                                                    "p", //0x70
                                                    "q", //0x71
                                                    "r", //0x72
                                                    "s", //0x73
                                                    "t", //0x74
                                                    "u", //0x75
                                                    "v", //0x76
                                                    "w", //0x77
                                                    "x", //0x78
                                                    "y", //0x79
                                                    "z", //0x7A
                                                    "{", //0x7B
                                                    "|", //0x7C
                                                    "}", //0x7D
                                                    "?", //0x7E
                                                    " ", //0x7F
                                                    "€", //0x80
                                                    "?", //0x81 'Escape
                                                    "?", //0x82 'Escape
                                                    "?", //0x83 'Escape
                                                    "?", //0x84 'Escape
                                                    "⋯", //0x85
                                                    "†", //0x86
                                                    "?", //0x87 'Escape
                                                    "ˆ", //0x88
                                                    "‰", //0x89
                                                    "Š", //0x8A
                                                    "‹", //0x8B
                                                    "Œ", //0x8C
                                                    "ₑ", //0x8D 'Should be super script e
                                                    "Ž", //0x8E
                                                    "•", //0x8F
                                                    "•", //0x90
                                                    "‘", //0x91
                                                    "’", //0x92
                                                    "“", //0x93
                                                    "”", //0x94
                                                    "•", //x95
                                                    "ᵉʳ", //0x96
                                                    "ʳᵉ", //0x97
                                                    "˜", //0x98
                                                    "™", //0x99
                                                    "š", //0x9A
                                                    "›", //0x9B
                                                    "œ", //0x9C
                                                    "•", //0x9D
                                                    "ž", //0x9E
                                                    "Ÿ", //0x9F
                                                    " ", //0xA0
                                                    "¡", //0xA1
                                                    "¢", //0xA2
                                                    "£", //0xA3
                                                    "¤", //0xA4
                                                    "¥", //0xA5
                                                    "¦", //0xA6
                                                    "§", //0xA7
                                                    "¨", //0xA8
                                                    "©", //0xA9
                                                    "ª", //0xAA
                                                    "«", //0xAB
                                                    "¬", //0xAC
                                                    "—", //0xAD
                                                    "®", //0xAE
                                                    "¯", //0xAF
                                                    "°", //0xB0
                                                    "±", //0xB1
                                                    "²", //0xB2
                                                    "³", //0xB3
                                                    "´", //0xB4
                                                    "µ", //0xB5
                                                    "¶", //0xB6
                                                    "„", //0xB7
                                                    "‚", //0xB8
                                                    "¹", //0xB9
                                                    "⁰", //0xBA
                                                    "»", //0xBB
                                                    "←", //0xBC
                                                    "♂", //0xBD
                                                    "♀", //0xBE
                                                    "¿", //0xBF
                                                    "À", //0xC0
                                                    "Á", //0xC1
                                                    "Â", //0xC2
                                                    "Ã", //0xC3
                                                    "Ä", //0xC4
                                                    "Å", //0xC5
                                                    "Æ", //0xC6
                                                    "Ç", //0xC7
                                                    "È", //0xC8
                                                    "É", //0xC9
                                                    "Ê", //0xCA
                                                    "Ë", //0xCB
                                                    "Ì", //0xCC
                                                    "Í", //0xCD
                                                    "Î", //0xCE
                                                    "Ï", //0xCF
                                                    "Ð", //0xD0
                                                    "Ñ", //0xD1
                                                    "Ò", //0xD2
                                                    "Ó", //0xD3
                                                    "Ô", //0xD4
                                                    "Õ", //0xD5
                                                    "Ö", //0xD6
                                                    "×", //0xD7
                                                    "Ø", //0xD8
                                                    "Ù", //0xD9
                                                    "Ú", //0xDA
                                                    "Û", //0xDB
                                                    "Ü", //0xDC
                                                    "Ý", //0xDD
                                                    "Þ", //0xDE
                                                    "ß", //0xDF
                                                    "à", //0xE0
                                                    "á", //0xE1
                                                    "â", //0xE2
                                                    "ã", //0xE3
                                                    "ä", //0xE4
                                                    "å", //0xE5
                                                    "æ", //0xE6
                                                    "ç", //0xE7
                                                    "è", //0xE8
                                                    "é", //0xE9
                                                    "ê", //0xEA
                                                    "ë", //0xEB
                                                    "ì", //0xEC
                                                    "í", //0xED
                                                    "î", //0xEE
                                                    "ï", //0xEF
                                                    "ð", //0xF0
                                                    "ñ", //0xF1
                                                    "ò", //0xF2
                                                    "ó", //0xF3
                                                    "ô", //0xF4
                                                    "õ", //0xF5
                                                    "ö", //0xF6
                                                    "÷", //0xF7
                                                    "ø", //0xF8
                                                    "ù", //0xF9
                                                    "ú", //0xFA
                                                    "û", //0xFB
                                                    "ü", //0xFC
                                                    "ý", //0xFD
                                                    "þ", //0xFE
                                                    "ÿ"}; //0xFF

        public override int GetByteCount(char[] chars, int index, int count)
        {
            var byteCount = 0;
            for (int i = 0; i < count; i++)
            {
                switch (chars[i])
                {
                    case '\0':
                        byteCount += 1;
                        return byteCount;
                    case '\\':
                        if (chars.Length > index + i + 4)
                        {
                            int parsed;
                            var x = new string(chars.Skip(i + 1).Take(4).ToArray());
                            if (int.TryParse(x, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out parsed))
                            {
                                // This is an escape sequence
                                i += 4;
                                byteCount += 2;
                            }
                            else
                            {
                                // This is not an escape sequence
                                byteCount += 1;
                            }
                        }
                        else
                        {
                            byteCount += 1;
                        }
                        break;
                    default:
                        byteCount += 1;
                        break;
                }
            }
            return byteCount;
        }

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            var eightBitChars = EightBitCharacters.ToList();
            var byteCount = 0;
            for (int i = 0; i < charCount; i++)
            {
                switch (chars[i])
                {
                    case '\0':
                        bytes[byteCount] = 0;
                        byteCount += 1;
                        return charCount;
                    case '\\':
                        if (chars.Length > charIndex + i + 4)
                        {
                            int parsed;
                            var x = new string(chars.Skip(i + 1).Take(4).ToArray());
                            if (int.TryParse(x, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out parsed))
                            {
                                // This is an escape sequence
                                bytes[byteCount] = (byte)((parsed >> 8) & 0xFF);
                                bytes[byteCount + 1] = (byte)(parsed & 0xFF);
                                i += 4;
                                byteCount += 2;
                            }
                            else
                            {
                                // This is not an escape sequence
                                bytes[byteCount] = 0x5C;
                                byteCount += 1;
                            }
                        }
                        else
                        {
                            bytes[byteCount] = 0x5C;
                            byteCount += 1;
                        }
                        break;
                    default:
                        bytes[byteCount] = (byte)eightBitChars.LastIndexOf(chars[i].ToString());
                        byteCount += 1;
                        break;
                }
            }
            return byteCount;
        }

        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            int charCount = 0;
            for (int i = index; i < count + index; i++)
            {
                switch (bytes[i])
                {
                    case 0:
                        return charCount;
                    case 0x81:
                    case 0x82:
                    case 0x83:
                    case 0x84:
                    case 0x87:
                        // Ex. `\81FF`
                        charCount += 5;
                        i += 1;
                        break;
                    default:
                        charCount += 1;
                        break;
                }
            }
            return charCount;
        }

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            int charCount = 0;
            for (int i = 0; i < byteCount; i++)
            {
                var current = bytes[i + byteIndex];
                switch (current)
                {
                    // To-do: handle this, escape, escape sequences, etc.
                    case 0:
                        return charCount;
                    case 0x81:
                    case 0x82:
                    case 0x83:
                    case 0x84:
                    case 0x87:
                        var escape = $"\\{current.ToString("X2")}{bytes[i + byteIndex + 1].ToString("X2")}";
                        for (int j = 0; j < escape.Length; j++)
                        {
                            chars[charIndex + i + j] = escape[j];
                        }

                        i += 1;
                        charCount += escape.Length;
                        break;
                    default:
                        chars[charIndex + i] = EightBitCharacters[current].Length > 0 ? EightBitCharacters[current][0] : '?';
                        charCount += 1;
                        break;
                }
            }
            return charCount;
        }

        public override int GetMaxByteCount(int charCount)
        {
            return charCount * 5;
        }

        public override int GetMaxCharCount(int byteCount)
        {
            return byteCount;
        }
    }
}
