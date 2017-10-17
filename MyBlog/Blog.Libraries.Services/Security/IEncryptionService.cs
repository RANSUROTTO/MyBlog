namespace Blog.Libraries.Services.Security
{

    public interface IEncryptionService
    {

        /// <summary>
        /// 创建加密盐值
        /// </summary>
        /// <param name="size">盐值大小</param>
        /// <returns>盐值</returns>
        string CreateSaltKey(int size);

        /// <summary>
        /// 哈希函数处理
        /// </summary>
        /// <param name="text">需处理文本</param>
        /// <param name="saltkey">哈希盐值</param>
        /// <param name="hashAlgorithm">哈希算法</param>
        /// <returns>处理后的文本</returns>
        string CreateHash(string text, string saltkey, string hashAlgorithm = "SHA1");

        /// <summary>
        /// 哈希函数处理
        /// </summary>
        /// <param name="data">需处理数据</param>
        /// <param name="hashAlgorithm">Hash algorithm</param>
        /// <returns>Data hash</returns>
        string CreateHash(byte[] data, string hashAlgorithm = "SHA1");

        /// <summary>
        /// 加密文本
        /// </summary>
        /// <param name="plainText">需要加密的文本</param>
        /// <param name="encryptionPrivateKey">加密密钥</param>
        /// <returns>加密后的文本</returns>
        string EncryptText(string plainText, string encryptionPrivateKey = "");

        /// <summary>
        /// 解密文本
        /// </summary>
        /// <param name="cipherText">需解密的密文</param>
        /// <param name="encryptionPrivateKey">解密密钥</param>
        /// <returns>解密后的文本</returns>
        string DecryptText(string cipherText, string encryptionPrivateKey = "");

    }

}
