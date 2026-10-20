//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.IO;

namespace GameFramework
{
    public static partial class Utility
    {
        public static partial class Compression
        {
            /// <summary>
            /// Compression helper interface.
            /// </summary>
            public interface ICompressionHelper
            {
                /// <summary>
                /// Compresses data.
                /// </summary>
                /// <param name="bytes">The binary data bytes to compress.</param>
                /// <param name="offset">The offset of the binary data bytes to compress.</param>
                /// <param name="length">The length of the binary data bytes to compress.</param>
                /// <param name="compressedStream">The stream to store the compressed binary data.</param>
                /// <returns>Whether data is compressed successfully.</returns>
                bool Compress(byte[] bytes, int offset, int length, Stream compressedStream);

                /// <summary>
                /// Compresses data.
                /// </summary>
                /// <param name="stream">The binary data stream to compress.</param>
                /// <param name="compressedStream">The stream to store the compressed binary data.</param>
                /// <returns>Whether data is compressed successfully.</returns>
                bool Compress(Stream stream, Stream compressedStream);

                /// <summary>
                /// Decompresses data.
                /// </summary>
                /// <param name="bytes">The binary data bytes to decompress.</param>
                /// <param name="offset">The offset of the binary data bytes to decompress.</param>
                /// <param name="length">The length of the binary data bytes to decompress.</param>
                /// <param name="decompressedStream">The stream to store the decompressed binary data.</param>
                /// <returns>Whether data is decompressed successfully.</returns>
                bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream);

                /// <summary>
                /// Decompresses data.
                /// </summary>
                /// <param name="stream">The binary data stream to decompress.</param>
                /// <param name="decompressedStream">The stream to store the decompressed binary data.</param>
                /// <returns>Whether data is decompressed successfully.</returns>
                bool Decompress(Stream stream, Stream decompressedStream);
            }
        }
    }
}
