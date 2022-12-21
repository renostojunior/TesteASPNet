using TesteASPNet.Service.Utils;

namespace TesteASPNet.Tests
{
    public class UnitTest
    {
        [Fact]
        public void FuncValidCNPJ()
        {
            var cnpj = "70730668000170";

            Assert.True(Utils.IsCnpj(cnpj));
        }
    }
}