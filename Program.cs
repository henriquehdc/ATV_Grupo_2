using SkiaSharp;

namespace Projeto {
	class Program {

		static unsafe byte Erodir(byte* entrada, int largura, int altura, int x , int y , int tamanhoErosao){
			int yInicial = y - tamanhoErosao;
			int xInicial = x - tamanhoErosao;
			int yFinal = y + tamanhoErosao;
			int xFinal = x + tamanhoErosao;

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
			int cena;
            int quadros;
			
            Console.WriteLine("Qual cena deseja analisar?");
            cena = Convert.ToInt32(Console.ReadLine());

			Console.WriteLine("Quantos quadros da cena "+ cena+" deseja analisar?");
            quadros = Convert.ToInt32(Console.ReadLine());

			for (int i = 0 ; i< quadros; i++){
				using (SKBitmap bitmapEntrada = SKBitmap.Decode("C:\\Users\\HENRIQUE.CORT\\Desktop\\ComputaçãoCognitiva\\ATV_Grupo_2\\ATV_Grupo_2\\entrada\\Cena"+cena+"_"+i+".png"),
				bitmapSaidaAritmetica = new SKBitmap(new SKImageInfo(bitmapEntrada.Width, bitmapEntrada.Height, SKColorType.Gray8))) {
			
					unsafe {
						byte* entrada = (byte*)bitmapEntrada.GetPixels();
						byte* saida = (byte*)bitmapSaidaAritmetica.GetPixels();			
						
						int pixelsTotais = bitmapEntrada.Width * bitmapEntrada.Height;

						for (int e = 0, s = 0; s < pixelsTotais; e += 4, s++) {
							if((entrada[e+1] > entrada[e] ) && (entrada[e+1] > entrada[e+2])){
								saida[s] = 0;
							}else{
								saida[s] = 255;
							}
						}

					}
					using (FileStream stream = new FileStream("C:\\Users\\HENRIQUE.CORT\\Desktop\\ComputaçãoCognitiva\\ATV_Grupo_2\\ATV_Grupo_2\\saida\\Cena"+cena+"_"+i+"_saida.png", FileMode.OpenOrCreate, FileAccess.Write)) {
						bitmapSaidaAritmetica.Encode(stream, SKEncodedImageFormat.Png, 100);
					}
				}		

				using (SKBitmap bitmapEntrada = SKBitmap.Decode("C:\\Users\\HENRIQUE.CORT\\Desktop\\ComputaçãoCognitiva\\ATV_Grupo_2\\ATV_Grupo_2\\saida\\Cena"+cena+"_"+i+"_saida.png"),
				bitmapSaidaAritmetica = new SKBitmap(new SKImageInfo(bitmapEntrada.Width, bitmapEntrada.Height, SKColorType.Gray8))) {
			
					unsafe{
						byte* entrada = (byte*)bitmapEntrada.GetPixels();
						byte* saida = (byte*)bitmapSaidaAritmetica.GetPixels();	
						int largura = bitmapEntrada.Width;
						int altura = bitmapEntrada.Height;
						int tamanhoErosao = 5;

						for (int y = 0; y < altura -1 ; y++) {
							for (int x= 0; x < largura-1 ; x++) {
								saida [y * largura + x ] = Erodir(entrada, largura,altura, x, y, tamanhoErosao);
							}
						}
					}

					using (FileStream stream = new FileStream("C:\\Users\\HENRIQUE.CORT\\Desktop\\ComputaçãoCognitiva\\ATV_Grupo_2\\ATV_Grupo_2\\saida\\Cena"+cena+"_"+i+"_saida.png", FileMode.OpenOrCreate, FileAccess.Write)) {
						bitmapSaidaAritmetica.Encode(stream, SKEncodedImageFormat.Png, 100);
					}
				}
						
			}
				
		}
	}
}
