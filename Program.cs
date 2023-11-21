using SkiaSharp;
//using é usado para limpar a memoria depois de usar ela

namespace Projeto {
	class Program {
		static unsafe byte menorValorRegiao(byte* entrada, int largura, int altura, int x , int y , int metadeJanela){
			int yInicial = y - metadeJanela;
			int xInicial = x - metadeJanela;
			int yFinal = y + metadeJanela;
			int xFinal = x + metadeJanela;

			if (xInicial < 0) {
				xInicial = 0;
			}
			if (yInicial < 0) {
				yInicial = 0;
			}
			if (xFinal > largura - 1) {
				xFinal = largura - 1;
			}
			if (yFinal > altura - 1) {
				yFinal = altura - 1;
			}

			byte menor = entrada[(yInicial * largura) + xInicial];

			for(y = yInicial; y <= yFinal; y++){
				for(x = xInicial; x <= xFinal; x++){
					int i = (y * largura) + x;
					if( entrada[i] < menor ){
						menor = entrada[i];
					}
				}
			}
			return menor;
		}

		static void Main(string[] args) {
			using (SKBitmap bitmapEntrada = SKBitmap.Decode("C:\\Users\\HENRIQUE.CORT\\Desktop\\ComputaçãoCognitiva\\Limiarizacao\\entrada\\GabaritoCorreto1.png"),
				bitmapSaidaAritmetica = new SKBitmap(new SKImageInfo(bitmapEntrada.Width, bitmapEntrada.Height, SKColorType.Gray8))) {
			
				unsafe {
					byte* entrada = (byte*)bitmapEntrada.GetPixels();
					byte* saidaAritmetica = (byte*)bitmapSaidaAritmetica.GetPixels();		

					int pixelsTotais = bitmapEntrada.Width * bitmapEntrada.Height;
					Console.Write(bitmapEntrada.Width);
					Console.Write(bitmapEntrada.Height);
					long media= 0;

					for (int e = 0, s = 0; s < pixelsTotais; e += 4, s++) {
						saidaAritmetica[s] = (byte)((entrada[e] + entrada[e + 1] + entrada[e + 2]) / 3);
						media += saidaAritmetica[s];
					}
					media = (byte)(media /pixelsTotais);

					for (int s = 0; s < pixelsTotais; s++) {
						if ( saidaAritmetica[s] > media){
							saidaAritmetica[s] = 0;
						}else{
							saidaAritmetica[s] = 255;
						};
					}
                }
					using (FileStream stream = new FileStream("C:\\Users\\HENRIQUE.CORT\\Desktop\\ComputaçãoCognitiva\\Limiarizacao\\entrada\\GabaritoLimiarizado.png", FileMode.OpenOrCreate, FileAccess.Write)) {
						bitmapSaidaAritmetica.Encode(stream, SKEncodedImageFormat.Png, 100);
					}
			}		

			using (SKBitmap bitmapEntrada = SKBitmap.Decode("C:\\Users\\HENRIQUE.CORT\\Desktop\\ComputaçãoCognitiva\\Limiarizacao\\entrada\\GabaritoLimiarizado.png"),
				bitmapSaidaAritmetica = new SKBitmap(new SKImageInfo(bitmapEntrada.Width, bitmapEntrada.Height, SKColorType.Gray8))) {
					
				int largura = bitmapEntrada.Width;
				int altura = bitmapEntrada.Height;
				int tamanhoJanela = 9;
				int metadeJanela = tamanhoJanela / 2 ;
				Console.Write(largura);
				Console.Write(altura);

				unsafe {
					byte* entrada = (byte*)bitmapEntrada.GetPixels();
					byte* saidaAritmetica = (byte*)bitmapSaidaAritmetica.GetPixels();		

					for (int y = 0; y < altura -1 ; y++) {
						for (int x= 0; x < largura-1 ; x++) {
                            saidaAritmetica [y * largura + x ] = menorValorRegiao(entrada, largura,altura, x, y, metadeJanela);
						}
					}					
				}
					using (FileStream stream = new FileStream("C:\\Users\\HENRIQUE.CORT\\Desktop\\ComputaçãoCognitiva\\Limiarizacao\\saida\\GabaritoErosao.png", FileMode.OpenOrCreate, FileAccess.Write)) {
						bitmapSaidaAritmetica.Encode(stream, SKEncodedImageFormat.Png, 100);
					}
			}		
		}
	}
}
