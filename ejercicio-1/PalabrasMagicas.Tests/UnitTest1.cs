using Xunit;

public class PalabrasMagicasTests
{
    [Fact]
    public void InvertirPalabra_DeberiaInvertirCorrectamente()
    {
        string palabra = "Hola";
        string resultadoEsperado = "aloH";

        string resultado = Program.InvertirPalabra(palabra);

        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void EsCapicua_DeberiaDetectarCapicua()
    {
        string palabra = "anilina";

        bool resultado = Program.EsCapicua(palabra);

        Assert.True(resultado);
    }

    [Fact]
    public void ContarVocales_DeberiaContarCorrectamente()
    {
        string palabra = "Hola";
        int resultadoEsperado = 2;

        int resultado = Program.ContarVocales(palabra);

        Assert.Equal(resultadoEsperado, resultado);
    }
}
